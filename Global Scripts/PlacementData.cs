using System;
using Godot;

public partial class PlacementData : Node
{
    public Vector2I BuildingSize = new Vector2I(1, 1);
    public Texture2D PlacementTexture;
    public int buildingcost;
    public PackedScene buildingToSpawn;
    public bool PlacementEnabled;
    public PlacementManager placementManager;

    public Node buildingContainer;

    public static PlacementData Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void selectBuilding(BuildingType buildingType)
    {
        var build = PlayerData.Instance.Buildings[buildingType];
        PlacementEnabled = true;
        PlacementTexture = build.PlacementTexture;
        buildingcost = build.Cost;
        buildingToSpawn = build.BuildingScene;
        BuildingSize = build.Size;
    }
}
