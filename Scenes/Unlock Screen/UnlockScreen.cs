using System;
using Godot;

public partial class UnlockScreen : CanvasLayer
{
    [Export]
    public PackedScene next_task_screen_box { get; set; }

    [Export]
    public PackedScene arrowScene { get; set; }

    [Export(PropertyHint.File, "*.tscn")]
    public string gamePlayScreen { get; set; } = string.Empty;

    private NextTaskScreenBox BuildingBox;
    public VBoxContainer VBoxContainerRecipe;

    public override void _Ready()
    {
        BuildingBox = GetNode<NextTaskScreenBox>("%BuildingBox");
        VBoxContainerRecipe = GetNode<VBoxContainer>("%VBoxContainerRecipe");
        GetNode<Button>("%Continue").Pressed += OnButtonPressed;

        var building = DayInfo.Instance.getTodaysBuilding();
        var buildingInfo = Buildings.Instance.data[building];

        var recipes = buildingInfo.recipes;
        var product_data = Products.Instance.data;

        BuildingBox.setTexture(buildingInfo.PlacementTexture);
        BuildingBox.setText(building.ToString());

        foreach (Recipe recipe in recipes) // 0 or N: amount of recipes
        {
            var hbox = new HBoxContainer();
            VBoxContainerRecipe.AddChild(hbox);
            if (recipe.input.Length != 0) // 0 - N: amount of inputs
            {
                createArrow("Takes", hbox);
                foreach (ProductType product in recipe.input)
                {
                    createBox(product_data[product].logo, product.ToString(), hbox);
                }
            }

            createArrow("Produces", hbox);

            { // 1 output per recipe
                var product = recipe.output;
                createBox(product_data[product].logo, product.ToString(), hbox);
            }
        }
    }

    private void createArrow(string name, Node container)
    {
        var arrowInstance = (NextTaskScreenBox)arrowScene.Instantiate();
        container.AddChild(arrowInstance);
        arrowInstance.setText(name);
    }

    private void createBox(Texture2D texture, string name, Node container)
    {
        var sceneInstance = (NextTaskScreenBox)next_task_screen_box.Instantiate();
        container.AddChild(sceneInstance);
        sceneInstance.setTexture(texture);
        sceneInstance.setText(name);
    }

    private void OnButtonPressed()
    {
        ScreenManager.Instance.SwitchScreenPath(gamePlayScreen, false);
    }
}
