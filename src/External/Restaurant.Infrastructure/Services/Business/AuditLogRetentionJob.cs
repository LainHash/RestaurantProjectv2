using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Restaurant.Application.Services.Business;
using Restaurant.Contract.Settings.AuditLog;

namespace Restaurant.Infrastructure.Services.Business
{
    /// <summary>
    /// Background Service chạy mỗi ngày một lần để xóa audit log cũ hơn RetentionDays.
    /// </summary>
    internal class AuditLogRetentionJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AuditLogRetentionJob> _logger;
        private readonly int _retentionDays;

        // Chạy lúc 02:00 UTC mỗi ngày
        private static readonly TimeOnly _runAt = new(2, 0, 0);

        public AuditLogRetentionJob(
            IServiceScopeFactory scopeFactory,
            IOptions<AuditLogSettings> settings,
            ILogger<AuditLogRetentionJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _retentionDays = settings.Value.RetentionDays;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "AuditLogRetentionJob started. Retention: {Days} days, runs daily at {Time} UTC.",
                _retentionDays, _runAt);

            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = CalculateDelay();
                _logger.LogDebug("Next audit log purge in {Delay}.", delay);
                await Task.Delay(delay, stoppingToken);

                if (stoppingToken.IsCancellationRequested) break;

                await PurgeAsync(stoppingToken);
            }
        }

        private async Task PurgeAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting audit log purge (retention = {Days} days).", _retentionDays);

                await using var scope = _scopeFactory.CreateAsyncScope();
                var service = scope.ServiceProvider.GetRequiredService<IAuditLogService>();
                await service.PurgeOldLogsAsync(_retentionDays, cancellationToken);

                _logger.LogInformation("Audit log purge completed.");
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Audit log purge failed.");
            }
        }

        private static TimeSpan CalculateDelay()
        {
            var now = DateTime.UtcNow;
            var nextRun = DateTime.UtcNow.Date.Add(_runAt.ToTimeSpan());

            if (nextRun <= now)
                nextRun = nextRun.AddDays(1);

            return nextRun - now;
        }
    }
}
