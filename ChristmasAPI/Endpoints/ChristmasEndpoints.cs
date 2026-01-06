using ChristmasApi.Models;
using ChristmasApi.Services;

namespace ChristmasApi.Endpoint;

public static class ChristmasEndpoint
{
    private static string route = "/christmas";

    public static IEndpointRouteBuilder MapChristmasMarket(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(route);

        group.MapGet("/", async(ChristmasService service) =>
        {
            var list = await service.GetAsync();
            return Results.Ok(list);
        });

        group.MapGet("/market/{id}", async(ChristmasService service, string id) =>
        {
            var market = await service.GetMarketByIdAsync(id);
            return Results.Ok(market);
        });

        group.MapGet("/location/{id}", async(ChristmasService service, string id) =>
        {
            var location = await service.GetLocationByIdAsync(id);
            return Results.Ok(location);
        });

        group.MapPost("/", async(ChristmasService service, ChristmasMarket c) =>
        {
            await service.CreateAsync(c);
            return Results.Created($"{route}/{c.Id}", c);
        });

        group.MapPut("/{id}", async(ChristmasService service, string id, ChristmasMarket c) =>
        {
            var marketUpdate = await service.GetMarketByIdAsync(id);

            if (marketUpdate is null)
                return Results.NotFound();
            c.Id = id;

            await service.UpdateAsync(id, c);
            return Results.NoContent();
        });

        group.MapDelete("/", async(ChristmasService service, string id) =>
        {
            var market = await service.GetMarketByIdAsync(id);

            if (market is null)
                return Results.NotFound();

            await service.RemoveAsync(id);
            return Results.NoContent();
        });

        group.MapGet("/search", async(ChristmasService service, string keyword) =>
        {
            var marketSearch = await service.SearchMarketAsync(keyword);
            return Results.Ok(marketSearch);
        });


        return routes;
    }
}