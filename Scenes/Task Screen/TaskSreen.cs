using Godot;
using Godot.Collections;

public partial class TaskSreen : Control
{
    private GridContainer inventoryContainer;

    [Export]
    public PackedScene next_task_screen_box { get; set; }

    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; } = string.Empty;

    private readonly Dictionary<ProductType, NextTaskScreenBox> productItems = new();

    public override void _Ready()
    {
        inventoryContainer = GetNode<GridContainer>("%InventoryGridContainer");
        var continue_button = GetNode<Button>("%Continue");

        foreach (Node child in inventoryContainer.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var (product, data) in Products.Instance.data)
        {
            var sceneInstance = (NextTaskScreenBox)next_task_screen_box.Instantiate();
            sceneInstance.setTexture(data.logo);
            int productCount = 0;
            if (DayInfo.Instance.Days[0].ProductNeeds.TryGetValue(product, out int value))
            {
                productCount = value;
            }
            else
            {
                productCount = 0;
            }
            sceneInstance.setText($"{product}: {productCount}");
            productItems[product] = sceneInstance;
            inventoryContainer.AddChild(sceneInstance);
        }

        continue_button.Pressed += OnButtonPressed;
    }

    public void updateProductAmounts()
    {
        foreach ((ProductType product, NextTaskScreenBox sceneInstance) in productItems)
        {
            sceneInstance.setText($"{product}: {Products.Instance.data[product].amount}");
        }
    }

    private void OnButtonPressed()
    {
        PlayerData.Instance.newDay();
        updateProductAmounts();
        ScreenManager.Instance.SwitchScreenPath(NextPath, false);
    }
}
