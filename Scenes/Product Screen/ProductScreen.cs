using System;
using Godot;
using Godot.Collections;

public partial class ProductScreen : CanvasLayer
{
    private HBoxContainer hBoxContainer;

    [Export]
    public PackedScene product_item { get; set; }
    private Label timeLabel;
    private readonly Dictionary<ProductType, ProductItem> productContainers = new();

    public override void _Ready()
    {
        PlacementData.Instance.productScreen = this;
        hBoxContainer = GetNode<HBoxContainer>("%HBoxContainer");
        timeLabel = GetNode<Label>("%Time");

        foreach (var (product, data) in Products.Instance.data)
        {
            ProductItem sceneInstance = (ProductItem)product_item.Instantiate();
            hBoxContainer.AddChild(sceneInstance);
            sceneInstance.setTexture(data.logo);
            productContainers[product] = sceneInstance;
        }

    }

    public void updateProductAmounts()
    {
        setTime(DayInfo.Instance.getToday().Time);


        foreach (ProductType product in productContainers.Keys)
        {
            productContainers[product].Hide();
        }
        foreach ((ProductType product, int number) in DayInfo.Instance.getToday().ProductNeeds)
        {
            productContainers[product].setText(number.ToString());
            productContainers[product].Show();
        }

    }

    public void setTime(float time)
    {
        TimeSpan elapsedTime = TimeSpan.FromSeconds(time);
        timeLabel.Text = string.Format("{0:D2}:{1:D2}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds);
    }
}
