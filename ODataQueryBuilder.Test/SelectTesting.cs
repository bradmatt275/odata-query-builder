using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ODataQueryBuilder.Test;

[TestClass]
public class SelectTesting
{
    [TestMethod("Select With Single Property")]
    public void SingleProperty()
    {
        var results = new ODataSelectBuilder()
            .Property("Test1")
            .Build();

        results.Should().Be("Test1");
    }

    [TestMethod("Select With Multiple Properties Input Type Individual")]
    public void MultiplePropertyInput1()
    {
        var results = new ODataSelectBuilder()
            .Property("Test1")
            .Property("Test2")
            .Build();

        results.Should().Be("Test1,Test2");
    }

    [TestMethod("Select With Multiple Properties Input Type Bulk")]
    public void MultiplePropertyInput2()
    {
        var results = new ODataSelectBuilder()
            .Properties("Test1", "Test2")
            .Build();

        results.Should().Be("Test1,Test2");
    }
}