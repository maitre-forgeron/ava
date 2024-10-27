using Ava.Domain.Models.User;
using Ava.Infrastructure.Services.UserService.Interfaces;

namespace Ava.Infrastructure.Services.UserService
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> GetCustomerProfileAsync(Guid customerId)
        {
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            return customer;
        }

        public async Task<Therapist> GetTherapistProfileAsync(Guid therapistId)
        {
            var therapist = await _unitOfWork.Therapists.GetTherapistByIdAsync(therapistId);
            if (therapist == null)
                throw new KeyNotFoundException("Therapist not found.");
            return therapist;
        }

        public async Task<List<Review>> GetTopReviewsForTherapistAsync(Guid therapistId, int count)
        {
            return await _unitOfWork.Therapists.GetTopReviewsForTherapistAsync(therapistId, count);
        }

        public async Task<List<Review>> GetMoreReviewsForTherapistAsync(Guid therapistId, int skip, int take)
        {
            return await _unitOfWork.Therapists.GetReviewsForTherapistAsync(therapistId, skip, take);
        }

        public async Task AddReviewAsync(Guid senderId, Guid recipientId, int reviewValue, string reviewText)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(senderId);
            UserProfile senderProfile = customer?.UserProfile;

            if (senderProfile == null)
            {
                var therapist = await _unitOfWork.Therapists.GetByIdAsync(senderId);
                senderProfile = therapist?.UserProfile;
            }

            var recipientTherapist = await _unitOfWork.Therapists.GetByIdAsync(recipientId);
            var recipientProfile = recipientTherapist?.UserProfile;

            if (senderProfile == null || recipientProfile == null)
                throw new ArgumentException("Invalid sender or recipient.");

            var review = new Review
            {
                SenderId = senderId,
                RecipientId = recipientId,
                ReviewValue = reviewValue,
                ReviewText = reviewText,
                Sender = senderProfile,
                Recipient = recipientProfile
            };

            _unitOfWork.Reviews.Add(review);
            await _unitOfWork.CommitAsync();
        }

    }
}
