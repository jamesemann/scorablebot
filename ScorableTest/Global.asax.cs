using System.Web;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;
using ScorableTest.Dialogs.Scorable1;
using ScorableTest.Dialogs.Scorable2;

namespace ScorableTest
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            builder.RegisterType<Scorable1>()
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Scorable2>()
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            builder.Update(Conversation.Container);
        }
    }
}