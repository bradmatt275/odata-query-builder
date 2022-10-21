using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODataQueryBuilder.Filter;

namespace ODataQueryBuilder.Test;

[TestClass]
public class FilterTesting
{
    [TestMethod("Single Root Filter")]
    public void SingleFilter()
    {
        var filter = new ODataFilterBuilder()
            .Where("Something eq 'Test'")
            .Build();

        filter.Should().Be("Something eq 'Test'");
    }

    [TestMethod("Collection Filter With One Operators")]
    public void CollectionFilterSingleOperators()
    {
        var filter = new ODataFilterBuilder()
            .Where(filter => filter
                .Where("Property2 eq 'Test'"))
            .Build();

        filter.Should().Be("(Property2 eq 'Test')");
    }

    [TestMethod("Collection Filter With Multiple Operators")]
    public void CollectionFilterMultipleOperators()
    {
        var filter = new ODataFilterBuilder()
            .Where(filter => filter
                .Where("Property2 eq 'Test'")
                .Or("Property2 eq 'Test'")
                .And("Property3 eq 'Test'"))
            .Build();

        filter.Should()
            .Be("(Property2 eq 'Test' or Property2 eq 'Test' and Property3 eq 'Test')");
    }

    [TestMethod("Multiple Root Filters")]
    public void MultipleRootLevelFilters()
    {
        var filter = new ODataFilterBuilder()
            .Where(filter => filter
                .Where("Property2 eq 'Test'")
                .Or("Property2 eq 'Test'")
                .And("Property3 eq 'Test'"))
            .And(filter => filter
                .Where("Property4 eq 'Test'"))
            .Build();

        filter.Should()
            .Be("(Property2 eq 'Test' or Property2 eq 'Test' and Property3 eq 'Test') and (Property4 eq 'Test')");
    }

    [TestMethod("Filter With Transform")]
    public void FilterWithTransform()
    {
        var filter = new ODataFilterBuilder()
            .Where(filter => filter
                .Where("Property2 eq 'Test'")
                .Or("Property2 eq 'Test'")
                .And("Property3 eq 'Test'")
                .WithTransform(_ => string.Empty))
            .And(filter => filter
                .Where("Property4 eq 'Test'"))
            .Build();

        filter.Should().Be("(Property4 eq 'Test')");
    }
}