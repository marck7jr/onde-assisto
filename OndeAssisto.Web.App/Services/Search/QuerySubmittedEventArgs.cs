using OndeAssisto.Common.Contracts;
using System;

namespace OndeAssisto.Web.App.Services.Search
{
    public class QuerySubmittedEventArgs : EventArgs , IQuerySubmittedEventArgs
    {
        public string QueryParams { get; set; }
    }
}