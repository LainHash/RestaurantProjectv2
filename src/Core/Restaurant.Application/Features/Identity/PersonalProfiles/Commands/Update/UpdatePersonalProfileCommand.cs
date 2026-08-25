using MediatR;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.PersonalProfiles.Commands.Update
{
    public record UpdatePersonalProfileCommand(Guid UserId, UpdatePersonalProfileRequest Body)
        : IRequest<Result<PersonalProfileResponse>>
    {
    }
}
