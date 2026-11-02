using System;
using Godot;

public partial class Grape_Crop : ACrop
{
    public new BuildingType buildingType = BuildingType.Grape_Crop;

    public override void _Ready()
    {
        recipes = [new Recipe([], ProductType.Grape, 20f)];
    }

    public override void _Process(double delta) { }
}
