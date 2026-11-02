using System.Collections.Generic;
using Godot;

public class Recipe
{
    public ProductType[] input;
    public ProductType output;

    public float timeProduce;

    public Recipe(ProductType[] input, ProductType output, float timeProduce)
    {
        this.input = input;
        this.output = output;
        this.timeProduce = timeProduce;
    }
}

public abstract partial class ABuilding : Node2D
{
    [Export]
    public int Cost { get; set; } = 100;

    [Export]
    public Vector2 Size { get; set; } = new Vector2(1, 1);

    public virtual BuildingType buildingType { get; set; }

    public Recipe[] recipes;

    public Queue<ProductType> InputInventory = new();
    public ProductType? outputInventory;

    public int lockedRecipeIndex = -1;

    public AnimatedSprite2D baseFrames;

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

    public virtual void Interact(Player player)
    {
        OnInteract();
    }

    public void drawProducts(Vector2 pos)
    {
        float y_offset = 0;
        foreach (var product in InputInventory)
        {
            var icon = Products.Instance.data[product].icon;

            Vector2 drawPos = pos + new Vector2(0, y_offset);

            DrawTexture(icon, drawPos);

            y_offset += icon.GetSize().Y + 5; // 5px padding
        }
    }

    public void interactWithPlayer()
    {
        var player = PlayerData.Instance;

        if (outputInventory != null && player.isInventoryNotFull())
        {
            player.inventory.Enqueue(outputInventory.Value);
            outputInventory = null;
        }

        if (lockedRecipeIndex == -1 && player.inventory.Count > 0)
        {
            for (int index = 0; index < recipes.Length; index++)
            {
                var recipe = recipes[index];
                if (recipe.input[0] == player.inventory.Peek())
                {
                    lockedRecipeIndex = index;
                    break;
                }
            }
        }

        if (lockedRecipeIndex == -1)
        {
            return;
        }

        while (
            player.inventory.Count > 0
            && InputInventory.Count == recipes[lockedRecipeIndex].input.Length
            && recipes[lockedRecipeIndex].input[InputInventory.Count] == player.inventory.Peek()
        )
        {
            InputInventory.Enqueue(player.inventory.Dequeue());
        }
    }
}
