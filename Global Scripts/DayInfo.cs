using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

public class Day
{
    [JsonPropertyName("unlockable_building")]
    public BuildingType? UnlockableBuilding { get; set; }

    [JsonPropertyName("product_needs")]
    public Dictionary<ProductType, int> ProductNeeds { get; set; }

    [JsonPropertyName("time")]
    public float Time { get; set; }

    [JsonPropertyName("number_of_money")]
    public int NumberOfMoneyGiven { get; set; }

    public Day() { }

    public Day(
        BuildingType unlockableBuilding,
        Dictionary<ProductType, int> productNeeds,
        float time,
        int numberOfMoneyGiven
    )
    {
        UnlockableBuilding = unlockableBuilding;
        ProductNeeds = productNeeds;
        Time = time;
        NumberOfMoneyGiven = numberOfMoneyGiven;
    }
}

public partial class DayInfo : Node
{
    public static DayInfo Instance { get; private set; }

    private static readonly JsonSerializerOptions DayJsonOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        PropertyNameCaseInsensitive = false,
    };

    [Export(PropertyHint.File, "*.json")]
    public string JsonDayFile { get; set; }
    public List<Day> Days;

    public int day;

    public override void _Ready()
    {
        Instance = this; // setting DayInfo static
        LoadDayInfo(JsonDayFile);
    }

    public Day getToday()
    {
        return Days[day];
    }

    public bool islastday()
    {
        return day == Days.Count;
    }

    public string formatDay()
    {
        return $"Day: ${day + 1}";
    }

    public BuildingType getTodaysBuilding()
    {
        return (BuildingType)Days[day].UnlockableBuilding;
    }

    public bool isBuildingUnlockedToday()
    {
        return Days[day].UnlockableBuilding != null;
    }

    private void LoadDayInfo(string filePath)
    {
        if (!FileAccess.FileExists(filePath))
        {
            throw new System.IO.FileNotFoundException(
                $"Config file not found at: {filePath}",
                filePath
            );
        }

        using var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        string jsonString = file.GetAsText();

        try
        {
            Days =
                JsonSerializer.Deserialize<List<Day>>(jsonString, DayJsonOptions)
                ?? throw new JsonException("Deserialized result was null.");
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Failed to parse DayInfo JSON: {ex.Message}");
        }
    }
}
