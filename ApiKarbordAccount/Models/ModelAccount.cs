namespace ApiKarbordAccount.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelAccount : DbContext
    {
        public ModelAccount()
            : base("name=ModelAccount")
        {
        }

        public virtual DbSet<Access> Access { get; set; }

        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
