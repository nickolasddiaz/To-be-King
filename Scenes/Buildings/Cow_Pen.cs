public partial class Cow_Pen : AMachine
{
    public override BuildingType buildingType { get; set; } = BuildingType.Cow_Pen;
    public override Recipe[] recipes =>
        [
            new Recipe(
                [ProductType.Barley, ProductType.Barley, ProductType.Barley],
                ProductType.Milk,
                20f
            ),
        ];

    public override void _Ready() { }

    public override void _Process(double delta) { }
}
