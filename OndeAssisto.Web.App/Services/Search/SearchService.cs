using OndeAssisto.Common.Contracts;
using System;

namespace OndeAssisto.Web.App.Services.Search
{
    public class SearchService : ISearchService
    {
        public event EventHandler<IQuerySubmittedEventArgs> QuerySubmitted;

        public void OnQuerySubmitted(string queryParams)
        {
            QuerySubmitted?.Invoke(this, new QuerySubmittedEventArgs { QueryParams = queryParams });
        }
    }
}
