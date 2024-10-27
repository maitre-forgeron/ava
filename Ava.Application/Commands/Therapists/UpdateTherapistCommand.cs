using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Commands.Therapists
{
    public class UpdateTherapistCommand : IRequest
    {
        public TherapistDto Therapist { get; set; }
    }
}
