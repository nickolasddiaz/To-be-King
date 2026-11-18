using Godot;

public partial class PauseMenu : CanvasLayer
{
    [Export(PropertyHint.File, "*.tscn")]
    public string startScreen { get; set; } = string.Empty;

    [Export(PropertyHint.File, "*.tscn")]
    public string nextTaskScreen { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Continue").Pressed += TogglePause;

        GetNode<Button>("%RestartDay").Pressed += async () =>
        {
            TogglePause();
            await LongSceneManagerCs.Instance.SwitchScene(
                nextTaskScreen,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: true,
                "no_transition"
            );
            PlacementData.Instance.restartDay();
        };

        GetNode<Button>("%RestartGame").Pressed += async () =>
        {
            TogglePause();
            await LongSceneManagerCs.Instance.SwitchScene(
                startScreen,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: false,
                "no_transition"
            );
            PlacementData.Instance.restartGame();
        };
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        GetTree().Paused = !GetTree().Paused;

        if (GetTree().Paused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
