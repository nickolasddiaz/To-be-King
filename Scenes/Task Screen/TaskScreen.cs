using Godot;
using Godot.Collections;

public partial class TaskScreen : CanvasLayer
{
    private GridContainer inventoryContainer;

    [Export]
    public PackedScene next_task_screen_box { get; set; }

    [Export(PropertyHint.File, "*.tscn")]
    public string gamePlayScreen { get; set; } = string.Empty;

    [Export(PropertyHint.File, "*.tscn")]
    public string unlockScreen { get; set; } = string.Empty;

    private readonly Dictionary<ProductType, NextTaskScreenBox> productItems = new();

    public override void _Ready()
    {
        inventoryContainer = GetNode<GridContainer>("%InventoryGridContainer");
        GetNode<Button>("%Continue").Pressed += OnButtonPressed;

        foreach (Node child in inventoryContainer.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var (product, data) in Products.Instance.data)
        {
            var sceneInstance = (NextTaskScreenBox)next_task_screen_box.Instantiate();
            inventoryContainer.AddChild(sceneInstance);
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
        }
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
        // PlayerData.Instance.newDay();
        updateProductAmounts();
        if (DayInfo.Instance.isBuildingUnlockedToday())
        {
            ScreenManager.Instance.SwitchScreenPath(unlockScreen, false);
        }
        else
        {
            ScreenManager.Instance.SwitchScreenPath(gamePlayScreen, false);
        }
    }
}
