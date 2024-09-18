using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Core.CrossCuttingConcerns.Logging.Loggers
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger()
        {
            // IConfiguration yapılandırmasını oluşturun
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // AppSettings'ten MSSQL yapılandırmasını alın
            var msSqlConfig = configuration.GetSection("MsSqlConfiguration");
            var connectionString = msSqlConfig.GetValue<string>("ConnectionString");
            var tableName = msSqlConfig.GetValue<string>("TableName");
            var autoCreateTable = msSqlConfig.GetValue<bool>("AutoCreateSqlTable");

            // MSSQL loglama yapılandırması
            var columnOptions = new ColumnOptions();

            // Default olan Message kolonu zaten mevcut, bu yüzden yeniden eklemiyoruz.
            //columnOptions.AdditionalColumns = new List<SqlColumn>
            //{
            //    new SqlColumn("LogLevel", SqlDbType.NVarChar) // Özel kolon ekle
            //};

            Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    tableName: tableName,
                    autoCreateSqlTable: autoCreateTable,
                    columnOptions: columnOptions
                )
                .CreateLogger();
        }
    }
}
