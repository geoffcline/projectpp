using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Project__.Models
{
    public class UsersVM
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DefaultGroupID { get; set; }
    }

    public class PlusPlusContext : DbContext
    {
        public PlusPlusContext() : base("name=PlusPlus") { }

        public DbSet<UsersVM> Users { get; set; }
    }
}