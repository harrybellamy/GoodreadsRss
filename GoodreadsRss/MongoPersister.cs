using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GoodreadsRss;

public class MongoPersister(string connectionString, string databaseName)
{
    private readonly string connectionString = connectionString;
    private readonly string databaseName = databaseName;

    static MongoPersister()
    {
        BsonClassMap.RegisterClassMap<UserStatusItem>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapProperty(c => c.BookAuthor);
            classMap.MapProperty(c => c.BookId);
            classMap.MapProperty(c => c.BookTitle);
            classMap.MapProperty(c => c.DonePercentage);
            classMap.MapProperty(c => c.Id);
            classMap.MapProperty(c => c.PublishDate);
        });
    }

    public void ListDbs()
    {
        MongoClient dbClient = new(connectionString);

        var dbList = dbClient.ListDatabases().ToList();

        Console.WriteLine("The list of databases on this server is: ");
        foreach (var db in dbList)
        {
            Console.WriteLine(db);
        }
    }

    public void SaveUserStatuses(IEnumerable<UserStatusItem> userStatuses)
    {
        MongoClient dbClient = new(connectionString);

        var db = dbClient.GetDatabase(databaseName);
        var collection = db.GetCollection<UserStatusItem>("userStatuses");
        collection.InsertMany(userStatuses);
    }
}
