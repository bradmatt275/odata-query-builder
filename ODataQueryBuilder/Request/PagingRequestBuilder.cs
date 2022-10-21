using ODataQueryBuilder.Request.Models;

namespace ODataQueryBuilder.Request;

public class PagingRequestBuilder<TEntity>
{
    private readonly PagingRequestConfiguration _pagingConfig = new();
    private Func<IReadOnlyCollection<TEntity>, int, bool>? _pagingNextFunc;

    internal PagingRequestBuilder()  {}

    /// <summary>
    /// Set page size.
    /// </summary>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public PagingRequestBuilder<TEntity> WithPageSize(int pageSize)
    {
        _pagingConfig.PageSize = pageSize;

        return this;
    }

    /// <summary>
    /// Custom parameter names for the HTTP request query.
    /// </summary>
    /// <param name="top"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    public PagingRequestBuilder<TEntity> WithParameterNames(string top, string skip)
    {
        _pagingConfig.TopParameterName = top;
        _pagingConfig.SkipParameterName = skip;

        return this;
    }

    /// <summary>
    /// Custom rules to determine when the next page should be fetched.
    /// By default this will done when the result set is smaller than the page size.
    /// </summary>
    /// <param name="continueFunc"></param>
    /// <returns></returns>
    public PagingRequestBuilder<TEntity> NextPageWhen(Func<IReadOnlyCollection<TEntity>, int, bool> continueFunc)
    {
        _pagingNextFunc = continueFunc;

        return this;
    }

    internal void Build(RequestBuilderSettings<TEntity> settings)
    {
        settings.Paging = _pagingConfig;
        settings.PagingNextFunc = _pagingNextFunc;
    }
}