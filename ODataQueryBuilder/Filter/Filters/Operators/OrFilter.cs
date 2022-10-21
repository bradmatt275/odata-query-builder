using ODataQueryBuilder.Filter.Filters.Shared;
using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters.Operators;

internal sealed class OrFilter : OperatorFilter
{
    public OrFilter(IFilter filter) : base("or", filter)
    {
       
    }
}