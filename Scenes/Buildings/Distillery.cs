using System;
using Godot;

public partial class Distillery : AMachine
{
    public new BuildingType buildingType = BuildingType.Distillery;

    public Vector2 inputPosition = new Vector2(0, 0);

    public override void _Ready()
    {
        recipes =
        [
            new Recipe(
                [ProductType.Barley, ProductType.Barley, ProductType.Barley],
                ProductType.Ale,
                20f
            ),
            new Recipe(
                [ProductType.Grape, ProductType.Grape, ProductType.Grape],
                ProductType.Wine,
                20f
            ),
        ];
        InputInventory.Enqueue(ProductType.Barley);
    }

    public override void _Process(double delta) { }

    public override void _Draw()
    {
        drawProducts(inputPosition);
    }
}
