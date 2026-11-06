using System;
using Godot;

public partial class BuildingUi : CanvasLayer
{
    private GridContainer inventoryContainer;

    [Export]
    private PackedScene BuildingUIButton;

    [Export(PropertyHint.File, "*.tscn")]
    public string NextPath { get; set; }

    public override void _Ready()
    {
        inventoryContainer = GetNode<GridContainer>("%GridContainer");
        GetNode<Button>("%Continue").Pressed += OnButtonPressed;

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
        }
    }

    private void OnButtonPressed()
    {
        // PlayerData.Instance.placementToGameplayTransition();
    }
}
