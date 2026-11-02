using System;
using Godot;

public partial class BuildingUi : Control
{
    private GridContainer inventoryContainer;

    [Export]
    private PackedScene BuildingUIButton;

    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; }

    public override void _Ready()
    {
        inventoryContainer = GetNode<GridContainer>("%GridContainer");
        PlacementData.Instance.buildingContainer = inventoryContainer;
        GetNode<Button>("%Continue").Pressed += OnButtonPressed;

        foreach (Node child in inventoryContainer.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var (buildingType, buildData) in PlayerData.Instance.Buildings)
        {
            // Fetch cost/texture from a lightweight data source instead of instantiating the whole scene tree
            int cost = buildData.Cost;
            string iconName = buildingType.ToString();

            Texture2D texture = buildData.IconTexture;

            BuildingUiButton sceneInstance = BuildingUIButton.Instantiate<BuildingUiButton>();
            sceneInstance.setText($"{cost}\n{buildingType}");
            sceneInstance.setTexture(texture);
            sceneInstance.setBuildingType(buildingType);
            inventoryContainer.AddChild(sceneInstance);

            sceneInstance.GetNode<Button>("%Button").Pressed += () =>
                PlacementData.Instance.selectBuilding(buildingType);
        }
    }

    private void OnButtonPressed()
    {
        PlayerData.Instance.placementToGameplayTransition();
    }
}
