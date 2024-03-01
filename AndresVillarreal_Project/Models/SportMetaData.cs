using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AndresVillarreal_Project.Models
{
    public class SportMetaData 
    {
       

        [Required(ErrorMessage = "You cannot leave the sport code blank.")]
        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "The sport code must be exactly 3 capital letters.")]
        [StringLength(3)]
        public string Code { get; set; }

        [Required(ErrorMessage = "You cannot leave the sport name blank.")]
        [StringLength(50, ErrorMessage = "Sport name cannot be more than 50 characters long.")]
        public string Name { get; set; }


    }
}
