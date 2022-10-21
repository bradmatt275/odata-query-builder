using ODataQueryBuilder.Filter.Filters.Operators;
using ODataQueryBuilder.Filter.Interfaces;

namespace ODataQueryBuilder.Filter.Filters.Shared;

public abstract class CollectionFilter
{
    protected enum FilterType
    {
        And,
        Or
    }

    protected readonly List<IFilter> _filters = new();

    protected CollectionFilter(IFilter filter)
    {
        _filters.Add(filter);
    }

    protected void AppendFilter(FilterType filterType, IFilter filter)
    {
        var previousFilter = _filters.LastOrDefault()?.Build();

        if (string.IsNullOrEmpty(previousFilter))
        {
            _filters.Add(filter);

            return;
        }

        switch (filterType)
        {
            case FilterType.And:
                _filters.Add(new AndFilter(filter));
                return;

            case FilterType.Or:
                _filters.Add(new OrFilter(filter));
                return;
        }
    }
}