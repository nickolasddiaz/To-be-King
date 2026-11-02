using System;
using Godot;

public abstract partial class ACrop : ABuilding
{
    public AnimatedSprite2D cropFrames;

    public override void _Ready()
    {
        cropFrames = GetNode<AnimatedSprite2D>("%CropAnimatedSprite");
    }

    public override void _Process(double delta) { }
}
