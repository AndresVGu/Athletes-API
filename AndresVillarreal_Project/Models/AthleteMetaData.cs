using System.ComponentModel.DataAnnotations;

namespace AndresVillarreal_Project.Models
{
    public class AthleteMetaData 
    {
    

        [Display(Name = "Athlete")]
        public string Summary
        {
            get
            {
                return FormalName + " - " + ACode;
            }
        }

        [Display(Name = "Athlete")]
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
        [Display(Name = "Athlete")]
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? "" :
                        (" " + (char?)MiddleName[0] + ".").ToUpper());
            }
        }
        [Display(Name = "ID Code")]
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


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Athlete Code")]
        [Required(ErrorMessage = "The 7 digit Code for the Athlete is required")]
        [RegularExpression("^\\d{7}$", ErrorMessage = "The Athlete Code must be exactly 7 numeric digits.")]
        [StringLength(7)]
        public string AthleteCode { get; set; }

        [Required(ErrorMessage = "You must enter the date of birth.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Height (cm)")]
        [Required(ErrorMessage = "You cannot leave the Height blank.")]
        [Range(61, 245, ErrorMessage = "Height must be between 61cm and 245cm.")]
        public int Height { get; set; }

        [Display(Name = "Weight (kg)")]
        [Required(ErrorMessage = "You cannot leave the Weight blank.")]
        [Range(18.0d, 180.0d, ErrorMessage = "Weight must be between 18kg and 180kg.")]
        public double Weight { get; set; }

        [Display(Name = "Club or Team affiliation")]
        [Required(ErrorMessage = "You must enter the Club or Team Affiliation.")]
        [StringLength(255, ErrorMessage = "Affiliation cannot be more than 255 characters long.")]
        public string Affiliation { get; set; }

        [Display(Name = "Other information that could be of interest to the media")]
        [Required(ErrorMessage = "You must enter information for the media.")]
        [StringLength(2000, ErrorMessage = "Media informaiton cannot be more than 2000 characters long.")]
        [DataType(DataType.MultilineText)]
        public string MediaInfo { get; set; }

        [Required(ErrorMessage = "You must enter M or W for the competition gender!")]
        [StringLength(1)]
        [RegularExpression("^[MW]$", ErrorMessage = "Competition gender must be either W or M")]
        public string Gender { get; set; }
        //Adding Data
        #region ExtraData
       
        [Display(Name ="Contingent")]
        public Contingent Contingent { get; set; }
     
        [Display(Name ="Sport")]
        public Sport Sport { get; set; }
        #endregion


        
    }

}
