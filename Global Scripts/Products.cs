using System;
using System.Collections.Generic;
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

public class ProductData
{
    public Texture2D icon;
    public Texture2D logo;
}

public partial class Products : Node
{
    public static Products Instance { get; private set; }

    public Dictionary<ProductType, ProductData> data = new() { };
    public Dictionary<ProductType, int> productNeeds;

    [Export]
    public SpriteFrames productIcons { get; set; }

    [Export]
    public SpriteFrames productLogos { get; set; }

    public override void _Ready()
    {
        Instance = this;
        productNeeds = DayInfo.Instance.Days[0].ProductNeeds;

        foreach (ProductType product in Enum.GetValues<ProductType>())
        {
            data[product] = new ProductData
            {
                icon = productIcons.GetFrameTexture(product.ToString(), 0),
                logo = productLogos.GetFrameTexture(product.ToString(), 0),
            };
        }
    }

    public bool checkIfProductsAreSatisfied()
    {
        foreach (var (_product, value) in productNeeds)
        {
            if (value != 0)
                return false;
        }

        return true;
    }
}
