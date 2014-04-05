using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using NancyRunner.Models;
using NancyRunner.Repository;

namespace NancyRunner.Modules
{
    public class SecureQuoteModule : SecureModule
    {
        private readonly IQuoteRepository _repo;
        public SecureQuoteModule(IQuoteRepository repo)
        {
            _repo = repo;

            Get["/quote/create"] = _ =>
            {
                var model = new Quote();
                return View["create.cshtml", model];
            };

            Post["/quote/create"] = args =>
            {
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
                _repo.DeleteById(args.id);
                return this.Response.AsRedirect("/quote");
            };

            Get["/quote/edit/{id}"] = args =>
            {
                var quote = _repo.GetById(args.id);
                return View["edit.cshtml", quote];
            };

            Post["/quote/edit/{id}"] = model =>
            {
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
    }
}
