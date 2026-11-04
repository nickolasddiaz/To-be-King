using System;
using Godot;

public partial class Bread_Oven : AMachine
{
    public override BuildingType buildingType { get; set; } = BuildingType.Bread_Oven;

    public override void _Ready()
    {
        recipes =
        [
            new Recipe(
                [ProductType.Barley, ProductType.Barley, ProductType.Barley],
                ProductType.Bread,
                20f
            ),
        ];
    }

    public override void _Process(double delta) { }
}
