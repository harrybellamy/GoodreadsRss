using System.ServiceModel.Syndication;
using GoodreadsRss;

namespace Goodreads.Tests.Unit;

public class ReviewItemTests
{
    private readonly string summaryText = """                 
        <a href="/book/show/316767.The_Box"><img align="right" hspace="10" alt="The Box by Marc Levinson" title="The Box by Marc Levinson" src="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1442129363l/316767._SY75_.jpg" /></a>
        Harry gave 5 stars to <a class="bookTitle" href="https://www.goodreads.com/book/show/316767.The_Box">The Box: How the Shipping Container Made the World Smaller and the World Economy Bigger (Hardcover)</a>
        <span class="by">by</span>
        <a class="authorName" href="https://www.goodreads.com/author/show/182171.Marc_Levinson">Marc Levinson</a>
        <br/>     
        """;
    private readonly string titleText = """
          
        Harry added 'The Box: How the Shipping Container Made the World Smaller and the World Economy Bigger'

        """;

    [Fact]
    public void CreateReturnsCorrectReviewItemTest()
    {
        var syndicationItem = new SyndicationItem
        {
            Id = "ReviewId",
            Summary = new TextSyndicationContent(summaryText),
            Title = new TextSyndicationContent(titleText),
            PublishDate = new DateTime(2024, 09, 01, 14, 50, 26, DateTimeKind.Utc),
        };

        var result = ReviewItem.Create(syndicationItem);

        DateTimeOffset expectedPublishDate = new DateTime(
            2024,
            09,
            01,
            14,
            50,
            26,
            DateTimeKind.Utc
        );
        var expected = new
        {
            BookTitle = "The Box",
            BookAuthor = "Marc Levinson",
            PublishDate = expectedPublishDate,
            Id = "ReviewId",
            Rating = 5,
        };
        Assert.Equivalent(expected, result);
    }
}
