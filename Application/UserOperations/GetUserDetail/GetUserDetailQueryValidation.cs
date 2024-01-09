using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UserOperations.GetUserDetail
{
    public class GetUserDetailQueryValidation : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidation()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
