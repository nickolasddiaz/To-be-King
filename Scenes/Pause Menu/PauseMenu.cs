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
        GetNode<Button>("%RestartDay").Pressed += () =>
        {
            ScreenManager.Instance.SwitchScreenPath(nextTaskScreen, false);
            TogglePause();
        };
        GetNode<Button>("%RestartGame").Pressed += () =>
        {
            ScreenManager.Instance.SwitchScreenPath(startScreen, false);
            TogglePause();
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
