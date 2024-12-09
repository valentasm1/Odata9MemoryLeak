using System.ComponentModel.DataAnnotations;

namespace ODataMemoryLeak.Services.Entities;

public class HumanTableEntity
{
    [Key]
    public int Id { get; set; }
    public string? Code { get; set; }

    public IEnumerable<HumanEntity> Humans { get; set; } = Enumerable.Empty<HumanEntity>();
}