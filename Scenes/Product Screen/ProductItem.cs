using System;
using Godot;

public partial class ProductItem : Control
{
    [Export(PropertyHint.File, "*.tscn")]
    private TextureRect productPic;
    private Label productLabel;

    public override void _Ready()
    {
        productPic = GetNode<TextureRect>("%Sprite2D");
        productLabel = GetNode<Label>("%Label");
    }

    public void setTexture(Texture2D pic)
    {
        productPic.Texture = pic;
    }

    public void setText(string text)
    {
        productLabel.Text = text;
    }
}
