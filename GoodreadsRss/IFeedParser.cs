namespace GoodreadsRss;

/// <summary>
/// Interface for feed parsers.
/// </summary>
public interface IFeedParser
{
    /// <summary>
    /// Parses the feed at the <paramref name="feedUrl"/> supplied
    /// and returns the parsed <see cref="FeedResult"/>.
    /// </summary>
    /// <param name="feedUrl">The URL of the feed to parse.</param>
    /// <returns>The parsed <see cref="FeedResult"/>.</returns>
    FeedResult Parse(string feedUrl);
}