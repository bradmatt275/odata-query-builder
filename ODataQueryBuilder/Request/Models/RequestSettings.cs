namespace ODataQueryBuilder.Request.Models;

internal class RequestSettings
{
    internal RequestConfiguration? Select { get; set; }
    internal PagingRequestConfiguration? Paging { get; set; }
    internal RequestConfiguration? Filter { get; set; }
}