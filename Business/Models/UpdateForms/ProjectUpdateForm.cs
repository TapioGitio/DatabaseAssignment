using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models.UpdateForms;

public class ProjectUpdateForm
{
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
