using System.ServiceModel.Syndication;

namespace GoodreadsRss;

public class ReviewItem(
    string bookTitle,
    string bookAuthor,
    string bookId,
    DateTimeOffset publishDate,
    string id,
    int rating
) : Item
{
    public string BookTitle { get; } = bookTitle;

    public string BookAuthor { get; } = bookAuthor;
    public string BookId { get; } = bookId;
    public DateTimeOffset PublishDate { get; } = publishDate;
    public string Id { get; } = id;
    public int Rating { get; } = rating;

    public static bool IsReviewItem(SyndicationItem syndicationItem) =>
        syndicationItem.Id.StartsWith("Review");

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
