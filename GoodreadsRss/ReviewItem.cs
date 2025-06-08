using System.ServiceModel.Syndication;

namespace GoodreadsRss;

/// <summary>
/// Represents a review regarding a book.
/// </summary>
/// <param name="bookTitle">The book title.</param>
/// <param name="bookAuthor">The book author.</param>
/// <param name="bookId">The book id.</param>
/// <param name="publishDate">The publish date of the item.</param>
/// <param name="id">The unique identifier of this update.</param>
/// <param name="rating">The rating for the book given in the review.</param>
public class ReviewItem(
    string bookTitle,
    string bookAuthor,
    string bookId,
    DateTimeOffset publishDate,
    string id,
    int rating
) : Item
{
    /// <summary>
    /// The book title.
    /// </summary>
    public string BookTitle { get; } = bookTitle;
    /// <summary>
    /// The book author.
    /// </summary>
    public string BookAuthor { get; } = bookAuthor;
    /// <summary>
    /// The book id.,
    /// </summary>
    public string BookId { get; } = bookId;
    /// <summary>
    /// The publish date of the item.
    /// </summary>
    public DateTimeOffset PublishDate { get; } = publishDate;
    /// <summary>
    /// THe unique identifier for this update.
    /// </summary>
    public string Id { get; } = id;
    /// <summary>
    /// The rating for the book given in the review.
    /// </summary>
    public int Rating { get; } = rating;

    /// <summary>
    /// Returns a value indictating whether a <see cref="SyndicationItem"/>
    /// is a <see cref="ReviewItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The <see cref="SyndicationItem"/>.</param>
    /// <returns>True if the <see cref="SyndicationItem"/> is a <see cref="ReviewItem"/>,
    /// false otherwise.</returns>
    public static bool IsReviewItem(SyndicationItem syndicationItem) =>
        syndicationItem.Id.StartsWith("Review");

    /// <summary>
    /// Creates a new <see cref="ReviewItem"/> from a <see cref="SyndicationItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The <see cref="SyndicationItem"/>.</param>
    /// <returns>The new <see cref="ReviewItem"/>.</returns>
    public static ReviewItem Create(SyndicationItem syndicationItem)
    {
        var rating = GetRating(syndicationItem);
        var (bookTitle, bookAuthor) = GetBookTitleAndAuthor(syndicationItem);
        var bookId = GetBookId(syndicationItem);
        return new ReviewItem(
            bookTitle,
            bookAuthor,
            bookId,
            syndicationItem.PublishDate,
            syndicationItem.Id,
            rating
        );
    }

    private static int GetRating(SyndicationItem syndicationItem)
    {
        var ratingIndex = syndicationItem.Summary.Text.IndexOf(" stars to <a") - 1;
        return int.Parse("" + syndicationItem.Summary.Text[ratingIndex]);
    }
}
