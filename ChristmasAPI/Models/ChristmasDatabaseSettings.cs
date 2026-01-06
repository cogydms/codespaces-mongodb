namespace ChristmasApi.Models;

public class ChristmasDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string MarketsCollectionName { get; set; } = null!;
    public string LocationsCollectionName { get; set; } = null!;
}
