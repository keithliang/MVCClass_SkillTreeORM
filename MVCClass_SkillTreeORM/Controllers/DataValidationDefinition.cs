using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClass_SkillTreeORM.Controllers
{
    public class DataValidationDefinition
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Account { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Email { get; set; }
    }
}