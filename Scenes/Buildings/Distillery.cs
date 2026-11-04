using System;
using Godot;

public partial class Distillery : AMachine
{
    public override BuildingType buildingType { get; set; } = BuildingType.Distillery;

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
        // InputInventory.Enqueue(ProductType.Barley);
        // InputInventory.Enqueue(ProductType.Barley);
        // InputInventory.Enqueue(ProductType.Barley);

        // outputInventory = ProductType.Ale;
    }

    public override void _Process(double delta) { }
}
