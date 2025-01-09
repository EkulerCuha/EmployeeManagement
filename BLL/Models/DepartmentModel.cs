using System.ComponentModel;
using BLL.DAL;

namespace BLL.Models;

public class DepartmentModel
{
    public Department Record { get; set; }
    
    [DisplayName("Department Name")]
    public string Name => Record.Name;
    
    public string Description => Record.Description;
}