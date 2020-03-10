using System;
using System.Collections.Generic;
using System.Text;

namespace OndeAssisto.Common.Contracts
{
    public interface IQuerySubmittedEventArgs
    {
        public string QueryParams { get; set; }
    }
}
