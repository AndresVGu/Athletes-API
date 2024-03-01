using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AndresVillarreal_Project.Models
{
    [ModelMetadataType(typeof(AthleteMetaData))]
    public class Athlete : Auditable, IValidatableObject
    {
        public int ID { get; set; }

        public string Summary
        {
            get
            {
                return FormalName + " - " + ACode;
            }
        }

        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? "" :
                        (" " + (char?)MiddleName[0] + ".").ToUpper());
            }
        }
        public string ACode
        {
            get
            {
                return "A:" + AthleteCode.ToString().PadLeft(7, '0');
            }
        }
        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int a = today.Year - DOB.Year
                    - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
                return a.ToString(); /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }


        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string AthleteCode { get; set; }

        public DateTime DOB { get; set; }

        public int Height { get; set; }
        public double Weight { get; set; }
        public string Affiliation { get; set; }

        public string MediaInfo { get; set; }

        public string Gender { get; set; }
        //Adding Data
        #region ExtraData
        public int ContingentID { get; set; }

        public Contingent Contingent { get; set; }
        public int SportID { get; set; }

        public Sport Sport { get; set; }
        #endregion


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Test date range for DOB - Allowed age range for Summer 2024
            if (DOB < Convert.ToDateTime("1994-08-22") || DOB >= Convert.ToDateTime("2012-08-07"))
            {
                yield return new ValidationResult("DOB must be between 1994-08-22 and 2012-08-06.", new[] { "DOB" });
            }
            //Test BMI Value
            double BMI = Weight / Math.Pow(Height / 100d, 2);
            if (BMI < 15 || BMI >= 40)
            {
                yield return new ValidationResult("BMI of " + BMI.ToString("n1")
                    + " is outside the allowable range of 15 to 40", new[] { "Weight" });
            }
        }
    }

}
