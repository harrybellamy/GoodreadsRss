# Goodreads RSS

[![.NET](https://github.com/harrybellamy/GoodreadsRss/actions/workflows/dotnet.yml/badge.svg)](https://github.com/harrybellamy/GoodreadsRss/actions/workflows/dotnet.yml)

## Usage

1. Create an instance of [FeedParser](GoodreadsRss/FeedParser.cs).
1. Call `FeedParser.Parse()`, passing in the URL of the feed you wish to parse.

Example:
```
using GoodreadsRss;
...

var feedParser = new FeedParser();
var feedUrl = "https://my-feed";
var feedResult = feedParser.parse(feedUrl);
```

Through [FeedResult](GoodreadsRss/FeedResult.cs) the various different types of updates can be accessed.