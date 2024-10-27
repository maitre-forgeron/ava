using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetTherapistProfileQuery : IRequest<TherapistDto>
    {
        public Guid Id { get; set; }
    }
}
