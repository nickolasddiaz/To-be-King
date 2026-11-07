using System;
using Godot;

public partial class EndGameScreen : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string StartPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Continue").Pressed += () =>
            ScreenManager.Instance.SwitchScreenPath(StartPath, false);
    }
}
