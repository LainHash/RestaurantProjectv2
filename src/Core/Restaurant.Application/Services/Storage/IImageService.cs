using Restaurant.Application.Features.Storage.Images.Commands.UpdateAvatar;
using Restaurant.Application.Features.Storage.Images.Commands.Upload;
using Restaurant.Application.Features.Storage.Images.Queries.GetAll;
using Restaurant.Application.Features.Storage.Images.Queries.GetAllByProductId;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Storage
{
    public interface IImageService
    {
        Task<PageResult<IEnumerable<ImageResponse>>> GetAllAsync(
            GetAllImagesSpecification specification,
            CancellationToken cancellationToken);

        Task<PageResult<IEnumerable<ImageResponse>>> GetAllByProductIdAsync(
            GetAllImagesByProductIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<UploadImageResponse>> UploadProductImageAsync(
            UploadProductImageCommand command,
            CancellationToken cancellationToken);

        Task<Result> UploadAvatarAsync(
            UploadAvatarCommand command,
            CancellationToken cancellationToken = default);
    }
}
