﻿using System;
using Autofac;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Infrastructure.Autofac;
using TinyCommerce.BuildingBlocks.Infrastructure.DataAccess;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.DataAccess
{
    public class DataAccessModule : Module
    {
        private readonly string _connectionString;

        public DataAccessModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            builder.RegisterType<PostgresConnectionFactory>()
                .As<IDbConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<CustomersContext>();
                    optionsBuilder.UseNpgsql(_connectionString);
                    optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new CustomersContext(optionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CustomersContext).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}