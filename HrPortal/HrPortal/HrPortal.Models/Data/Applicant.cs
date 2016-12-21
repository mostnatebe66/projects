using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HrPortal.Models.Data
{
    public class Applicant
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Put your First Name here")]
        [StringLength(maximumLength: 40)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Put your Last Name here")]
        [StringLength(maximumLength: 40)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please list your street address")]
        [StringLength(maximumLength: 40)]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please list the city you live in")]
        [StringLength(maximumLength: 25)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please list the state you live in")]
        [StringLength(maximumLength: 2)]
        public string State { get; set; }

        [Required(ErrorMessage ="Please List your 5 digit zip code")]
        [StringLength(maximumLength: 5)]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please list your phone number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 16)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please put your email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please list the position you're applying for")]
        [StringLength(maximumLength: 40)]
        public string PositionApplied { get; set; }

        [Required(ErrorMessage = "Please list your current Employer")]
        [StringLength(maximumLength: 40)]
        public string CurrentEmployer { get; set; }
    }
}
