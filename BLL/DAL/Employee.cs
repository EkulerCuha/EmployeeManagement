using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class Employee
{
    public int Id { get; set; }
    
    [Required]
    [Range(10000000000,99999999999, ErrorMessage = "SSN must be 11 characters long.")]
    
    public long Ssn { get; set; }
    
    [Required]
    [StringLength(50)]
    
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    
    public string LastName { get; set; }
    
    public bool IsFemale { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public DateTime? HireDate { get; set; }
    
    public double Salary { get; set; }
    
    public int DepartmentId { get; set; }
    
    public Department Department { get; set; }
}