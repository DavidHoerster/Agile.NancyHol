using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Conventions;
using NancyRunner.Repository;

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
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            container.Register<IQuoteRepository>(new StaticQuoteRepository());
            container.Register<IUserMapper, UserRepository>();

            //configure forms authentication
            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
            {
                RedirectUrl = "~/login",
                UserMapper = container.Resolve<IUserMapper>(),
            };
            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }
    }
}