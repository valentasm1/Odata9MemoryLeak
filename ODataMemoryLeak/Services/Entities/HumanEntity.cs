using System.ComponentModel.DataAnnotations;

namespace ODataMemoryLeak.Services.Entities;

public class HumanEntity
{
    [Key]
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? HumanTableId { get; set; }
    public HumanTableEntity? HumanTable { get; set; }
}