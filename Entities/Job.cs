using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi;

public class Job
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}