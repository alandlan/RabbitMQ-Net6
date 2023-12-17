using Domain.Entity;
using Domain.Repositories;
using Domain.UseCases;

namespace App.UseCases
{
    public class ProcessUseCase : IProcessUseCase
    {
        private readonly IProcessRepository _processRepository;

        public ProcessUseCase(IProcessRepository processRepository)
        {
            _processRepository = processRepository;
        }
        
        public async Task<ProcessEntity> CreateAsync(ProcessEntity process)
        {
            return await _processRepository.CreateAsync(process);
        }
    }
}
