using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace DAL.Entities
{
    public class Employee
    {
        [Required]
        [Key]
        [Name("Personnel_Records.Payroll_Number")]
        public string Payroll_Number { get; set; }
        [Name("Personnel_Records.Forenames")]
        public string Forename { get; set; }
        [Name("Personnel_Records.Surname")]
        public string Surname { get; set; }
        [Name("Personnel_Records.Date_of_Birth")]
        public string Date_of_Birth { get; set; }
        [Name("Personnel_Records.Telephone")]
        public int Telephone { get; set; }
        [Name("Personnel_Records.Mobile")]
        public int Mobile { get; set; }
        [Name("Personnel_Records.Address")]
        public string Address { get; set; }
        [Name("Personnel_Records.Address_2")]
        public string Address_2 { get; set; }
        [Name("Personnel_Records.Postcode")]
        public string Postcode { get; set; }
        [Name("Personnel_Records.EMail_Home")]
        public string EMail_Home { get; set; }
        [Name("Personnel_Records.Start_Date")]
        public string Start_Date { get; set; }
        
    }
}
