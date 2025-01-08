namespace GameOfVlad.Utils;

public record struct Size(float Width, float Height)
{
    public static Size Create(float width, float height) => new(width, height);
}