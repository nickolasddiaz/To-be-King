using System;
using Godot;

public partial class Sheep_Pen : APen
{
    public new BuildingType buildingType = BuildingType.Sheep_Pen;

    public override void _Ready()
    {
        recipes =
        [
            new Recipe(
                [ProductType.Barley, ProductType.Barley, ProductType.Barley],
                ProductType.Wool,
                20f
            ),
        ];
    }

    public override void _Process(double delta) { }
}
