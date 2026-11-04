using System;
using Godot;

public partial class Cow_Pen : APen
{
    public override BuildingType buildingType { get; set; } = BuildingType.Cow_Pen;

    public override void _Ready()
    {
        recipes =
        [
            new Recipe(
                [ProductType.Barley, ProductType.Barley, ProductType.Barley],
                ProductType.Milk,
                20f
            ),
        ];
    }

    public override void _Process(double delta) { }
}
