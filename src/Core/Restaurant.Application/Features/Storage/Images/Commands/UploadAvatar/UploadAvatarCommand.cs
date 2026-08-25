using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Commands.UpdateAvatar
{
    public record UploadAvatarCommand(Guid UserId, Stream FileStream, string FileName)
        : IRequest<Result>
    {
    }
}
