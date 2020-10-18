using Autofac;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.Events;
using TinyCommerce.BuildingBlocks.Infrastructure;
using TinyCommerce.BuildingBlocks.Infrastructure.Autofac;
using TinyCommerce.BuildingBlocks.Infrastructure.DomainEvents;
using TinyCommerce.BuildingBlocks.Infrastructure.Processing.Outbox;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Configuration.Processing
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

            builder.RegisterGeneric(typeof(UnitOfWorkBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}