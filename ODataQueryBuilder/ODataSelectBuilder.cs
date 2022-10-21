using ODataQueryBuilder.Request.Models;

namespace ODataQueryBuilder;

public class ODataSelectBuilder
{
    private readonly List<string> _properties = new();

    public ODataSelectBuilder Properties(params string[] properties)
    {
        _properties.AddRange(properties);

        return this;
    }

    public ODataSelectBuilder Property(string property)
    {
        _properties.Add(property);

        return this;
    }

    public string Build()
    {
        return string.Join(",", _properties);
    }
}