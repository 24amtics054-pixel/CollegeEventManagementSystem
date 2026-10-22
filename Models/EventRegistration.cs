using System.ComponentModel.DataAnnotations;

namespace CollegeEventManagementSystem.Models;

public class EventRegistration
{
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }
    public Event? Event { get; set; }

    [Required, StringLength(80)]
    public string StudentName { get; set; } = "";

    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = "";

    [Required, StringLength(20)]
    public string StudentId { get; set; } = "";

    public DateTime RegisteredAt { get; set; } = DateTime.Now;
}
