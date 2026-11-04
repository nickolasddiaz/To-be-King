using System;
using System.Collections.Generic;
using Godot;

public partial class PlayerData : Node
{
    public static PlayerData Instance { get; private set; }
    public int money = 5000;
    public PackedScene MySceneToSpawn;
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

    public void toPlacementScreen() { }

    public void toGamePlayScreen() { }

    public void toEndOfDayScreen() { }

    public void handleNewGame() { }

    public string formatTime()
    {
        TimeSpan elapsedTime = TimeSpan.FromSeconds(time);

        // Format as minute:second
        return string.Format("{0:D2}:{1:D2}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds);
    }
}
