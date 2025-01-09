using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class Project
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Name { get; set; }
    
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}