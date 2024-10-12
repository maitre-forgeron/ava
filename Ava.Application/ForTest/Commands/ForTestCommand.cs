using MediatR;

namespace Ava.Application.ForTest.Commands
{
    public record ForTestCommand : IRequest<bool>;

    public class ForTestCommandHandler : IRequestHandler<ForTestCommand, bool>
    {
        public Task<bool> Handle(ForTestCommand request, CancellationToken cancellationToken)
        {
            bool result = true;
            return Task.FromResult(result);
        }
    }
}
