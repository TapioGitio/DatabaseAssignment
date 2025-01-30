using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models.RegForms;

public class StatusRegistrationForm
{
    [Column(TypeName = "nvarchar(15)")]
    public string Status { get; set; } = null!;
}
