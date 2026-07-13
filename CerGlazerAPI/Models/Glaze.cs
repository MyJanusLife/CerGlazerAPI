using CerGlazerAPI.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace CerGlazerAPI.Models
{
    public class Glaze
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Glazes_EnsureIdealConeCategory]
        public string IdealConeCategory { get; set; }
    }
}
