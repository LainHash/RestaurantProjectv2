using FluentValidation;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Commands.Update
{
    public class UpdateRestaurantTableCommandValidator
        : AbstractValidator<UpdateRestaurantTableCommand>
    {
        public UpdateRestaurantTableCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.");

            When(x => x.Body != null, () =>
            {
                RuleFor(x => x.Body.AreaId)
                    .NotEmpty().WithMessage("AreaId is required.");

                RuleFor(x => x.Body.TableNumber)
                    .NotEmpty().WithMessage("TableNumber is required.")
                    .MaximumLength(20).WithMessage("TableNumber must not exceed 20 characters.");

                RuleFor(x => x.Body.Capacity)
                    .GreaterThan(0).WithMessage("Capacity must be greater than 0.");

                RuleFor(x => x.Body.Shape)
                    .IsInEnum().WithMessage("Invalid table shape.");

                RuleFor(x => x.Body.Status)
                    .IsInEnum().WithMessage("Invalid table status.");

                RuleFor(x => x.Body.PositionX)
                    .GreaterThanOrEqualTo(0).WithMessage("PositionX must be greater than or equal to 0.");

                RuleFor(x => x.Body.PositionY)
                    .GreaterThanOrEqualTo(0).WithMessage("PositionY must be greater than or equal to 0.");

                RuleFor(x => x.Body.Width)
                    .GreaterThan(0).WithMessage("Width must be greater than 0.");

                RuleFor(x => x.Body.Height)
                    .GreaterThan(0).WithMessage("Height must be greater than 0.");

                RuleFor(x => x.Body.Rotation)
                    .InclusiveBetween(0, 360).WithMessage("Rotation must be between 0 and 360 degrees.");
            });
        }
    }
}
