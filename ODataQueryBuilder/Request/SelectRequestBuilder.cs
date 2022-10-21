using ODataQueryBuilder.Request.Models;

namespace ODataQueryBuilder.Request;

public class SelectRequestBuilder
{
    private readonly List<string> _properties = new();
    private readonly RequestConfiguration _configuration = new()
    {
        ParameterName = "$select"
    };

    internal SelectRequestBuilder()
    {

    }

    public SelectRequestBuilder Properties(params string[] properties)
    {
        _properties.AddRange(properties);

        return this;
    }

    public SelectRequestBuilder Property(string property)
    {
        _properties.Add(property);

        return this;
    }

    public SelectRequestBuilder WithParameterName(string parameterName)
    {
        _configuration.ParameterName = parameterName;

        return this;
    }

    internal void Build(RequestSettings settings)
    {
        _configuration.Value = string.Join(",", _properties);

        settings.Select = _configuration;
    }
}