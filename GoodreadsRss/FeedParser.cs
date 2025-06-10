namespace GoodreadsRss;

using System.ServiceModel.Syndication;
using System.Xml;

/// <summary>
/// Class that does the feed parsing to produce a <see cref="FeedResult"/>
/// </summary>
public class FeedParser : IFeedParser
{
    /// <summary>
    /// Makes a call to the feed at the <paramref name="feedUrl"/> supplied
    /// and returns the parsed <see cref="FeedResult"/>.
    /// </summary>
    /// <param name="feedUrl">The URL of the feed to parse.</param>
    /// <returns>The parsed <see cref="FeedResult"/>.</returns>
    public FeedResult Parse(string feedUrl)
    {
        var reader = XmlReader.Create(feedUrl);
        var feed = SyndicationFeed.Load(reader);
        reader.Close();
        return new FeedResult(feed.Items);
    }
}
