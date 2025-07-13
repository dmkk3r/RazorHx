using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using RazorHx.Htmx;

namespace RazorHx.Results.Interfaces;

public interface IRazorHxResult : IResult, IStatusCodeHttpResult, IContentTypeHttpResult
{
    Type ComponentType { get; }
    ParameterView Parameters { get; }
    List<(Type, ParameterView)> OutOfBandComponents { get; }
    List<HtmxTrigger> Triggers { get; }
}