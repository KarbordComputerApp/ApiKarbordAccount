namespace ApiKarbordAccount.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;


    public partial class ModelTiket : DbContext
    {
        public ModelTiket()
            : base("name=TicketPoshtibani")
        {

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
