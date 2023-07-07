using WebAppHealthChecker.Application.WebApps.Queries.GetUserWebApps;

namespace WebAppHealthChecker.Application.WebApps.Queries.GetWebApp
{
    internal class GetWebAppQueryHandler : IRequestHandler<GetWebAppQuery, WebAppDto>
    {
        private readonly IApplicationUnitOfWork _uow;

        public GetWebAppQueryHandler(IApplicationUnitOfWork unitOfWork)
            => _uow = unitOfWork;

        public async Task<WebAppDto> Handle(GetWebAppQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.WebApps.Where(x => x.Guid == request.Guid).Select(x => new WebAppDto
            {
                CheckInterval = x.CheckInterval,
                LastCheck = x.LastCheck,
                LastStatusCode = x.LastStatusCode,
                Name = x.Name,
                URL = x.URL
            }).FirstOrDefaultAsync();
            return result;
        }
    }
}
