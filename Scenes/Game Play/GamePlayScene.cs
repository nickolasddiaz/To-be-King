using System;
using Godot;

public partial class GamePlayScene : Node2D
{
    public override void _Ready()
    {
        PlacementData.Instance.buildingContainer = GetNode<Node2D>("%BuildingPlacementLocation");
    }

    public override void _Process(double delta) { }
}
