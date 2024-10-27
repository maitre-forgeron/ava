using Ava.Application.Dtos;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetAllTherapistsQuery : IRequest<IEnumerable<TherapistDto>> { }
}
