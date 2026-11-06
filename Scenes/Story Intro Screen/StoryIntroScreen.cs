using System;
using Godot;

public partial class StoryIntroScreen : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        var continue_button = GetNode<Button>("%Continue");
        continue_button.Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        ScreenManager.Instance.SwitchScreenPath(NextPath, false);
    }
}
