using Godot;

public partial class EndOfDayScreen : CanvasLayer
{
    private Label title;
    private Label label;

    [Export(PropertyHint.File, "*.tscn")]
    public string EndGamePath { get; set; } = string.Empty;

    [Export(PropertyHint.File, "*.tscn")]
    public string NextTaskPath { get; set; } = string.Empty;

    public override void _Ready()
    {
        GetNode<Button>("%Continue").Pressed += NextButtonPressed;

        title = GetNode<Label>("%Title");
        label = GetNode<Label>("%Label");
    }

    public void updateEndofDay()
    {
        title.Text = $"End of day results:{DayInfo.Instance.formatDay()}";

        if (Products.Instance.checkIfProductsAreSatisfied())
        {
            if (DayInfo.Instance.islastday())
            {
                title.Text = "Congrats you beat the Game";
            }

            DayInfo.Instance.day++;
            label.Text = "You passed this day: going to the next day";
        }
        else
        {
            label.Text = "You failed this dat: restarting the day";
        }
    }

    private void NextButtonPressed()
    {
        if (DayInfo.Instance.islastday() && Products.Instance.checkIfProductsAreSatisfied())
        {
            ScreenManager.Instance.SwitchScreenPath(EndGamePath, false);
        }
        else
        {
            ScreenManager.Instance.SwitchScreenPath(NextTaskPath, false);
        }
    }
}
