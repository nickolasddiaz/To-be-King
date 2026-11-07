using Godot;

public partial class NextTaskScreenBox : Control
{
    private TextureRect productPic;
    private Label productLabel;

    public override void _Ready()
    {
        productLabel = GetNode<Label>("%Label");
        productPic = GetNode<TextureRect>("%TextureRect");
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
