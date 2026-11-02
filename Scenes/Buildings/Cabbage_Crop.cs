using System;
using Godot;

public partial class Cabbage_Crop : ACrop
{
    public new BuildingType buildingType = BuildingType.Cabbage_Crop;

    public override void _Ready()
    {
        recipes = [new Recipe([], ProductType.Cabbage, 20f)];
    }

    public override void _Process(double delta) { }

    public override void _Draw() { }
}
