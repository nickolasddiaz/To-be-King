using System;
using Godot;

public partial class Bread_Oven : AMachine
{
    public AnimatedSprite2D barelyFrames;

    public AnimatedSprite2D breadFrames;

    public new BuildingType buildingType = BuildingType.Bread_Oven;

    public override void _Ready()
    {
        barelyFrames = GetNode<AnimatedSprite2D>("%BarelyAnimatedSprite");
        breadFrames = GetNode<AnimatedSprite2D>("%BreadAnimatedSprite");
    }

    public override void _Process(double delta) { }
}
