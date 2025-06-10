using System.ServiceModel.Syndication;

namespace GoodreadsRss;

/// <summary>
/// Abstract base class for all items received from the GoodReads feed.
/// </summary>
public abstract class Item
{
    protected static (string bookTitle, string bookAuthor) GetBookTitleAndAuthor(
        SyndicationItem syndicationItem
    )
    {
        const string altTagStartStr = @"alt=""";
        var altTagStart = syndicationItem.Summary.Text.IndexOf(altTagStartStr);
        var altTagEnd = syndicationItem.Summary.Text.IndexOf(
            '"',
            altTagStart + altTagStartStr.Length
        );

        var bookTitleAndAuthor = syndicationItem.Summary.Text[
            (altTagStart + altTagStartStr.Length)..altTagEnd
        ];

        var split = bookTitleAndAuthor.Split(" by ");
        return (split[0], split[1]);
    }

    protected static string GetBookId(SyndicationItem syndicationItem)
    {
        var firstIndex = syndicationItem.Summary.Text.IndexOf('"');
        var secondIndex = syndicationItem.Summary.Text.IndexOf('"', firstIndex + 1);

        return syndicationItem
            .Summary.Text[(firstIndex + 1)..secondIndex]
            .Replace("/book/show/", "");
    }
}
