using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Commands.Therapists
{
    public class AddTherapistCommand : IRequest
    {
        public Therapist Therapist { get; set; }
    }

}
