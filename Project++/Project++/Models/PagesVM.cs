using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Project__.Models
{
    public class Algebra
    {
        public string sql { get; set; }
    }

    public class Strings
    {
        public string String_Name { get; set; }
        public string String_Type { get; set; }
    }

    public class DefaultConnection : DbContext
    {
        public DbSet<Strings> strings { get; set; }
    }

}