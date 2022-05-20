﻿using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Entitties
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public UserRole Role { get; set; }

        public enum UserRole
        {
            customer = 1,
            admin,
            driver
        }
    }
}