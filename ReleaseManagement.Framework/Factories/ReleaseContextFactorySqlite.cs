using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ReleaseManagement.Framework.Data;
using System;
using Microsoft.Extensions.Configuration;

namespace ReleaseManagement.Framework.Factories
{
    public class ReleaseContextFactorySqlite : IDesignTimeDbContextFactory<ReleaseDataContext>
    {
        public ReleaseContextFactorySqlite()
        {
            _connectionStringName = "ReleaseDB";
        }

        private readonly string _connectionStringName;

        public ReleaseDataContext CreateDbContext(string[] args)
        {
            var environmentName =
                        Environment.GetEnvironmentVariable(
                            "Hosting:Environment");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        private ReleaseDataContext Create(string basePath, string environmentName)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true);
            //.AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString(_connectionStringName);

            if (String.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException($"Could not find a connection string named '{_connectionStringName}'.");
            }
            else
            {
                if (string.IsNullOrEmpty(connstr))
                    throw new ArgumentException(
                        $"{nameof(_connectionStringName)} is null or empty.",
                        nameof(_connectionStringName));

                var optionsBuilder = new DbContextOptionsBuilder<ReleaseDataContext>();

                optionsBuilder.UseSqlite(connstr);

                return new ReleaseDataContext(optionsBuilder.Options);
            }
        }

        private ReleaseDataContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                    $"{nameof(connectionString)} is null or empty.",
                    nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<ReleaseDataContext>();

            optionsBuilder.UseSqlite(connectionString);

            return new ReleaseDataContext(optionsBuilder.Options);
        }
    }
}
