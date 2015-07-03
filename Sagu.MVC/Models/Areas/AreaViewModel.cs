using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagu.MVC.Models
{
    public class AreaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public Sagu.DTO.AreaImage Image { get; set; }
    }
}
