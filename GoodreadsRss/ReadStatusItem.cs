using System.ServiceModel.Syndication;

namespace GoodreadsRss;

public class ReadStatusItem : Item
{
    private ReadStatusItem(string bookTitle, string bookAuthor, DateTimeOffset publishDate, string id)
    {
        BookTitle = bookTitle;
        PublishDate = publishDate;
        BookAuthor = bookAuthor;
        Id = id;
    }

    public string BookTitle { get; }
    public string BookAuthor { get; }
    public DateTimeOffset PublishDate { get; }
    public string Id { get; }

    public static bool IsReadStatusItem(SyndicationItem syndicationItem) 
        => syndicationItem.Id.StartsWith("ReadStatus");

    public static ReadStatusItem Create(SyndicationItem syndicationItem)
    {
        var (bookTitle, bookAuthor) = GetBookTitleAndAuthor(syndicationItem);
        return new ReadStatusItem(
            bookTitle,
            bookAuthor,
            syndicationItem.PublishDate, 
            syndicationItem.Id);
    }
}
