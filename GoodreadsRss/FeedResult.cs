using System.ServiceModel.Syndication;

namespace GoodreadsRss;

/// <summary>
/// Represents the result returned from a call to the GoodReads feed.
/// </summary>
/// <param name="items">The <see cref="SyndicationItem"/>s in the feed.</param>
public class FeedResult(IEnumerable<SyndicationItem> items)
{
    private IEnumerable<SyndicationItem> Items { get; } = items;

    /// <summary>
    /// Gets the <see cref="UserStatusItem"/>s present in the feed.
    /// </summary>
    public IEnumerable<UserStatusItem> UserStatuses =>
        Items.Where(w => UserStatusItem.IsUserStatusItem(w)).Select(s => UserStatusItem.Create(s));

    /// <summary>
    /// Gets the <see cref="ReadStatusItem"/>s present in the feed.
    /// </summary>
    public IEnumerable<ReadStatusItem> ReadStatuses =>
        Items.Where(w => ReadStatusItem.IsReadStatusItem(w)).Select(s => ReadStatusItem.Create(s));

    /// <summary>
    /// Gets the <see cref="ReviewItem"/>s present in the feed.
    /// </summary>
    public IEnumerable<ReviewItem> Reviews =>
        Items.Where(w => ReviewItem.IsReviewItem(w)).Select(s => ReviewItem.Create(s));
}
