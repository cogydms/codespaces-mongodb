using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChristmasApi.Models;

public class ChristmasMarket
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string Location_Id { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class Locations
{
    [BsonId] 
    public string Id { get; set; }

    public string City { get; set; }
    public string Country { get; set; }
    public string Address { get; set; }
    public string GoogleMapUrl { get; set; }
}