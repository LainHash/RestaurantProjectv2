using MediatR;
using Restaurant.Application.Features.Storage.Images.Commands.UpdateAvatar;
using Restaurant.Application.Services.Storage;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Storage.Images.Commands.UploadAvatar
{
    internal class UploadAvatarCommandHandler(IImageService imageService)
                : IRequestHandler<UploadAvatarCommand, Result>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<Result> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
        {
            var response = await _imageService.UploadAvatarAsync(request, cancellationToken);
            return response;
        }
    }
}
