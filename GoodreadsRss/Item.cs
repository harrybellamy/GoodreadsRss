using System.ServiceModel.Syndication;

namespace GoodreadsRss;

/// <summary>
/// Abstract base class for all items received from the GoodReads feed.
/// </summary>
public abstract class Item
{
    /// <summary>
    /// Gets the book's title and author from the supplied <see cref="SyndicationItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The syndication item.</param>
    /// <returns>The book's title and author.</returns>
    protected internal static (string bookTitle, string bookAuthor) GetBookTitleAndAuthor(
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

    /// <summary>
    /// Gets the book's id from the supplied <see cref="SyndicationItem"/>.
    /// </summary>
    /// <param name="syndicationItem">The syndication item.</param>
    /// <returns>The book's id.</returns>
    protected internal static string GetBookId(SyndicationItem syndicationItem)
    {
        var firstIndex = syndicationItem.Summary.Text.IndexOf('"');
        var secondIndex = syndicationItem.Summary.Text.IndexOf('"', firstIndex + 1);

        return syndicationItem
            .Summary.Text[(firstIndex + 1)..secondIndex]
            .Replace("/book/show/", "");
    }
}
