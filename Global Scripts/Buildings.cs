using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public enum BuildingType
{
    Barley_Crop,
    Cabbage_Crop,
    Grape_Crop,
    Sheep_Pen,
    Cow_Pen,
    Bread_Oven,
    Distillery,
    Hut,
}

public class BuildingData
{
    public int Cost;
    public ImageTexture PlacementTexture;
    public Texture2D IconTexture;
    public PackedScene BuildingScene;
    public Vector2I Size;
    public bool unlocked;

    public Recipe[] recipes;

    public BuildingData(string Path)
    {
        BuildingScene = GD.Load<PackedScene>(Path);

        if (BuildingScene is null)
        {
            throw new InvalidOperationException($"Building's location is unknown: {Path}");
        }

        var instance = BuildingScene.Instantiate();
        if (instance is not ABuilding building)
        {
            throw new InvalidOperationException($"Scene root must inherit Building: {Path}");
        }

        recipes = building.recipes;
        Cost = building.Cost;
        Size = (Vector2I)building.Size;
        building.Free();
    }
}

public partial class Buildings : Node
{
    public Dictionary<BuildingType, BuildingData> data = new() { };

    [Export]
    public SpriteFrames buildingIcons { get; set; }

    public static Buildings Instance { get; private set; }

    public override async void _Ready()
    {
        Instance = this;
        foreach (BuildingType building in Enum.GetValues<BuildingType>())
        {
            var buildingData = new BuildingData($"res://Scenes/Buildings/{building}.tscn")
            {
                IconTexture = buildingIcons.GetFrameTexture(building.ToString(), 0),
            };

            buildingData.PlacementTexture = await RenderBuildingAsync(
                buildingData.BuildingScene,
                buildingData.Size
            );
            data[building] = buildingData;
        }
    }

    private async Task<ImageTexture> RenderBuildingAsync(PackedScene scene, Vector2I size)
    {
        var viewport = new SubViewport
        {
            Size = size * (int)ABuilding.BUILDINGUNITSIZE,
            TransparentBg = true,
            Disable3D = true,
            RenderTargetUpdateMode = SubViewport.UpdateMode.Once,
        };
        AddChild(viewport);

        var building = scene.Instantiate<ABuilding>();
        building.Position = new Vector2(viewport.Size.X, viewport.Size.Y) / 2f;
        viewport.AddChild(building);

        await ToSignal(RenderingServer.Singleton, RenderingServer.SignalName.FramePostDraw);

        Image image = viewport.GetTexture().GetImage();
        viewport.QueueFree();

        return ImageTexture.CreateFromImage(image);
    }
}
