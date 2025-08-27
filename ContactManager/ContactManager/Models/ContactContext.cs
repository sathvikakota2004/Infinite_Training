using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ContactManager.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext() : base("ContactContext") // connection string name
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}