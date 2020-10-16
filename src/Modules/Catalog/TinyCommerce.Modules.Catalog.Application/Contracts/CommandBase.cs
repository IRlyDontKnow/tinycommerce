using System;

namespace TinyCommerce.Modules.Catalog.Application.Contracts
{
    public abstract class CommandBase : ICommand
    {
        protected CommandBase(Guid id)
        {
            Id = id;
        }

        protected CommandBase() : this(Guid.NewGuid())
        {
        }

        public Guid Id { get; }
    }
}