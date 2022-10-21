using ODataQueryBuilder.Filter.Filters.Shared;
using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters.Operators;

internal sealed class AndFilter : OperatorFilter
{
    internal AndFilter(IFilter filter) : base("and", filter)
    {

    }
}