using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace NancyRunner.Modules
{
    public class QuoteModule : NancyModule
    {
        public QuoteModule() : base("/quote")   //base address for routes in this module
        {
            Get["/"] = _ =>
            {
                return "Hello Nancy!";
            };

            Get["/{id}"] = args =>
            {
                return "Hello Nancy for ID " + args.id;
            };
        }
    }
}
