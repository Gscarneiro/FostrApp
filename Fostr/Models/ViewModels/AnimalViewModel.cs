using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Fostr.Models.ViewModels
{
    public class AnimalViewModel : BaseModel
    {

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Species")]
        public string Species { get; set; }

        [Display(Name = "Race")]
        public string Race { get; set; }

        public IFormFile Image { get; set; }
    }
}
