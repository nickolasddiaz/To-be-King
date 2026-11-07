using System;
using Godot;

public abstract partial class AMachine : ABuilding
{
    public Vector2 inputPosition = new Vector2(16, -14);
    public Vector2 outputPosition = new Vector2(-32, -10);

    public override void _Ready() { }

    public override void _Process(double delta) { }

    public void drawOutputInventory(Vector2 pos)
    {
        if (outputInventory != null)
        {
            var icon = Products.Instance.data[outputInventory.Value].icon;
            DrawTexture(icon, pos);
        }
    }

    public void drawInputInventory(Vector2 pos)
    {
        // make sure the animation player settings "show behind parent is true"
        float y_offset = 0;
        foreach (var product in InputInventory)
        {
            var icon = Products.Instance.data[product].icon;

            Vector2 drawPos = pos + new Vector2(0, y_offset);

            DrawTexture(icon, drawPos);

            y_offset += 5; // 5px padding
        }
    }

    public override void _Draw()
    {
        drawInputInventory(inputPosition);
        drawOutputInventory(outputPosition);
    }
}
