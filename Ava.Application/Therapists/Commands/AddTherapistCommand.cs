﻿using Ava.Application.Dtos;
using Ava.Domain.Interfaces.Repositories.UserRepositories;
using Ava.Domain.Models.User;
using MediatR;

namespace Ava.Application.Therapists.Commands
{
    public class AddTherapistCommand : IRequest<TherapistDto>
    {
        public TherapistDto TherapistDto { get; set; }

        public AddTherapistCommand(TherapistDto therapistDto)
        {
            TherapistDto = therapistDto;
        }
    }

    public class AddTherapistCommandHandler : IRequestHandler<AddTherapistCommand, TherapistDto>
    {
        private readonly ITherapistRepository _therapistRepository;

        public AddTherapistCommandHandler(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }

        public async Task<TherapistDto> Handle(AddTherapistCommand request, CancellationToken cancellationToken)
        {
            var therapist = new Therapist(
                userProfileId: request.TherapistDto.UserProfileId,
                rating: request.TherapistDto.Rating,
                summary: request.TherapistDto.Summary,
                certificateId: request.TherapistDto.CertificateId
            );

            if (request.TherapistDto.Reviews != null && request.TherapistDto.Reviews.Any())
            {
                foreach (var reviewDto in request.TherapistDto.Reviews)
                {
                    var review = new Review
                    {
                        AuthorId = reviewDto.SenderId,
                        RecipientId = reviewDto.RecipientId,
                        Rating = reviewDto.ReviewValue,
                        Summary = reviewDto.ReviewText,
                        Author = null,
                        Recipient = null
                    };
                    therapist.Reviews.Add(review);
                }
            }

            await _therapistRepository.AddTherapistAsync(therapist);

            return new TherapistDto
            {
                UserProfileId = therapist.UserProfileId,
                Rating = therapist.Rating,
                Summary = therapist.Summary,
                CertificateId = therapist.CertificateId,
                Id = therapist.Id,
                Reviews = request.TherapistDto.Reviews
            };
        }
    }
}