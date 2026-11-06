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
        var credits_button = GetNode<Button>("%Credits");
        credits_button.Pressed += CreditOnButtonPressed;

        var gameplay_button = GetNode<Button>("%StartGame");
        gameplay_button.Pressed += GameplayOnButtonPressed;
    }

    private void CreditOnButtonPressed()
    {
        ScreenManager.Instance.SwitchScreenPath(CreditsPath, false);
    }

    private void GameplayOnButtonPressed()
    {
        ScreenManager.Instance.SwitchScreenPath(NextPath, false);
    }
}
