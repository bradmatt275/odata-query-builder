using ODataQueryBuilder.Filter.Filters;

namespace ODataQueryBuilder;

public sealed class ODataFilterBuilder
{
    public RootFilter Where(string expression)
    {
        return new RootFilter(new ExpressionFilter(expression));
    }

    public RootFilter Where(Func<BlockFilterBuilder, BlockFilter> configure)
    {
        return new RootFilter(configure(new BlockFilterBuilder()));
    }
}