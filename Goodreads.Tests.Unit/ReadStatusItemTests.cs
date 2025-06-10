using System.ServiceModel.Syndication;
using GoodreadsRss;

namespace Goodreads.Tests.Unit;

public class ReadStatusItemTests
{
    private readonly string summaryText = """                   
        <a href="/book/show/62192505-the-last-action-heroes"><img align="right" hspace="10" alt="The Last Action Heroes by Nick de Semlyen" title="The Last Action Heroes by Nick de Semlyen" src="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1684818116l/62192505._SY75_.jpg" /></a>
        Harry is currently reading <a only_path="false" class="bookTitle" href="/book/show/62192505-the-last-action-heroes">The Last Action Heroes: The Triumphs, Flops, and Feuds of Hollywood&#39;s Kings of Carnage</a>
        <span class="by">by</span>
        <a only_path="false" class="authorName" href="/author/show/18636135.Nick_de_Semlyen">Nick de Semlyen</a>
        <br/>
        """;

    [Theory]
    [InlineData("blah", false)]
    [InlineData("ReadStatusId", true)]
    public void IsReadStatusTest(string id, bool expectedResult)
    {
        var syndicationItem = new SyndicationItem { Id = id };

        var result = ReadStatusItem.IsReadStatusItem(syndicationItem);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void CreateReturnsCorrectReadStatusTest()
    {
        var syndicationItem = new SyndicationItem
        {
            Id = "ReadStatusId",
            Summary = new TextSyndicationContent(summaryText),
            PublishDate = new DateTime(2024, 09, 01, 14, 50, 26, DateTimeKind.Utc),
        };

        var result = ReadStatusItem.Create(syndicationItem);

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
            BookTitle = "The Last Action Heroes",
            BookAuthor = "Nick de Semlyen",
            PublishDate = expectedPublishDate,
            Id = "ReadStatusId",
        };
        Assert.Equivalent(expected, result);
    }
}
