using System.Text;
using ODataQueryBuilder.Filter.Filters;
using ODataQueryBuilder.Filter.Filters.Shared;
using ODataQueryBuilder.Filter.Interfaces;
using ODataQueryBuilder.Request.Models;

namespace ODataQueryBuilder.Request;

public sealed class RequestFilter : CollectionFilter
{
    private readonly RequestConfiguration _settings = new();
    private Func<string, string>? _transformFunction;

    internal RequestFilter(IFilter filter) : base(filter)
    {
        _settings.ParameterName = "$filter";
    }

    public RequestFilter WithParameterName(string name)
    {
        _settings.ParameterName = name;

        return this;
    }

    public RequestFilter WithTransform(Func<string, string> transform)
    {
        _transformFunction = transform;

        return this;
    }

    public RequestFilter And(Func<BlockFilterBuilder, BlockFilter> configure)
    {
        AppendFilter(FilterType.And, configure(new BlockFilterBuilder()));

        return this;
    }

    public RequestFilter Or(Func<BlockFilterBuilder, BlockFilter> configure)
    {
        AppendFilter(FilterType.Or, configure(new BlockFilterBuilder()));

        return this;
    }

    internal RequestConfiguration Build()
    {
        var builder = new StringBuilder();

        foreach (var filter in _filters)
        {
            builder.Append(filter.Build());
        }

        _settings.Value = _transformFunction?.Invoke(builder.ToString()) ?? builder.ToString();

        return _settings;
    }
}