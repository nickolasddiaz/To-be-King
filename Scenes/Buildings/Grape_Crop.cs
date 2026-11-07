public partial class Grape_Crop : ACrop
{
    public override BuildingType buildingType { get; set; } = BuildingType.Grape_Crop;

    public override Recipe[] recipes => [new Recipe([], ProductType.Grape, 20f)];

    public override void _Ready() { }

    public override void _Process(double delta) { }
}
