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
        LongSceneManagerCs.Instance.MaxCacheSize = 20;
        GetNode<Button>("%Credits").Pressed += async () =>
        {
            await LongSceneManagerCs.Instance.SwitchScene(
                CreditsPath,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: false,
                "no_transition"
            );
        };

        GetNode<Button>("%StartGame").Pressed += async () =>
        {
            await LongSceneManagerCs.Instance.SwitchScene(
                NextPath,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: false,
                "no_transition"
            );
        };
    }
}
