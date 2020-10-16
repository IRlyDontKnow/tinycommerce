using System;
using MediatR;

namespace TinyCommerce.Modules.Catalog.Application.Contracts
{
    public interface ICommand : IRequest
    {
        public Guid Id { get; }
    }
}