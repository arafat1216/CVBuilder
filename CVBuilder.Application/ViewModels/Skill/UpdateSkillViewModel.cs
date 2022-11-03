using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.ViewModels.Skill
{
    public class UpdateSkillViewModel
    {
        public int? SkillId { get; set; }
        public string? Name { get; set; }
        public string Reason { get; set; }
    }
}
