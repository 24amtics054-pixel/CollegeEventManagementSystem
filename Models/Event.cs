using System.ComponentModel.DataAnnotations;

namespace CollegeEventManagementSystem.Models;

public class Event
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; } = "";

    [Required, StringLength(60)]
    public string Category { get; set; } = "Technical";

    [Required, StringLength(1000)]
    public string Description { get; set; } = "";

    [Required, StringLength(100)]
    public string Venue { get; set; } = "";

    [Required, DataType(DataType.Date)]
    public DateTime EventDate { get; set; } = DateTime.Today.AddDays(7);

    [Range(1, 10000)]
    public int Capacity { get; set; } = 100;

    [StringLength(100)]
    public string? Organizer { get; set; }
}
