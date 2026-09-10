using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Delete
{
    internal class DeleteAreaCommandHandler(IAreaService areaService)
        : IRequestHandler<DeleteAreaCommand, Result>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<Result> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteAreaSpecification(request);
            var response = await _areaService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
