﻿using System.Data.Entity;
using Models;

namespace Common
{
    public class DefaultContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Tsingbo.Web.Models.DefaultContext>());

        public DefaultContext()
            : base("name=DefaultConnection")
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Article> Articles { get; set; }
    }
}
