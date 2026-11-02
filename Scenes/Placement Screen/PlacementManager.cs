using System;
using System.Collections.Generic;
using Godot;

public partial class PlacementManager : Node2D
{
    [Export]
    public Color GridColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    public float cellsize = 32;

    private TileMapLayer GridPlacement;

    private Vector2I hoveredcell;

    private bool mouseingrid;

    //So we need a dictionary because we need to know WHAT we are moving IF we are actually going to implement moving/deleting
    private Dictionary<Vector2I, Node2D> occupiedcells = new Dictionary<Vector2I, Node2D>();

    public override void _Ready()
    {
        PlacementData.Instance.placementManager = this;
        GridPlacement = GetNode<TileMapLayer>("GridPlacement");
    }

    public override void _Process(double delta)
    {
        //Finding the hovered cell and deciding whether to show the preview or not.
        hoveredcell = GetMouseCell();
        mouseingrid = IsCellInsideGrid(hoveredcell);

        QueueRedraw();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        //ignore placement and hide during gameplay
        if (
            !PlacementData.Instance.PlacementEnabled
            || @event is not InputEventMouseButton mouseEvent
            || mouseEvent.ButtonIndex != MouseButton.Left
            || !mouseEvent.Pressed
        )
        {
            return;
        }

        Vector2I clickedCell = GetMouseCell();

        //Says no to any clicks outside and modified to include occupancy for multiple slots.
        if (!CanPlaceBuilding(clickedCell))
        {
            return;
        }

        //Create the actual building and spawn it at the center of the cells. 2x2 etc. so not like on click.
        Node2D building = PlacementData.Instance.buildingToSpawn.Instantiate<Node2D>();

        //centering
        Vector2I cencell = clickedCell + PlacementData.Instance.BuildingSize - new Vector2I(1, 1);

        Vector2 buildingPosition = ToGlobal(
            (GetCellCenter(clickedCell) + GetCellCenter(cencell)) / 2f
        );

        //Basically putting it down/making sure that the cell is actually recorded and saved.
        for (int x = 0; x < PlacementData.Instance.BuildingSize.X; x++)
        {
            for (int y = 0; y < PlacementData.Instance.BuildingSize.Y; y++)
            {
                Vector2I cell = clickedCell + new Vector2I(x, y);
                occupiedcells.Add(cell, building);
            }
        }
        PlacementData.Instance.buildingContainer.AddChild(building);
        building.GlobalPosition = buildingPosition;

        //Reducing money only after the building has been placed.
        PlayerData.Instance.money -= PlacementData.Instance.buildingcost;
        PlacementData.Instance.PlacementEnabled = false;

        QueueRedraw();
    }

    private bool IsCellOccupied(Vector2I cell)
    {
        //checking our dictionary with the saved thing that we did before
        return occupiedcells.ContainsKey(cell);
    }

    //Placing the building. THIS IS NOT THE PREVIEW.
    private bool CanPlaceBuilding(Vector2I clickcell)
    {
        for (int x = 0; x < PlacementData.Instance.BuildingSize.X; x++)
        {
            for (int y = 0; y < PlacementData.Instance.BuildingSize.Y; y++)
            {
                Vector2I cell = clickcell + new Vector2I(x, y);

                //double check form aking we are in it and if something is in it.
                if (!IsCellInsideGrid(cell) || IsCellOccupied(cell))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static bool IsCellInsideGrid(Vector2I cell)
    {
        //so this is my old code...basically outlines the boundaries
        return cell.X >= 2 && cell.X < 16 && cell.Y >= 2 && cell.Y < 10;
    }

    private Vector2I GetMouseCell()
    {
        Vector2 mouseposition = GridPlacement.GetLocalMousePosition();

        return GridPlacement.LocalToMap(mouseposition);
    }

    private Vector2 GetCellCenter(Vector2I cell)
    {
        //Converting because we want to use tilemaps? Guess so.
        Vector2 gridposition = GridPlacement.MapToLocal(cell);
        Vector2 worldposition = GridPlacement.ToGlobal(gridposition);
        return ToLocal(worldposition);
    }

    public override void _Draw()
    {
        draw_grid();

        //more hiding stuff ---and then added || because of the no select bug. Links to unhandled input which should reject placement.
        if (!PlacementData.Instance.PlacementEnabled || !mouseingrid)
        {
            return;
        }
        //Since the rectangles origin is 0,0 they are drawn top left. This means we need to divide from the axis.
        //Preview not artual building placement.

        Vector2 cellposition = GetCellCenter(hoveredcell) - (new Vector2(cellsize, cellsize) / 2f);

        //I dont know if we need this resize actually. It might double scale
        Vector2 size = (Vector2)PlacementData.Instance.BuildingSize * cellsize;
        Rect2 preview = new Rect2(cellposition, size);

        Color outlinecolor = Colors.Green;

        //modified this originally because we aren't dealing in soley 1x1 vectors anyrmore.

        if (!CanPlaceBuilding(hoveredcell))
        {
            outlinecolor = Colors.Red;
        }

        Color fillcolor = outlinecolor;
        fillcolor.A = 0.4f;

        PlacementData.Instance.PlacementTexture.Draw(GetCanvasItem(), cellposition);

        DrawRect(preview, fillcolor);
        DrawRect(preview, outlinecolor, false, 2f);
    }

    public void draw_grid()
    {
        //Doing our vertical lines
        for (int x = 2; x <= 16; x++)
        {
            DrawLine(
                new Vector2(x * cellsize, 2 * cellsize),
                new Vector2(x * cellsize, 10 * cellsize),
                GridColor
            );
        }

        //Horizontal line drawing
        for (int y = 2; y <= 10; y++)
        {
            DrawLine(
                new Vector2(2 * cellsize, y * cellsize),
                new Vector2(16 * cellsize, y * cellsize),
                GridColor
            );
        }
    }
}
