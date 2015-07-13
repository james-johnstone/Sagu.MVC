using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagu.DTO;

namespace Sagu.MVC.Models
{
    public class AnimalsViewModel
    {
        public IEnumerable<Animal> Animals { get; set; }
    }
}
