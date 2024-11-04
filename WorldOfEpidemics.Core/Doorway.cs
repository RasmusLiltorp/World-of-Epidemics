namespace WorldOfEpidemics.Core;

public record Doorway
{
    public required string Name { get; init; }
    public required Room Room { get; init; }
}
