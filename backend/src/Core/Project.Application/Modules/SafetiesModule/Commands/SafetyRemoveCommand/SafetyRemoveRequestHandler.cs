using MediatR;
using Project.Application.Repositories;


namespace Project.Application.Modules.SafetiesModule.Commands.SafetyRemoveCommand
{
    class SafetyRemoveRequestHandler : IRequestHandler<SafetyRemoveRequest>
    {
        private readonly ISafetyRepository safetyRepository;

        public SafetyRemoveRequestHandler(ISafetyRepository safetyRepository)
        {
            this.safetyRepository = safetyRepository;
        }
        public async Task Handle(SafetyRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await safetyRepository.GetAsync(x=>x.Id==request.Id && x.DeletedBy == null, cancellationToken);
            safetyRepository.Remove(entity);
            await safetyRepository.SaveAsync(cancellationToken);
        }
    }
}
