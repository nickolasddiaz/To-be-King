public partial class Cabbage_Crop : ACrop
{
    public override BuildingType buildingType { get; set; } = BuildingType.Cabbage_Crop;

    public override Recipe[] recipes => [new Recipe([], ProductType.Cabbage, 20f)];

    public override void _Ready() { }

    public override void _Process(double delta) { }

    public override void _Draw() { }
}
