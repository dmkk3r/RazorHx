using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace RazorHx.Results.Interfaces;

public interface IRazorHxStreamResult : IResult, IContentTypeHttpResult
{
    Type ComponentType { get; }
    List<(Type, ParameterView)> OutOfBandComponents { get; }
}