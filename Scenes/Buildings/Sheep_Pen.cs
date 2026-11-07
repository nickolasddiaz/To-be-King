public partial class Sheep_Pen : AMachine
{
    public override BuildingType buildingType { get; set; } = BuildingType.Sheep_Pen;

    public override Recipe[] recipes =>
        [
            new Recipe(
                [ProductType.Barley, ProductType.Barley, ProductType.Barley],
                ProductType.Wool,
                20f
            ),
        ];

    public override void _Ready() { }

    public override void _Process(double delta) { }
}
