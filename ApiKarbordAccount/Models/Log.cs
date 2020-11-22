namespace ApiKarbordAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        [Key]
        public int id { get; set; }

        public int idaccess { get; set; }

        public string usercode { get; set; }

        public int action { get; set; }

        public DateTime date { get; set; }
    }
}
