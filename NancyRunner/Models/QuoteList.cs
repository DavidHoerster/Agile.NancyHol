using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyRunner.Models
{
    public class QuoteList : BaseQuote
    {
        public IEnumerable<Quote> Quotes { get; set; }
    }
}
