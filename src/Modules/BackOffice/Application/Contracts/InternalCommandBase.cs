using System;

namespace TinyCommerce.Modules.BackOffice.Application.Contracts
{
    public abstract class InternalCommandBase : ICommand
    {
        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}