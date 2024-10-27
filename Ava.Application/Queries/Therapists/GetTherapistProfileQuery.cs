using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Queries.Therapists
{
    public class GetTherapistProfileQuery : IRequest<Therapist>
    {
        public Guid Id { get; set; }
    }
}
