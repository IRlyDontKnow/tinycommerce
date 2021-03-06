﻿using System;

namespace TinyCommerce.Modules.BackOffice.Application.Authentication
{
    public class SecurityAdministrator
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
