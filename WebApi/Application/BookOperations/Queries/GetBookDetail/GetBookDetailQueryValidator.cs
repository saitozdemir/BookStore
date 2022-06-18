using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DbOperations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query=>query.BookId).GreaterThan(0);
        }
    }
}