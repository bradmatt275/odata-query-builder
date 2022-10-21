using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters.Shared;

internal abstract class OperatorFilter : IFilter
{
    private readonly string _operatorType;
    private readonly IFilter _filter;

    protected OperatorFilter(string operatorType, IFilter filter)
    {
        _operatorType = operatorType;
        _filter = filter;
    }

    string IFilter.Build()
    {
        var compiledFilter = _filter.Build();

        return string.IsNullOrEmpty(compiledFilter) ? string.Empty : $" {_operatorType} {compiledFilter}";
    }
}