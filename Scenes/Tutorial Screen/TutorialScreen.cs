using Godot;

public partial class TutorialScreen : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Continue").Pressed += async () =>
        {
            await LongSceneManagerCs.Instance.SwitchScene(
                NextPath,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: true,
                "no_transition"
            );

            TaskScreen taskScene = (TaskScreen)LongSceneManagerCs.Instance.GetCurrentScene();
            taskScene.updateProductAmounts();
        };
    }
}
