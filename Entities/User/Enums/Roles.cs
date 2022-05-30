using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public enum Roles
    {
        [Display(Name ="Адміністратор")]
        Admin,

        [Display(Name = "Працівник")]
        Applicant,

        [Display(Name = "Роботодавець")]
        Employer
    }
}
