using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AndresVillarreal_Project.Models
{
    public class ContingentMetaData
    {
    

        [Required(ErrorMessage = "You cannot leave the contingent code blank.")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "The contingent code must be exactly 2 capital letters.")]
        [StringLength(2)]
        public string Code { get; set; }

        [Required(ErrorMessage = "You cannot leave the contingent name blank.")]
        [StringLength(50, ErrorMessage = "Contingent name cannot be more than 50 characters long.")]
        public string Name { get; set; }

      

    }
}
