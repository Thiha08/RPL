using Autofac;
using RPL.Core.Interfaces;
using RPL.Core.Services;
using RPL.Core.Settings.SMS;

namespace RPL.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemSearchService>()
                .As<IToDoItemSearchService>().InstancePerLifetimeScope();

            builder.RegisterType<SmsSettings>().As<ISmsSettings>().InstancePerLifetimeScope()
                .OnActivated(x => x.Instance.Initialize());
        }
    }
}
