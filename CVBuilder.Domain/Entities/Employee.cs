using CVBuilder.Domain.Enums;

namespace CVBuilder.Domain.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }

        //Navigation Properties
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<Degree>? Degrees { get; set; }
        public ICollection<WorkExperience>? WorkExperiences { get; set; }
        public ICollection<Project>? Projects { get; set; }


    }
}
