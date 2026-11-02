using System;
using Godot;

public partial class Bread_Oven : AMachine
{
    public new BuildingType buildingType = BuildingType.Bread_Oven;

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
