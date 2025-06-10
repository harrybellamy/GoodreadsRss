using System.ServiceModel.Syndication;

namespace GoodreadsRss;

/// <summary>
/// Represents a user status update regarding a book.
/// </summary>
public class UserStatusItem : Item
{
    private UserStatusItem(
        int donePercentage,
        string bookTitle,
        string bookAuthor,
        string bookId,
        DateTimeOffset publishDate,
        string id
    )
    {
        DonePercentage = donePercentage;
        BookTitle = bookTitle;
        BookAuthor = bookAuthor;
        BookId = bookId;
        PublishDate = publishDate;
        Id = id;
    }

    public int DonePercentage { get; }

    /// <summary>
    /// The title of the book.
    /// </summary>
    public string BookTitle { get; }

    /// <summary>
    /// The author of the book.
    /// </summary>
    public string BookAuthor { get; }

    /// <summary>
    /// The id of the book.
    /// </summary>
    public string BookId { get; }

    /// <summary>
    /// The date this item was published.
    /// </summary>
    public DateTimeOffset PublishDate { get; }

    /// <summary>
    /// The unique id of this item.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Returns a value indictating whether a <see cref="SyndicationItem"/>
    /// is a <see cref="UserStatusItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The <see cref="SyndicationItem"/>.</param>
    /// <returns>True if the <see cref="SyndicationItem"/> is a <see cref="UserStatusItem"/>,
    /// false otherwise.</returns>
    public static bool IsUserStatusItem(SyndicationItem syndicationItem) =>
        syndicationItem.Id.StartsWith("UserStatus");

    /// <summary>
    /// Creates a new <see cref="UserStatusItem"/> from a <see cref="SyndicationItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The <see cref="SyndicationItem"/>.</param>
    /// <returns>The new <see cref="UserStatusItem"/>.</returns>
    public static UserStatusItem Create(SyndicationItem syndicationItem)
    {
        var donePercentage = GetDonePercentage(syndicationItem);
        var (bookTitle, bookAuthor) = GetBookTitleAndAuthor(syndicationItem);
        var bookId = GetBookId(syndicationItem);
        return new UserStatusItem(
            donePercentage,
            bookTitle,
            bookAuthor,
            bookId,
            syndicationItem.PublishDate,
            syndicationItem.Id
        );
    }

    private static int GetDonePercentage(SyndicationItem syndicationItem)
    {
        var percentageStr = syndicationItem.Title.Text.Split('%')[0].Trim().Split(' ').Last();
        return int.Parse(percentageStr);
    }
}
