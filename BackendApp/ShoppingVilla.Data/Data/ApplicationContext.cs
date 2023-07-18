using Microsoft.EntityFrameworkCore;
using ShoppingVilla.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingVilla.Data.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<UserLogin> userLogins { get; set; }
        public DbSet<UserRegister> userRegister { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLogin>(ul =>
            {
                ul.HasNoKey();
            });

            #region for single unique key
            builder.Entity<UserRegister>(r =>
            {
                r.HasIndex(u=>u.Email).IsUnique();
                r.HasIndex(u => u.UserName).IsUnique();
            });
            #endregion
            #region for multiple unique key
            //builder.Entity<UserRegister>().HasKey(r=> new {r.Id, r.Email,r.UserName});
            #endregion

        }
    }
}
