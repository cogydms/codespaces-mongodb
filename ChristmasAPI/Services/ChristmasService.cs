using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ChristmasApi.Models;

namespace ChristmasApi.Services;

public class ChristmasService
{
    private readonly IMongoCollection<ChristmasMarket> MarketCollection;
    private readonly IMongoCollection<Locations> LocationsCollection;

    public ChristmasService(IOptions<ChristmasDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var db = client.GetDatabase(settings.Value.DatabaseName);
        MarketCollection = db.GetCollection<ChristmasMarket>(settings.Value.MarketsCollectionName);
        LocationsCollection = db.GetCollection<Locations>(settings.Value.LocationsCollectionName);
    }

    public async Task<List<ChristmasMarket>> GetAsync() =>
        await MarketCollection.Find(_ => true).ToListAsync();
    
    public async Task<ChristmasMarket> GetMarketByIdAsync(string id) =>
        await MarketCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task<Locations> GetLocationByIdAsync(string id) =>
        await LocationsCollection.Find(l => l.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ChristmasMarket c) =>
        await MarketCollection.InsertOneAsync(c);

    public async Task UpdateAsync(string id, ChristmasMarket c) =>
        await MarketCollection.ReplaceOneAsync(m => m.Id==id, c);

    public async Task RemoveAsync(string id) =>
        await MarketCollection.DeleteOneAsync(m => m.Id ==id);
        
    public async Task<List<ChristmasMarket>> SearchMarketAsync(string keyword) =>
        await MarketCollection.Find(Builders<ChristmasMarket>.Filter.Text(keyword)).ToListAsync();

    public async Task<List<ChristmasMarket>> GetMarketsByDateAsync(DateTime date)
    {
        return await MarketCollection.Find(m => m.StartDate <= date && m.EndDate >= date).ToListAsync();
    }

}

/*
API must expose at least 7 endpoints:

1. Get all documents from one collection
2. Get one document by id (two endpoints, one for each collection)
3. Get all documents by search (use the text index)
4. Add a document (in one collection)
5. Edit a document (in one collection)
6. Delete a document (in one collection)

*/