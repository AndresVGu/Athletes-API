using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AndresVillarreal_Project.Models
{
    [ModelMetadataType(typeof(SportMetaData))]
    public class SportDTO : Auditable
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<AthleteDTO> Athletes { get; set;}


    }
}
