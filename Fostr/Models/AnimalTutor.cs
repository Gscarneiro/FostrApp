using Fostr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fostr.Models
{
    public class AnimalTutor : BaseModel
    {
        public int AnimalId { get; set; }

        public Animal Animal { get; set; }

        public string UserId { get; set; }

        public FostrUser FostrUser { get; set; }
    }
}
