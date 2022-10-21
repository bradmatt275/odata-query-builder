using System.Text;
using ODataQueryBuilder.Filter.Filters.Shared;
using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters;

public sealed class RootFilter : CollectionFilter
{
    private Func<string, string>? _transformFunction;

    internal RootFilter(IFilter filter) : base(filter)
    {
        
    }

    public RootFilter And(Func<BlockFilterBuilder, BlockFilter> configure)
    {
        AppendFilter(FilterType.And, configure(new BlockFilterBuilder()));

        return this;
    }

    public RootFilter Or(Func<BlockFilterBuilder, BlockFilter> configure)
    {
        AppendFilter(FilterType.Or, configure(new BlockFilterBuilder()));

        return this;
    }

    public RootFilter WithTransform(Func<string, string> transform)
    {
        _transformFunction = transform;

        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();

        foreach (var filter in _filters)
        {
            builder.Append(filter.Build());
        }

        var compiledFilter = _transformFunction?.Invoke(builder.ToString()) ?? builder.ToString();

        if (string.IsNullOrEmpty(compiledFilter))
        {
            return string.Empty;
        }

        return compiledFilter;
    }
}