using System.ComponentModel;
using BLL.DAL;

namespace BLL.Models;

public class EmployeeModel
{
    public Employee Record { get; set; }
    public string Ssn => Record.Ssn.ToString();
    public string FirstName => Record.FirstName;
    
    public string LastName => Record.LastName;
    
    [DisplayName("Female")]
    public string IsFemale => Record.IsFemale ? "Yes" : "No";

    [DisplayName("Birth Date")]
    public string BirthDate => !Record.BirthDate.HasValue ? string.Empty : Record.BirthDate.Value.ToString("MM/dd/yyyy");
    
    [DisplayName("Hire Date")]
    public string HireDate => !Record.HireDate.HasValue ? string.Empty : Record.HireDate.Value.ToString("MM/dd/yyyy");
    
    public string Salary => Record.Salary.ToString("N2");
    
    public string Department => Record.Department?.Name;
    
}