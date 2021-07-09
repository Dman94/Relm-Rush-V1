using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//stores refferance to all nodes and organizes them into grid
public class GridManager : MonoBehaviour
{

    [SerializeField] Vector2Int gridSize;

    [Tooltip("World Grid Size -- should match UnityEditor Snap Settings")]
    [SerializeField] int unityGridSize = 10 ;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } } // created a property to keep the origanl from being tampered with

    Tile tile;

    void Awake()
    {
        tile = FindObjectOfType<Tile>();
        CreateGrid();
    }




    public Node GetNode(Vector2Int Coordinates)
    {
        if (Grid.ContainsKey(Coordinates))
        {
            return Grid[Coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int Coordinates)
    {
        if (Grid.ContainsKey(Coordinates))
        {
            Grid[Coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition( Vector3 Position)
    {
        Vector2Int Coordinates = new Vector2Int();
        Coordinates.x = Mathf.RoundToInt(Position.x / unityGridSize);
        Coordinates.y = Mathf.RoundToInt(Position.z / unityGridSize);

        return Coordinates;
    }

    public Vector3 GetWorldPositionFromCoordinates(Vector2Int Coordinates)
    {
        Vector3 WorldPosition = new Vector3();
        WorldPosition.x = Coordinates.x * unityGridSize;
        WorldPosition.z = Coordinates.y * unityGridSize;

        return WorldPosition;
    }


    void CreateGrid()
    {
        for (int X = 0; X < gridSize.x; X++) // every x value looped through will loop through it's Y column elements and add them to grid
        {
            for (int Y = 0; Y < gridSize.y; Y++)
            {
                Vector2Int coordinates = new Vector2Int(X, Y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}

