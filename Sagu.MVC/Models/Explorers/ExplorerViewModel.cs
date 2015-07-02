using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagu.DTO;

namespace Sagu.MVC.Models
{
    public class ExplorerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExploredArea> ExploredAreas { get; set; }
    }
}
