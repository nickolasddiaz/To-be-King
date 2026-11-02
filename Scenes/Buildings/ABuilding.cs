using System;
using Godot;

public abstract partial class ABuilding : Node2D
{
    // Shared properties that every building MUST define in the Inspector
    [Export]
    public int Cost { get; set; } = 100;

    [Export]
    public Vector2 Size { get; set; } = new Vector2(1, 1);

    public AnimatedSprite2D baseFrames;

    public virtual BuildingType buildingType { get; set; }

    public const float BUILDINGUNITSIZE = 32.0f;

    public virtual ImageTexture getImageTexture()
    {
        return new ImageTexture();
    }

    public virtual void OnInteract() { }

    public override void _Ready()
    {
        baseFrames = GetNode<AnimatedSprite2D>("%BaseAnimatedSprite");
    }

    public override void _Process(double delta)
    {
        // Shared frame processing can go here
    }

    public virtual void Interact(Player player)
    {
        OnInteract();
    }

    protected ImageTexture helperGetTexture2D(Texture2D[] texture)
    {
        Image combinedImg = Image.CreateEmpty(
            (int)(Size.X * BUILDINGUNITSIZE),
            (int)(Size.Y * BUILDINGUNITSIZE),
            false,
            Image.Format.Rgba8
        );

        Image GetSubImage(Texture2D tex)
        {
            Image fullImg = tex.GetImage();
            if (tex is AtlasTexture atlasTex)
            {
                return fullImg.GetRegion((Rect2I)atlasTex.Region);
            }
            return fullImg;
        }

        foreach (var tex in texture)
        {
            Image img = GetSubImage(tex);
            combinedImg.BlendRect(img, new Rect2I(Vector2I.Zero, img.GetSize()), Vector2I.Zero);
        }
        return ImageTexture.CreateFromImage(combinedImg);
    }
}
