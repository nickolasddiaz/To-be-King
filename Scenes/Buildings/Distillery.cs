using System;
using Godot;

public partial class Distillery : AMachine
{
    private AnimatedSprite2D _barelyNode;
    private AnimatedSprite2D _barely2Node;
    private AnimatedSprite2D _grapeNode;
    private AnimatedSprite2D _aleNode;
    private AnimatedSprite2D _wineNode;

    public new BuildingType buildingType = BuildingType.Distillery;

    public override void _Ready()
    {
        _barelyNode = GetNode<AnimatedSprite2D>("%BarelyAnimatedSprite");
        _barely2Node = GetNode<AnimatedSprite2D>("%Barely2AnimatedSprite");
        _grapeNode = GetNode<AnimatedSprite2D>("%GrapeAnimatedSprite");
        _aleNode = GetNode<AnimatedSprite2D>("%AleAnimatedSprite");
        _wineNode = GetNode<AnimatedSprite2D>("%WineAnimatedSprite");
    }

    public override void _Process(double delta) { }
}
