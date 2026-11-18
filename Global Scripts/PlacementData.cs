using Godot;

public partial class PlacementData : Node
{
	[Export(PropertyHint.File, "*.tscn")]
    public string endOfDayPath { get; set; } = string.Empty;
	public Vector2I BuildingSize = new Vector2I(1, 1);
	public Texture2D PlacementTexture;
	public int buildingcost;
	public PackedScene buildingToSpawn;
	public bool PlacementEnabled;
	public PlacementManager placementManager;
	public ProductScreen productScreen;

	public BuildingUi buildingUi;

	public Node2D buildingContainer;
	public GamePlayScene gamePlayScene;

	public bool isGamePlaying;
	public float time;

	public static PlacementData Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public void selectBuilding(BuildingType buildingType)
	{
		var build = Buildings.Instance.data[buildingType];
		PlacementEnabled = true;
		PlacementTexture = build.PlacementTexture;
		buildingcost = build.Cost;
		buildingToSpawn = build.BuildingScene;
		BuildingSize = build.Size;
	}

	public void enterPlacement()
	{
		placementManager.enterPlacement();
		gamePlayScene.hidePlayer();
		productScreen.updateProductAmounts();
		buildingUi.updateBuildingAmounts();
		isGamePlaying = false;
	}
	public void leavePlacement()
	{
		gamePlayScene.showPlayer();
		placementManager.leavePlacement();
		isGamePlaying = true;
		time = DayInfo.Instance.getToday().Time;
	}

	public void restartDay()
	{
		enterPlacement();
	}

	public void restartGame()
	{
		enterPlacement();
	}

	public async override void _Process(double delta)
	{
		if (isGamePlaying)
		{
			int tempTime = (int)time;
			time -= (float)delta;
			if (tempTime != (int)time)
				productScreen.setTime(time);

			if (time <= 0.0f)
			{
				isGamePlaying = false;
					await LongSceneManagerCs.Instance.SwitchScene(
					endOfDayPath,
					LongSceneManagerCs.LoadMethod.BothPreloadFirst,
					cacheCurrentScene: true,
					"no_transition"
				);
			}
		}
	}
}
