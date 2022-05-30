﻿using DAL.Entities.Addons;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ResumeModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserModel Profile { get; set; }

        [DataType(DataType.Url)]
        public string LinkedIn { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
