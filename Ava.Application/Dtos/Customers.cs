﻿namespace Ava.Application.Dtos;

public record CreateCustomerDto(Guid Id, string FirstName, string LastName, string PersonalId);

public record UpdateCustomerDto(Guid Id, string FirstName, string LastName);

public record CustomerDto(Guid Id, string FirstName, string LastName, string PersonalId, Guid? PhotoId);
