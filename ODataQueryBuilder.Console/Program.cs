using ODataQueryBuilder;

var data = Enumerable.Range(0, 3000)
    .Select(num => num.ToString())
    .ToArray();

var results = await new ODataRequestBuilder<string>()
    .WithFilter(filter => filter
        .Where("Something")
        .Or(x => x.Where(""))
        .WithTransform(filter => $"{filter}")
        .WithParameterName("filter"))
    .WithPaging(config => config
        .WithPageSize(10)
        .WithParameterNames("top", "skip")
        .NextPageWhen((results, _) => results.Count > 0))
    .WithSelect(config => config
        .Property("test")
        .WithParameterName("select"))
    .ExecuteAsync(request =>
    {
        request.AddUpdateParameter("company", "nrw");

        return Task.FromResult(data.Skip(request.Skip).Take(request.Top));
    });

Console.WriteLine("Hello, World!");
