namespace ApiKarbordAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Access")]
    public partial class Access
    {
        public int Id { get; set; }

        [Required]
        public string lockNumber { get; set; }

        public string CompanyName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string AddressApi { get; set; }


        public string SqlServerName { get; set; }

        public string SqlUserName { get; set; }

        public string SqlPassword { get; set; }



        [StringLength(10)]
        public string fromDate { get; set; }

        [StringLength(10)]
        public string toDate { get; set; }

        public byte? userCount { get; set; }

        public string AFI1_Group { get; set; }

        public string AFI1_Access { get; set; }

        public string AFI8_Group { get; set; }

        public string AFI8_Access { get; set; }

        public string ERJ_Group { get; set; }

        public string ERJ_Access { get; set; }

        public bool? active { get; set; }

        public bool? multilang { get; set; }
    }
}
