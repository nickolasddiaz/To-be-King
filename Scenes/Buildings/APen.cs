using System;
using Godot;

public abstract partial class APen : ABuilding
{
    public Vector2 inputPosition = new Vector2(16, -14);
    public Vector2 outputPosition = new Vector2(-32, -10);

    public override void _Ready() { }

    public override void _Process(double delta) { }

    public override void _Draw()
    {
        drawInputInventory(inputPosition);
        drawOutputInventory(outputPosition);
    }
}
