using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Core
{
    public class Factory : IDbContextFactory<SqlServerContext>
    {
        private const int DefaultTenantId = -1;

        private readonly IDbContextFactory<SqlServerContext> _pooledFactory;

        public Factory(
            IDbContextFactory<SqlServerContext> pooledFactory)
        {
            _pooledFactory = pooledFactory;
        }

        public SqlServerContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();
            return context;
        }
    }
}
