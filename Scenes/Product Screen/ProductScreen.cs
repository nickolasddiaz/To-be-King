using Godot;
using Godot.Collections;

public partial class ProductScreen : Control
{
    private HBoxContainer hBoxContainer;

    [Export]
    public PackedScene product_item { get; set; }
    private Label timeLabel;
    private readonly Dictionary<ProductType, ProductItem> productContainers = new();

    public override void _Ready()
    {
        hBoxContainer = GetNode<HBoxContainer>("%HBoxContainer");
        timeLabel = GetNode<Label>("%Time");

        foreach (var (product, data) in Products.Instance.data)
        {
            ProductItem sceneInstance = (ProductItem)product_item.Instantiate();
            sceneInstance.setTexture(data.logo);
            sceneInstance.setText(data.amount.ToString());
            productContainers[product] = sceneInstance;
            hBoxContainer.AddChild(sceneInstance);
        }

        setTimeText("9:99");
    }

    public void updateProductAmounts()
    {
        foreach (var (product, sceneInstance) in productContainers)
        {
            sceneInstance.setText(Products.Instance.data[product].amount.ToString());
        }
    }

    public void setTimeText(string text)
    {
        timeLabel.Text = text;
    }
}
