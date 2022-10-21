using System.Web;
using ODataQueryBuilder.Request;
using ODataQueryBuilder.Request.Models;

namespace ODataQueryBuilder;

public class ODataRequestBuilder<TEntity>
{
    private readonly RequestBuilderSettings<TEntity> _settings = new();

    public ODataRequestBuilder<TEntity> WithFilter(Func<FilterRequestBuilder, RequestFilter> configure)
    {
        _settings.Filter = configure(new FilterRequestBuilder()).Build();

        return this;
    }

    public ODataRequestBuilder<TEntity> WithPaging(Action<PagingRequestBuilder<TEntity>> configure)
    {
        var builder = new PagingRequestBuilder<TEntity>();

        configure(builder);

        builder.Build(_settings);

        return this;
    }

    public ODataRequestBuilder<TEntity> WithSelect(Action<SelectRequestBuilder> configure)
    {
        var builder = new SelectRequestBuilder();

        configure(builder);

        builder.Build(_settings);

        return this;
    }

    public async Task<ICollection<TEntity>> ExecuteAsync(Func<ODataRequest, Task<IEnumerable<TEntity>>> callback)
    {
        var entities = new List<TEntity>();

        if (_settings.Paging is not { PageSize: > 0 })
        {
            entities.AddRange(await callback(new ODataRequest(_settings)));

            return entities;
        }

        var tempResults = new List<TEntity>();

        bool ShouldContinue() => _settings.PagingNextFunc?.Invoke(tempResults, _settings.Paging.PageSize)
            ?? tempResults.Count >= _settings.Paging.PageSize;

        do
        {
            tempResults.Clear();

            var request = new ODataRequest(_settings)
            {
                Skip = entities.Count,
                Top = _settings.Paging.PageSize
            };

            tempResults.AddRange(await callback(request));

            entities.AddRange(tempResults);
        }
        while (ShouldContinue());

        return entities;
    }
}