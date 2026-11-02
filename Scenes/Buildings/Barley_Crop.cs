using System;
using Godot;

public partial class Barley_Crop : ACrop
{
    public new BuildingType buildingType = BuildingType.Barley_Crop;

    public override void _Ready()
    {
        recipes = [new Recipe([], ProductType.Barley, 20f)];
    }

    public override void _Process(double delta) { }
}
