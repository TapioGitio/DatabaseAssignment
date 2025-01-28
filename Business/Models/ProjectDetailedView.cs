namespace Business.Models;

public class ProjectDetailedView
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public string ManagerFirstName { get; set; } = null!;
    public string ManagerLastName { get; set; } = null!;
    public string ManagerPhoneNumber { get; set; } = null!;
    public string CustomerFirstName { get; set; } = null!;
    public string CustomerLastName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
}
