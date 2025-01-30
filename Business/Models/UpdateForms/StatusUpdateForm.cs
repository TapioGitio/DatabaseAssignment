using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models.UpdateForms;

public class StatusUpdateForm
{

    [Column(TypeName = "nvarchar(15)")]
    public string Status { get; set; } = null!;
}

