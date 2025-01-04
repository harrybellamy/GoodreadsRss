using GoodreadsRss;
using System.ServiceModel.Syndication;

namespace Goodreads.Tests.Unit;

public class UserStatusItemTests
{
    private readonly string summaryText = """
                <a href="/book/show/57693406-the-kaiju-preservation-society"><img align="right" hspace="10" alt="The Kaiju Preservation Society by John Scalzi" title="The Kaiju Preservation Society by John Scalzi" src="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1624897234l/57693406._SY75_.jpg" /></a>
        <a href="/user/show/109940637-harry">Harry</a>
        is 30% done with &lt;a href=&quot;/book/show/57693406-the-kaiju-preservation-society&quot;&gt;The Kaiju Preservation Society&lt;/a&gt;.
        """;
    private readonly string titleText = """
                Harry
        is 51 % done with The Kaiju Preservation So
        """;

    [Theory]
    [InlineData("UserStatusId", true)]
    [InlineData("ReadStatusId", false)]
    public void IsUserStatusTest(string id, bool expectedResult)
    {
        var syndicationItem = new SyndicationItem
        {
            Id = id
        };

        var result = UserStatusItem.IsUserStatusItem(syndicationItem);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void CreateReturnsCorrectUserStatusTest()
    {
        var syndicationItem = new SyndicationItem
        {
            Id = "ReadStatusId",
            Summary = new TextSyndicationContent(summaryText),
            PublishDate = new DateTime(2024, 09, 01, 14, 50, 26, DateTimeKind.Utc),
            Title = new TextSyndicationContent(titleText),   
        };

        var result = UserStatusItem.Create(syndicationItem);

        DateTimeOffset expectedPublishDate = new DateTime(2024, 09, 01, 14, 50, 26, DateTimeKind.Utc);
        var expected = new
        {
            BookTitle = "The Kaiju Preservation Society",
            BookAuthor = "John Scalzi",
            PublishDate = expectedPublishDate,
            BookId = "57693406-the-kaiju-preservation-society",
            Id = "ReadStatusId",
            DonePercentage = 51
        };

        Assert.Equivalent(expected, result);
    }
}
