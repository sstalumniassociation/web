using System;
using FastEndpoints;
using FluentValidation;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Auth.Verify;

public class PostVerifyRequestValidator : Validator<PostVerifyRequest>
{
  public PostVerifyRequestValidator()
  {
    RuleFor(r => r.Email)
      .EmailAddress();
  }
}
