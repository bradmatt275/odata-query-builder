using System.Web;

namespace ODataQueryBuilder.Request.Models;

public class ODataRequest
{
    private readonly RequestSettings _settings;
    private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>();

    internal ODataRequest(RequestSettings settings)
    {
        _settings = settings;

        UpdateQuery();
    }

    public string? Filter { get; private set; }
    public int Top { get; set; }
    public int Skip { get; set; }
    public string? Select { get; private set; }
    public string? Query { get; private set; }

    public void AddUpdateParameter(string key, string? value)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        if (string.IsNullOrEmpty(value))
        {
            RemoveParameter(key);

            return;
        }

        if (_parameters.ContainsKey(key))
        {
            _parameters.Add(key, value);
        }
        else
        {
            _parameters[key] = value;
        }

        UpdateQuery();
    }

    public void RemoveParameter(string key)
    {
        if (!_parameters.ContainsKey(key))
        {
            return;
        }

        _parameters.Remove(key);

        UpdateQuery();
    }

    private void UpdateQuery()
    {
        var queryBuilder = HttpUtility.ParseQueryString(string.Empty);

        foreach (var parameter in _parameters)
        {
            queryBuilder[parameter.Key] = parameter.Value;
        }

        if (_settings.Filter is { Value: { } })
        {
            Filter = _settings.Filter.Value;

            queryBuilder[_settings.Filter.ParameterName] = Filter;
        }

        if (_settings.Select is { Value: { } })
        {
            Select = _settings.Select.Value;

            queryBuilder[_settings.Select.ParameterName] = Select;
        }

        if (_settings.Paging is { PageSize: > 0 })
        {
            queryBuilder[_settings.Paging.TopParameterName] = Top.ToString();
            queryBuilder[_settings.Paging.SkipParameterName] = Skip.ToString();
        }

        Query = queryBuilder.ToString();
    }
}