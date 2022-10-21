using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters;

internal class ExpressionFilter : IFilter
{
    private readonly string _expression;

    internal ExpressionFilter(string expression)
    {
        _expression = expression;
    }

    string IFilter.Build()
    {
        return _expression;
    }
}