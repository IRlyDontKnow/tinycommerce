using Autofac;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.BuildingBlocks.Infrastructure;
using TinyCommerce.BuildingBlocks.Infrastructure.Autofac;
using TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;
using TinyCommerce.Modules.Customers.Application.Configuration;
using TinyCommerce.Modules.Customers.Infrastructure.InternalCommands;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing
{
    public class ProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkDomainEventsProvider>()
                .As<IDomainEventsProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Infrastructure.Outbox.Outbox>()
                .As<IOutbox>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandsScheduler>()
                .As<ICommandsScheduler>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(UnitOfWorkBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}