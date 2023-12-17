using Domain.Entity;

namespace Domain.Repositories
{
    public interface IProcessRepository
    {
        Task<ProcessEntity> CreateAsync(ProcessEntity processEntity);
    }
}
