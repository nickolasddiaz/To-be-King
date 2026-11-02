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
    public int amount;
    public Texture2D icon;
    public Texture2D logo;
}

public partial class Products : Node
{
    public static Products Instance { get; private set; }

    public Dictionary<ProductType, ProductData> data = new() { };

    [Export]
    public SpriteFrames productIcons { get; set; }

    [Export]
    public SpriteFrames productLogos { get; set; }

    public override void _Ready()
    {
        Instance = this;

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
        foreach (var (_key, value) in data)
        {
            if (value.amount != 0)
                return false;
        }

        return true;
    }
}
