public partial class Barley_Crop : ACrop
{
    public override BuildingType buildingType { get; set; } = BuildingType.Barley_Crop;

    public override Recipe[] recipes => [new Recipe([], ProductType.Barley, 20f)];

    public override void _Ready() { }

    public override void _Process(double delta) { }
}
