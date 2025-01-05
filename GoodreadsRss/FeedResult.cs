using System.ServiceModel.Syndication;

namespace GoodreadsRss;

public class FeedResult(IEnumerable<SyndicationItem> items)
{
    private IEnumerable<SyndicationItem> Items { get; } = items;
    public IEnumerable<UserStatusItem> UserStatuses =>
        Items.Where(w => UserStatusItem.IsUserStatusItem(w)).Select(s => UserStatusItem.Create(s));
    public IEnumerable<ReadStatusItem> ReadStatuses =>
        Items.Where(w => ReadStatusItem.IsReadStatusItem(w)).Select(s => ReadStatusItem.Create(s));
    public IEnumerable<ReviewItem> Reviews =>
        Items.Where(w => ReviewItem.IsReviewItem(w)).Select(s => ReviewItem.Create(s));
}
