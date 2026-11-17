using System;
using Godot;

public partial class CreditsScreen : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string StartPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Back").Pressed += async () =>
        {
            await LongSceneManagerCs.Instance.SwitchScene(
                StartPath,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: false,
                "no_transition"
            );
        };
    }
}
