using System;
using Godot;

public partial class BuildingUiButton : Control
{
    public Button productPic;

    private Label productLabel;

    public override void _Ready()
    {
        productPic = GetNode<Button>("%Button");
        productLabel = GetNode<Label>("%Label");
    }

    public static void onClick() { }

    public void setTexture(Texture2D pic)
    {
        productPic.Icon = pic;
    }

    public void setText(string text)
    {
        productLabel.Text = text;
    }
}
