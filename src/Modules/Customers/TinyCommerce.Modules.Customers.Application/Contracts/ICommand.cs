using System;
using MediatR;

namespace TinyCommerce.Modules.Customers.Application.Contracts
{
    public interface ICommand : IRequest
    {
        public Guid Id { get; }
    }
}