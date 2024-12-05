using BLL.DAL;

namespace BLL.Models;

public class DepartmentModel
{
    public Department Record { get; set; }
    
    public string Name => Record.Name;
    
    public string Description => Record.Description;
}