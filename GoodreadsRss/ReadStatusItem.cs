using System.ServiceModel.Syndication;

namespace GoodreadsRss;

/// <summary>
/// Represents a read status update regarding a book.
/// </summary>
public class ReadStatusItem : Item
{
    private ReadStatusItem(
        string bookTitle,
        string bookAuthor,
        DateTimeOffset publishDate,
        string id
    )
    {
        BookTitle = bookTitle;
        PublishDate = publishDate;
        BookAuthor = bookAuthor;
        Id = id;
    }

    /// <summary>
    /// The title of the book.
    /// </summary>
    public string BookTitle { get; }

    /// <summary>
    /// The author of the book.
    /// </summary>
    public string BookAuthor { get; }

    /// <summary>
    /// The publish date of this update.
    /// </summary>
    public DateTimeOffset PublishDate { get; }

    /// <summary>
    /// The unique identifier of this item.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Returns a value indictating whether a <see cref="SyndicationItem"/>
    /// is a <see cref="ReadStatusItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The <see cref="SyndicationItem"/>.</param>
    /// <returns>True if the <see cref="SyndicationItem"/> is a <see cref="ReadStatusItem"/>,
    /// false otherwise.</returns>
    public static bool IsReadStatusItem(SyndicationItem syndicationItem) =>
        syndicationItem.Id.StartsWith("ReadStatus");

    /// <summary>
    /// Creates a new <see cref="ReadStatusItem"/> from a <see cref="SyndicationItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The <see cref="SyndicationItem"/>.</param>
    /// <returns>The new <see cref="ReadStatusItem"/>.</returns>
    public static ReadStatusItem Create(SyndicationItem syndicationItem)
    {
        var (bookTitle, bookAuthor) = GetBookTitleAndAuthor(syndicationItem);
        return new ReadStatusItem(
            bookTitle,
            bookAuthor,
            syndicationItem.PublishDate,
            syndicationItem.Id
        );
    }
}
