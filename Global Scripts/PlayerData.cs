using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public enum ProductType
{
    Barley,
    Cabbage,
    Grape,
    Wool,
    Milk,
    Bread,
    Wine,
    Ale,
}

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

        Cost = building.Cost;
        Size = (Vector2I)building.Size;
        building.Free();
    }
}

public partial class PlayerData : Node
{
    public Godot.Collections.Dictionary<ProductType, int> productAmounts =
        new Godot.Collections.Dictionary<ProductType, int>();
    public Godot.Collections.Dictionary<BuildingType, bool> isBuildingUnlockedDict =
        new Godot.Collections.Dictionary<BuildingType, bool>();
    public Godot.Collections.Dictionary<ProductType, Texture2D> productIconsDict = new() { };
    public Dictionary<BuildingType, BuildingData> Buildings = new() { };
    public int money = 5000;
    public int day;
    public PackedScene MySceneToSpawn;
    public EndOfDayScreen endOfDayScreen;
    public static PlayerData Instance { get; private set; } // setting PlayerData static
    public ProductScreen productScreen;
    public float time = 4; // seconds
    public bool startTimer;

    [Export]
    public SpriteFrames productIcons { get; set; }

    [Export]
    public SpriteFrames buildingIcons { get; set; }

    public override async void _Ready()
    {
        Instance = this; // setting PlayerData static
        foreach (ProductType product in Enum.GetValues<ProductType>())
        {
            Texture2D frameTexture = productIcons.GetFrameTexture(product.ToString(), 0);

            if (frameTexture == null)
            {
                throw new InvalidOperationException(
                    $"spr_productIcons.aseprite: A frame is null: {product}"
                );
            }

            productIconsDict[product] = frameTexture;
            productAmounts[product] = 0;
        }

        foreach (BuildingType building in Enum.GetValues<BuildingType>())
        {
            isBuildingUnlockedDict[building] = true;

            var data = new BuildingData($"res://Scenes/Buildings/{building}.tscn")
            {
                IconTexture = buildingIcons.GetFrameTexture(building.ToString(), 0),
            };
            Buildings[building] = data;

            data.PlacementTexture = await RenderBuildingAsync(data.BuildingScene, data.Size);
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

    public void newDay()
    {
        var today = DayInfo.Instance.Days[day];
        money += today.NumberOfMoneyGiven;
        time = today.Time;
        for (int i = 0; i <= day; i++)
        {
            isBuildingUnlockedDict[today.UnlockableBuilding] = true;
        }
        productScreen.setTimeText(formatTime());
        productScreen.updateProductAmounts();
    }

    public void placementToGameplayTransition()
    {
        PlacementData.Instance.PlacementEnabled = false;
        // placementManager.SetPlacementEnabled(false);

        // placementManager.GetNode<Player>("../Player").SetControlEnabled(true);

        // placementManager.Visible = false;
        // placementManager.ProcessMode = ProcessModeEnum.Disabled;

        startTimer = true;
    }

    public void gameplayToEndofDay()
    {
        PlacementData.Instance.PlacementEnabled = true;
        // placementManager.ProcessMode = ProcessModeEnum.Inherit;
        // placementManager.Visible = true;
        startTimer = false;
        const string endDayPath = "res://Scenes/end_of_day_screen.tscn";
        ScreenManager.Instance.SwitchScreenPath(endDayPath, false);
        endOfDayScreen.updateEndofDay();
    }

    public string formatTime()
    {
        TimeSpan elapsedTime = TimeSpan.FromSeconds(time);

        // Format as minute:second
        return string.Format("{0:D2}:{1:D2}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds);
    }

    public override void _Process(double delta)
    {
        if (startTimer)
        {
            time -= (float)delta;
            productScreen.setTimeText(formatTime());
            if (time <= 0.0f)
            {
                startTimer = false;
                gameplayToEndofDay();
            }
        }
    }
}
