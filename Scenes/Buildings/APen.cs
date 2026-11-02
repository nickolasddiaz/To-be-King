using System;
using Godot;

public abstract partial class APen : ABuilding
{
    public AnimatedSprite2D barelyFrames;

    public AnimatedSprite2D produceFrames;

    public AnimatedSprite2D animalFrames;

    public AnimatedSprite2D fenceFrames;

    public override void _Ready()
    {
        barelyFrames = GetNode<AnimatedSprite2D>("%BaseAnimatedSprite");
        produceFrames = GetNode<AnimatedSprite2D>("%BaseAnimatedSprite");
        animalFrames = GetNode<AnimatedSprite2D>("%BaseAnimatedSprite");
        fenceFrames = GetNode<AnimatedSprite2D>("%BaseAnimatedSprite");
    }

    public override void _Process(double delta) { }
}
