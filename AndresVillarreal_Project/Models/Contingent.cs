using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AndresVillarreal_Project.Models
{
    [ModelMetadataType(typeof(ContingentMetaData))]
    public class Contingent
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        
        public ICollection<Athlete> Athletes { get; set;}

    }
}
