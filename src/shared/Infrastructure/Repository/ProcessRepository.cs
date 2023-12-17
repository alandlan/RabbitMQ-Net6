using Domain.Entity;
using Domain.Repositories;
using Infrastructure.Database.Core;

namespace Infrastructure.Repository
{
    public class ProcessRepository : IProcessRepository
    {
        //create a new process repository class
        private readonly SqlServerContext _context;

        public ProcessRepository(SqlServerContext context)
        {
            _context = context;
        }

        //create a new process
        public async Task<ProcessEntity> CreateAsync(ProcessEntity process)
        {
            _context.Processes.Add(process);
            await _context.SaveChangesAsync();
            return process;
        }
    }
}
