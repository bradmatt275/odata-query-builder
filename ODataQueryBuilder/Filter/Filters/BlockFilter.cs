using System.Text;
using ODataQueryBuilder.Filter.Filters.Shared;
using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters;

public sealed class BlockFilterBuilder
{
    internal BlockFilterBuilder() { }

    public BlockFilter Where(string expression)
    {
        return new BlockFilter(new ExpressionFilter(expression));
    }
}

public sealed class BlockFilter : CollectionFilter, IFilter
{
    private Func<string, string>? _transformFunction;

    internal BlockFilter(IFilter filter) : base(filter)
    {

    }

    public BlockFilter And(string expression)
    {
        AppendFilter(FilterType.And, new ExpressionFilter(expression));

        return this;
    }

    public BlockFilter Or(string expression)
    {
        AppendFilter(FilterType.Or, new ExpressionFilter(expression));

        return this;
    }

    public BlockFilter WithTransform(Func<string, string> transform)
    {
        _transformFunction = transform;

        return this;
    }

    string IFilter.Build()
    {
        var builder = new StringBuilder();

        foreach (var record in _filters)
        {
            builder.Append(record.Build());
        }

        var compiledFilter = _transformFunction?.Invoke(builder.ToString()) ?? builder.ToString();

        if (string.IsNullOrEmpty(compiledFilter))
        {
            return string.Empty;
        }

        return $"({compiledFilter})";
    }
}