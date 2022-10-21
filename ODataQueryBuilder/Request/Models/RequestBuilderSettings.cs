namespace ODataQueryBuilder.Request.Models;

internal sealed class RequestBuilderSettings<TEntity> : RequestSettings
{
    public Func<IReadOnlyCollection<TEntity>, int, bool>? PagingNextFunc { get; set; }
}