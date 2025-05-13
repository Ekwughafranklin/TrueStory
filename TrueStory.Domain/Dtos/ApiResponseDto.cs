using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrueStory.Domain.Dtos;

public class ApiResponseDto<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Success";
    public T Data { get; set; }

    public ApiResponseDto()
    {
    }

    public ApiResponseDto(string message)
    {
        Message = message;
    }

    public ApiResponseDto(string message, T data)
    {
        Message = message;
        Data = data;
    }

    public ApiResponseDto(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public ApiResponseDto(T data)
    {
        Data = data;
    }
}

public record ValidationErrorDto(
    [property: JsonPropertyName("propertyName")]
    string PropertyName,
    [property: JsonPropertyName("errorMessage")]
    string ErrorMessage);
public record ErrorResponseDto(
    [property: JsonPropertyName("success")]
    bool Success,
    [property: JsonPropertyName("message")]
    string Message,
    [property: JsonPropertyName("errors")]
    IEnumerable<ValidationErrorDto> Errors);