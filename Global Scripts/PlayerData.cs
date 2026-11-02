using System;
using System.Collections.Generic;
using Godot;

public partial class PlayerData : Node
{
    public static PlayerData Instance { get; private set; }
    public int money = 5000;
    public int day;
    public PackedScene MySceneToSpawn;
    public EndOfDayScreen endOfDayScreen;
    public float time = 4; // seconds
    public bool startTimer;
    public Queue<ProductType> inventory = new();
    public int inventorySize = 3;

    public override async void _Ready()
    {
        Instance = this;
    }

    public bool isInventoryNotFull()
    {
        return inventory.Count >= inventorySize;
    }

    public void newDay()
    {
        var today = DayInfo.Instance.Days[day];
        money += today.NumberOfMoneyGiven;
        time = today.Time;
        for (int i = 0; i <= day; i++)
        {
            // isBuildingUnlockedDict[today.UnlockableBuilding] = true;
        }
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
            if (time <= 0.0f)
            {
                startTimer = false;
                gameplayToEndofDay();
            }
        }
    }
}
