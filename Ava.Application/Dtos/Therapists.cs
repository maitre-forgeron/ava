namespace Ava.Application.Dtos;

public record TherapistDto(Guid Id, string FirstName, string LastName, double Rating, string Summary, Guid CertificateId, List<ReviewDto> Reviews);

public record UpdateTherapistDto(Guid Id, string FirstName, string LastName, double Rating, string Summary);

public record CreateTherapistDto(Guid Id, string FirstName, string LastName, string PersonalId, double Rating, string Summary);
