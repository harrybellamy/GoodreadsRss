using GoodreadsRss;

namespace Goodreads.Tests.Unit
{
    public class UnitTest1
    {
        [Fact(Skip = "Mongo dependencies should be removed.")]
        public void Test1()
        {
            var feedParser = new FeedParser();
            var parseResult = feedParser.Parse("https://www.goodreads.com/user/updates_rss/109940637");

            var userStatuses = parseResult.UserStatuses.ToArray();
            var mongoPersister = new MongoPersister("", "");
            mongoPersister.SaveUserStatuses(userStatuses);
        }

        [Fact(Skip = "Mongo dependencies should be removed.")]
        public void ListDbsTest()
        { 
            var mongoPersister = new MongoPersister("", "");
            mongoPersister.ListDbs();
        }
    }
}