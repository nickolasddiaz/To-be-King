using System;
using Godot;

/// Base class for every building (crops, pens, ovens, distillery...).
/// Needs a CollisionShape2D child so the player can detect clicks on it.
public abstract partial class ABuilding : Node2D
{
    // Shared properties that every building MUST define in the Inspector
    [Export]
    public int Cost { get; set; } = 100;

    [Export]
    public Vector2 Size { get; set; } = new Vector2(1, 1);

    [Export]
    public Texture2D pic { get; set; }

    [Export]
    public SpriteFrames frames { get; set; }

    const float BUILDINGUNITSIZE = 32.0f;

    public virtual void OnInteract() { }

    private void set_area2d_size()
    {
        var shape = GetNode<CollisionShape2D>("%CollisionShape2D");

        if (shape.Shape is RectangleShape2D rectangle)
        {
            rectangle.Size = Size * BUILDINGUNITSIZE;
        }
        else
        {
            throw new Exception("Building CollisionShape2D needs to be a RectangleShape2D");
        }
    }

    public override void _Ready()
    {
        set_area2d_size();
    }

    public override void _Process(double delta)
    {
        // Shared frame processing can go here
    }

    public virtual void Interact(Player player)
    {
        OnInteract();
    }
}
