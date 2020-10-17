using System;
using System.Collections.Generic;

namespace TinyCommerce.BuildingBlocks.Application
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(List<string> errors)
        {
            Errors = errors;
        }

        public InvalidCommandException(string error)
        {
            Errors = new List<string> {error};
        }

        public List<string> Errors { get; }
    }
}