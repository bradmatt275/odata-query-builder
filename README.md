
# odata-query-builder

Simple fluent dotnet OData query builder.

The goal of this project is to provide an easy way of building OData parameters in a clean way without manually building strings.

It is allows building queries for systems that don't necessarily conform to the OData standard. For example, Dynamics 365 OData endpoints.

## Installation

To install `OData.Fluent.QueryBuilder` from `Visual Studio`, find `OData.Fluent.QueryBuilder` in the `NuGet` package manager user interface or enter the following command in the package manager console:
```
Install-Package OData.Fluent.QueryBuilder
```

To add a link to the main dotnet project, run the following command line:
```
dotnet add package OData.Fluent.QueryBuilder
```
## OData Filter Builder

#### Simple filters

```csharp
var filter = new ODataFilterBuilder()
    .Where("Property1 eq 'Test'")
    .Build();
```
Output: `Property1 eq 'Test'`

#### Multiple filters

```csharp
var filter = new ODataFilterBuilder()
    .Where(filter => filter
        .Where("Property2 eq 'Test'")
        .Or("Property2 eq 'Test'")
        .And("Property3 eq 'Test'"))
    .And(filter => filter
        .Where("Property4 eq 'Test'"))
    .Build();
```
Output: `(Property2 eq 'Test' or Property2 eq 'Test' and Property3 eq 'Test') and (Property4 eq 'Test')`

#### Filter Transformations

Manually transform filter before outputting the results.

This is useful when you want to apply a filter conditionally.

```csharp
var filter = new ODataFilterBuilder()
    .Where(filter => filter
        .Where("Property2 eq 'Test'")
        .Or("Property2 eq 'Test'")
        .And("Property3 eq 'Test'")
        .WithTransform(_ => string.Empty))
    .And(filter => filter
        .Where("Property4 eq 'Test'"))
    .Build();
```

Output: `(Property4 eq 'Test')`

## OData Select Builder

```csharp
var results = new ODataSelectBuilder()
    .Property("Test1")
    .Property("Test2")
    .Build();
```

#### or

```csharp
var results = new ODataSelectBuilder()
    .Properties("Test1", "Test2")
    .Build();
```

Output: `Test1,Test2`

## OData Request Builder

Build a full request with support for paging.

```csharp
var oDataRequest = new ODataRequestBuilder<ExampleModel>()
    .WithFilter(config => config
        .Where("ProjectsPublished/ProjectStage eq Microsoft.Dynamics.DataEntities.ProjStatus'InProcess'")
        .Or(filter => filter
            .Where("ProjectsPublished/ProjectStage eq Microsoft.Dynamics.DataEntities.ProjStatus'Completed'")
            .And($"ProjectsPublished/ActualEndDate le {DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd")}"))
        .WithParameterName("filters")
        .WithTransform(filter => $"({filter})"))
    .WithPaging(config => config
        .WithPageSize(10)
        .WithParameterNames("limit", "offset")
        .NextPageWhen((results, _) => results.Count > 0));

var results = await oDataRequest.ExecuteAsync(async request =>
{
    var response = await client.GetAsync($"api/v1/data?{request.Query}");
    var data = await response.Content.ReadFromJsonAsync<IEnumerable<ExampleModel>>();

    return data ?? Enumerable.Empty<ExampleModel>();
});
```

Results will be combined and returned a single collection.
