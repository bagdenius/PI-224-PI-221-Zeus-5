using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class UserModel
    {
        public Roles Role { get; set; }

        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public string ProfilePicture { get; set; }

        [PersonalData]
        public string Bio { get; set; }

        [PersonalData]
        public string Sector { get; set; }

        [PersonalData]
        public string EmployeesCount { get; set; }

        [PersonalData]
        public string Organisation { get; set; }

        [PersonalData]
        public bool IsVerified { get; set; }

        [PersonalData]
        public ResumeModel Resume { get; set; }
    }
}
