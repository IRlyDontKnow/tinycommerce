﻿using System;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing.InternalCommands
{
    public class InternalCommandDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}