namespace SchoolAttendance_UI.Models
{
    public class UserRoleAssignmentViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> AssignedRoles { get; set; } // Roles already assigned to the user
        public string RoleToAssign { get; set; } // New role to be assigned
    }


}
