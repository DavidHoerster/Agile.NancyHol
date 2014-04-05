using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NancyRunner.Models;

namespace NancyRunner.Repository
{
    public interface IQuoteRepository
    {
        void DeleteById(string quoteId);
        QuoteList GetAll();
        Quote GetById(string quoteId);
        QuoteDetail GetWithDetail(string quoteId);
        bool Insert(Quote theQuote);
        bool Update(Quote theQuote);
    }
}
