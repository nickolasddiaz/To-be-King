using System;
using Godot;

public partial class GamePlayScene : Node2D
{
    /// Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PlacementData.Instance.placementManager = GetNode<PlacementManager>("%PlacementManager");
    }

    /// Called every frame. 'delta' is the elapsed time since the previous frame.
    /// The elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
