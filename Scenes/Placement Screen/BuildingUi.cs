using System.Collections.Generic;
using Godot;

public partial class BuildingUi : CanvasLayer
{
    private GridContainer inventoryContainer;

    [Export]
    private PackedScene BuildingUIButton;

    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; }

    private readonly Dictionary<BuildingType, BuildingUiButton> buildingContainers = new();

    public override void _Ready()
    {
        inventoryContainer = GetNode<GridContainer>("%GridContainer");
        GetNode<Button>("%Continue").Pressed += PlacementData.Instance.leavePlacement;
        PlacementData.Instance.buildingUi = this;

        foreach (Node child in inventoryContainer.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var (buildingType, buildData) in Buildings.Instance.data)
        {
            int cost = buildData.Cost;
            string iconName = buildingType.ToString();

            Texture2D texture = buildData.IconTexture;

            BuildingUiButton sceneInstance = BuildingUIButton.Instantiate<BuildingUiButton>();
            inventoryContainer.AddChild(sceneInstance);

            sceneInstance.setText($"{cost}\n{buildingType}");
            sceneInstance.setTexture(texture);

            sceneInstance.productPic.Pressed += () =>
                PlacementData.Instance.selectBuilding(buildingType);

            // sceneInstance.Hide();
            buildingContainers[buildingType] = sceneInstance;
        }
    }

    public void updateBuildingAmounts()
    {
        if (DayInfo.Instance.getToday().UnlockableBuilding is BuildingType buildingType)
        {
             buildingContainers[buildingType].Show();
        }
    }
}
