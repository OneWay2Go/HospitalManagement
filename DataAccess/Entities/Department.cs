using HospitalManagement.Application.Commands.CreateDepartment;

namespace HospitalManagement.DataAccess.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }


        public DepartmentDto ToDto()
        {
            return new()
            {
                DepartmentName = DepartmentName,
                Location = Location,
                Description = Description
            };
        }
    }
}
