using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Conventions;

namespace NancyRunner
{
    public class AgileBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            //set this up to define a directory of static assets
            // contains my JS, CSS, Images, etc.
            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/assets")
            );
        }
    }
}
