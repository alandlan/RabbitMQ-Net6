using Domain.Entity;

namespace Domain.UseCases
{
    public interface IProcessUseCase
    {
        Task<ProcessEntity> CreateAsync(ProcessEntity process);
    }
}
