using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Commands.Therapists
{
    public class UpdateTherapistCommand : IRequest
    {
        public Therapist Therapist { get; set; }
    }
}
