using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NancyRunner.Models;

namespace NancyRunner.Repository
{
    public class StaticQuoteRepository : IQuoteRepository
    {
        private static Dictionary<String, Quote> _repo = new Dictionary<string, Quote>();

        public void DeleteById(string quoteId)
        {
            _repo.Remove(quoteId);
        }

        public QuoteList GetAll()
        {
            var quotes = _repo.Select(k => k.Value);
            return new QuoteList()
            {
                Quotes = quotes
            };
        }

        public Quote GetById(string quoteId)
        {
            return _repo[quoteId];
        }

        public QuoteDetail GetWithDetail(string quoteId)
        {
            return _repo.Where(q => q.Key == quoteId)
                        .Select(q => new QuoteDetail
            {
                Abstract = q.Value.Abstract,
                ArticleBody = q.Value.ArticleBody,
                FullName = q.Value.FullName,
                Id = q.Value.Id,
                Source = q.Value.Source,
                Title = q.Value.Title,
                Year = q.Value.Year,
                UserName = q.Value.UserName,
                SimilarItems = new List<QuoteDetail>()
            }).First();
        }

        public bool Insert(Quote theQuote)
        {
            _repo.Add(theQuote.Id, theQuote);
            return true;
        }

        public bool Update(Quote theQuote)
        {
            var quote = _repo[theQuote.Id];
            if (quote == null)
            {
                _repo.Add(theQuote.Id, theQuote);
            }
            else
            {
                quote = theQuote;
            }
            return true;
        }
    }
}
