using System;
using System.Threading;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NET.Core.Base.Api.Extensions
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        public SqlServerHealthCheck(string connection)
        {
            _connection = connection;
        }

        private readonly string _connection;

        public async  Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                using (var conn = new SqlConnection(_connection))
                {
                    await conn.OpenAsync(cancellationToken);
                    var command = conn.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM produtos";
                    return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
                }
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
