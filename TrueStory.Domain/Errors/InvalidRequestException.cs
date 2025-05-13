using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueStory.Domain.Dtos;

namespace TrueStory.Domain.Errors;

//standard helper class to handle 40 type error thrown from code
public class InvalidRequestException : Exception
{
    public int StatusCode { get; set; } = 400;
    public new string Message { get; set; } = string.Empty;

    public IEnumerable<ValidationErrorDto> Errors { get; set; } = new List<ValidationErrorDto>();

    public InvalidRequestException()
    {

    }

    public InvalidRequestException(string message)
        : base(message)
    {

    }

    public InvalidRequestException(string message, int statusCode)
        : base(message)
    {
        this.Message = message;
        this.StatusCode = statusCode;
    }

    public InvalidRequestException(string message, Exception inner)
        : base(message, inner)
    {

    }

    public InvalidRequestException(IEnumerable<ValidationFailure> failures, string message = "Validation error occured.")
        : base(message)
    {
        var errors = failures.Select(failure =>
            new ValidationErrorDto(failure.PropertyName, failure.ErrorMessage));
        Errors = errors;
    }
}