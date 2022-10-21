namespace ODataQueryBuilder.Request.Models;

internal class PagingRequestConfiguration
{
    public int PageSize { get; set; }
    public string TopParameterName { get; set; } = "$top";
    public string SkipParameterName { get; set; } = "$skip";
}