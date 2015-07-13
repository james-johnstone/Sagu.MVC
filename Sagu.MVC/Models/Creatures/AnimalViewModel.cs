using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sagu.DTO;

namespace Sagu.MVC.Models
{
    public class AnimalViewModel
    {
        public IEnumerable<SelectListItem> Areas { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Kana { get; set; }
        public bool IsUnique { get; set; }
        public double EncounterRate { get; set; }
        public SkillAttribute Attrbiutes { get; set; }
        [Display(Name="Area")]
        public Guid AreaId { get; set; }
        public Area Area { get; set; }
    }
}
