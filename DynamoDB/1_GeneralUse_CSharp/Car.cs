namespace GeneralUse;

public record Car
{
    public int Id { get; set; }
    public int Year { get; set; }
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
}
