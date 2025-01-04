namespace GoodreadsRss;

using System.ServiceModel.Syndication;
using System.Xml;

public class FeedParser
{
    public FeedResult Parse(string feedUrl)
    {
        var reader = XmlReader.Create(feedUrl);
        var feed = SyndicationFeed.Load(reader);
        reader.Close();
        return new FeedResult(feed.Items);
    }
}
