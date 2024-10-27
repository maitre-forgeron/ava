using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Commands.Therapists
{
    public class AddTherapistCommand : IRequest<TherapistDto>
    {
        public TherapistDto TherapistDto { get; set; }

        public AddTherapistCommand(TherapistDto therapistDto)
        {
            TherapistDto = therapistDto;
        }
    }
}
