using FluentValidation;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.UpdateByCustomer
{
    public class UpdateReservationByCustomerCommandValidator
        : AbstractValidator<UpdateReservationByCustomerCommand>
    {
        public UpdateReservationByCustomerCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
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
            });
        }
    }
}
