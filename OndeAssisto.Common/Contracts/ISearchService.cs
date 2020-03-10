using System;
using System.Collections.Generic;
using System.Text;

namespace OndeAssisto.Common.Contracts
{
    public interface ISearchService
    {
        public event EventHandler<IQuerySubmittedEventArgs> QuerySubmitted;

        public void OnQuerySubmitted(string queryParams);
    }
}
