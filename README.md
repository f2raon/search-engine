# search-engine
Search Engine project

The main aim of this service is to search user's query from 3 search engines such as Yandex, Google and Bing.
And then fetch data wchich comes first, store data in DB and show the results to user.

For this, I have integrated with 3 REST API services, Google Custom Search API, Yandex.XML and Bing Custom Search.
Thhe UI and UX may be not good (for me it is not good), but I'm not good at front-end..

There only 2 pages:
1.{domain}\Home\Index -> there user can search from these web services and get the results.
2.{domain}\Home\Filter -> there user can filter his search history which are stored in DB.

Technology stack
1.Language -> C#
2.Software Framework -> .NET Framework 4.7.2
3.Web App Framework -> ASP.NET Web API
4.ORM -> EntityFramework
5.For DB I'm used Code First approach, hence there are not any sql scripts...
6.local DB MS SQL run in MS SQL 2008+
