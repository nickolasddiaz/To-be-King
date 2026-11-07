using System;
using Godot;

public partial class TutorialScreen : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Continue").Pressed += () =>
            ScreenManager.Instance.SwitchScreenPath(NextPath, false);
    }
}
