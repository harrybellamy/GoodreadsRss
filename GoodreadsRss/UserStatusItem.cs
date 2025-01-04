using System.ServiceModel.Syndication;

namespace GoodreadsRss;

public class UserStatusItem : Item
{
    private UserStatusItem(
        int donePercentage,
        string bookTitle,
        string bookAuthor,
        string bookId,
        DateTimeOffset publishDate,
        string id)
    {
        DonePercentage = donePercentage;
        BookTitle = bookTitle;
        BookAuthor = bookAuthor;
        BookId = bookId;
        PublishDate = publishDate;
        Id = id;
    }

    public int DonePercentage { get; }
    public string BookTitle { get; }
    public string BookAuthor { get; }
    public string BookId { get; }
    public DateTimeOffset PublishDate { get; }
    public string Id { get; }

    public static bool IsUserStatusItem(SyndicationItem syndicationItem) => syndicationItem.Id.StartsWith("UserStatus");

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
            syndicationItem.Id);
    }

    private static int GetDonePercentage(SyndicationItem syndicationItem)
    {
        var percentageStr = syndicationItem.Title.Text.Split('%')[0].Trim().Split(' ').Last();
        return int.Parse(percentageStr);
    }
}
