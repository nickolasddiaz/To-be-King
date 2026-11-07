using System;
using Godot;

public partial class StartGameScreen : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string CreditsPath { get; set; } = string.Empty;

    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Credits").Pressed += () =>
            ScreenManager.Instance.SwitchScreenPath(CreditsPath, false);

        GetNode<Button>("%StartGame").Pressed += () =>
            ScreenManager.Instance.SwitchScreenPath(NextPath, false);
    }
}
