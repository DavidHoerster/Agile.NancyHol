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

            #region Before Request Hook

            Before += ctx =>
            {
                Console.WriteLine("I'm about to execute {0}", ctx.Request.Url.ToString());
                return null;
            };

            #endregion

            #region After Request Hook

            After += ctx =>
            {
                if (ctx.CurrentUser != null)
                {
                    ViewBag.UserName = ctx.CurrentUser.UserName;
                    ViewBag.IsLoggedIn = true;
                }
                else
                {
                    ViewBag.UserName = "Whoever You Are";
                    ViewBag.IsLoggedIn = false;
                }
            };

            #endregion 

            Get["/quote"] = _ =>
            {
                var quotes = _repo.GetAll();
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
                return View["details.cshtml", quote];
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