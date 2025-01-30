using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.RegForms;

public class ProjectRegistrationForm
{

    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }

    [Column(TypeName = "nvarchar(15)")]
    public string Status { get; set; } = null!;
}
