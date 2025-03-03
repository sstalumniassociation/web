using System.Text.Json.Serialization;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CreateUserCheckInRequest), "User")]
[JsonDerivedType(typeof(CreateGuestCheckInRequest), "Guest")]
public abstract class CreateCheckInRequest
{
    public string Type { get; set; }
}

public class CreateUserCheckInRequest : CreateCheckInRequest
{
    public Guid UserId { get; set; }
}

public class CreateGuestCheckInRequest : CreateCheckInRequest
{
    public string Name { get; set; }
    public string Nric { get; set; }
    public string Phone { get; set; }
    public string Reason { get; set; }
}