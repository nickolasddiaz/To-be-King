using System.Threading.Tasks;
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

    // called when scene is transitioned to this
    public void updateProductAmounts()
    {
        foreach (ProductType product in productItems.Keys)
        {
            productItems[product].Hide();
        }
        foreach ((ProductType product, int number) in DayInfo.Instance.getToday().ProductNeeds)
        {
            productItems[product].setText($"{product}: {number}");
            productItems[product].Show();
        }

    }

    private async void OnButtonPressed()
    {
        // PlayerData.Instance.newDay();
        updateProductAmounts();
        if (DayInfo.Instance.isBuildingUnlockedToday())
        {
            await LongSceneManagerCs.Instance.SwitchScene(
                unlockScreen,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: true, // rebuilds the building unlocked based on the day
                "no_transition"
            );
        }
        else
        {
            await LongSceneManagerCs.Instance.SwitchScene(
                gamePlayScreen,
                LongSceneManagerCs.LoadMethod.BothPreloadFirst,
                cacheCurrentScene: true,
                "no_transition"
            );
            PlacementData.Instance.enterPlacement();
        }
    }
}
