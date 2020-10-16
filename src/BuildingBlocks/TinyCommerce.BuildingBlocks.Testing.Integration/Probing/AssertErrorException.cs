using System;

namespace TinyCommerce.BuildingBlocks.Testing.Integration.Probing
{
    public class AssertErrorException : Exception
    {
        public AssertErrorException(string message)
            : base(message)
        {
        }
    }
}