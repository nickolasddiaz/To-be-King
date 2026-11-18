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

    [Export(PropertyHint.File, "*.tscn")]
    public string gamePlayScreen { get; set; } = string.Empty;

    public override async void _Ready()
    {
        Instance = this;
        LongSceneManagerCs.Instance.MaxCacheSize = 20;
        LongSceneManagerCs.Instance.PreloadScene(gamePlayScreen);
    }

    public bool isInventoryNotFull()
    {
        return inventory.Count >= inventorySize;
    }

}
