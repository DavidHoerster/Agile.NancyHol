using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using NancyRunner.Models;
using NancyRunner.Repository;

namespace NancyRunner.Modules
{
    public class QuoteModule : NancyModule
    {
        private readonly IQuoteRepository _repo;
        public QuoteModule(IQuoteRepository repo)
        {
            _repo = repo;

            Get["/quote"] = _ =>
            {
                var quotes = _repo.GetAll();
                SetUserName(quotes);
                return View["Index.cshtml", quotes];
            };

            Get["/api/quote"] = _ =>
            {
                //need to materialize the list...
                //  content negotiation won't render to XML
                //  but JSON works fine if just an IEnumerable<>
                return _repo.GetAll().Quotes.ToList();
            };

            Get["/quote/{id}"] = args =>
            {
                var quote = _repo.GetWithDetail(args.id);
                SetUserName(quote);
                return View["details.cshtml", quote];
            };

            Get["/quote/create"] = _ =>
            {
                this.RequiresAuthentication();
                var model = new Quote();
                SetUserName(model);
                return View["create.cshtml", model];
            };

            Post["/quote/create"] = args =>
            {
                this.RequiresAuthentication();
                var newQuote = this.Bind<Quote>();
                if (_repo.Insert(newQuote))
                {
                    return this.Response.AsRedirect("/quote");
                }
                else
                {
                    return View["index.cshtml"];
                }
            };

            Get["/quote/delete/{id}"] = args =>
            {
                this.RequiresAuthentication();
                _repo.DeleteById(args.id);
                return this.Response.AsRedirect("/quote");
            };

            Get["/quote/edit/{id}"] = args =>
            {
                this.RequiresAuthentication();
                var quote = _repo.GetById(args.id);
                SetUserName(quote);
                return View["edit.cshtml", quote];
            };

            Post["/quote/edit/{id}"] = model =>
            {
                this.RequiresAuthentication();
                var theQuote = this.Bind<Quote>();
                if (_repo.Update(theQuote))
                {
                    return this.Response.AsRedirect("/quote");
                }
                else
                {
                    return "ooops!";
                }
            };
        }

        private void SetUserName(BaseQuote theObject)
        {
            if (this.Context.CurrentUser == null)
            {
                theObject.UserName = String.Empty;
            }
            else
            {
                theObject.UserName = this.Context.CurrentUser.UserName;
            }
        }
    }
}