using System;
using MediatR;

namespace TinyCommerce.Modules.BackOffice.Application.Contracts
{
    public interface ICommand : IRequest
    {
        public Guid Id { get; }
    }
}