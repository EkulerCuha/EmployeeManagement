using System.ComponentModel;
using BLL.DAL;
namespace BLL.Models;

public class ProjectModel
{
    public Project Record { get; set; }
    
    public string Name => Record.Name;
    
    [DisplayName("Start Date")]
    public string StartDate => !Record.StartDate.HasValue ? string.Empty : Record.StartDate.Value.ToString("MM/dd/yyyy");
    
    [DisplayName("Due Date")]
    public string DueDate => !Record.DueDate.HasValue ? string.Empty : Record.DueDate.Value.ToString("MM/dd/yyyy");
}