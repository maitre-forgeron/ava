using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetAllTherapistsQuery : IRequest<IEnumerable<Therapist>> { }
}
