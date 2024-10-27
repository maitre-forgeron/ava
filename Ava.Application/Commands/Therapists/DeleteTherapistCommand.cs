using MediatR;

namespace Ava.Application.Commands.Therapists
{
    public class DeleteTherapistCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
