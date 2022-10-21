using ODataQueryBuilder.Filter.Filters;

namespace ODataQueryBuilder.Request;

public sealed class FilterRequestBuilder
{
    internal FilterRequestBuilder()
    {

    }

    public RequestFilter Where(string expression)
    {
        return new RequestFilter(new ExpressionFilter(expression));
    }

    public RequestFilter Where(Func<BlockFilterBuilder, BlockFilter> configure)
    {
        return new RequestFilter(configure(new BlockFilterBuilder()));
    }
}