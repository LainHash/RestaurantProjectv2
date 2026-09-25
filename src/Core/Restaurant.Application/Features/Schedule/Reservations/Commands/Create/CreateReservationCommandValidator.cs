using FluentValidation;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.Create
{
    public class CreateReservationCommandValidator
        : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.BranchId)
                    .NotEmpty().WithMessage("BranchId is required.");

                RuleFor(x => x.Body.ReservationDate)
                    .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow))
                    .WithMessage("Reservation date cannot be in the past.");

                RuleFor(x => x.Body.ReservationTime)
                    .InclusiveBetween(
                        new TimeOnly(8, 0),
                        new TimeOnly(21, 0))
                    .WithMessage("ReservationTime must be between 08:00 and 21:00.");

                RuleFor(x => x.Body.GuestCount)
                    .GreaterThan(0).WithMessage("GuestCount must be greater than 0.");

                RuleFor(x => x.Body.Note)
                    .MaximumLength(1000).WithMessage("Note must not exceed 1000 characters.")
                    .When(x => !string.IsNullOrEmpty(x.Body.Note));

                RuleFor(x => x.Body.ReservationTables)
                    .Must(tables => tables.Select(t => t.RestaurantTableId).Distinct().Count() == tables.Count())
                    .WithMessage("Each table can only appear once in a reservation.")
                    .When(x => x.Body.ReservationTables != null && x.Body.ReservationTables.Any());

                When(x => x.UserId == null, () =>
                {

                    RuleFor(x => x.Body.GuestName)
                        .NotEmpty().WithMessage("GuestName is required.")
                        .MaximumLength(100).WithMessage("GuestName must not exceed 100 characters.");

                    RuleFor(x => x.Body.GuestPhone)
                        .NotEmpty().WithMessage("GuestPhone is required.")
                        .MaximumLength(20).WithMessage("GuestPhone must not exceed 20 characters.")
                        .Matches(@"^\+?[0-9]{9,15}$").WithMessage("GuestPhone must be a valid phone number (9-15 digits).");

                    RuleFor(x => x.Body.GuestEmail)
                        .MaximumLength(256).WithMessage("GuestEmail must not exceed 256 characters.")
                        .EmailAddress().WithMessage("A valid email is required.")
                        .When(x => !string.IsNullOrWhiteSpace(x.Body.GuestEmail));
                });

                RuleForEach(x => x.Body.ReservationTables).ChildRules(table =>
                {
                    table.RuleFor(t => t.RestaurantTableId)
                        .NotEmpty().WithMessage("RestaurantTableId is required.");
                });
            });
        }
    }
}
