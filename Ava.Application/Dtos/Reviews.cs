﻿namespace Ava.Application.Dtos;

public record ReviewDto(Guid Id, Guid AuthorId, Guid RecipientId, int Rating, string Summary);
public record RatingSummary(double AverageRating, int TotalReviews);