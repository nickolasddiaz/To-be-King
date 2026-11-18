using Godot;

public partial class GamePlayScene : Node2D
{
    public Player Player { get; private set; }

    public override void _Ready()
    {
        PlacementData.Instance.buildingContainer = GetNode<Node2D>("%BuildingPlacementLocation");
        PlacementData.Instance.gamePlayScene = this;
        Player = GetNode<Player>("%Player");
    }

    public void hidePlayer()
    {
        Player.Hide();
        Player.SetPhysicsProcess(false);
    }

    public void showPlayer()
    {
        Player.Show();
        Player.Position = new Vector2(800, 270);
        Player.SetPhysicsProcess(true);
    }

    public override void _Process(double delta) { }

}
