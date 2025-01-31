using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.RegForms;

public class ProjectRegistrationForm
{
    [Required]

    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }
    public int ProjectManagerId { get; set; }
    public int ServiceId { get; set; } 
    public int CustomerId { get; set; } 


}
