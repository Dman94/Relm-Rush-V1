using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{


    Queue<Node> Frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
   



    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    Vector2Int[] Directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }


    [SerializeField] Node currentSearchNode;
    Node StartNode;
    Node DestinationNode;
  

    GridManager gridManager;






    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
     

        if(gridManager != null)
        {
            grid = gridManager.Grid;
            StartNode = grid[StartCoordinates];
            DestinationNode = grid[DestinationCoordinates];
          
        }
    }






    void Start()
    {
        GetNewPath();
    }


    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        BuildPath();
        return BuildPath();
    }



    
    void BreadthFirstSearch()
    {
        StartNode.isWalkable = true;
        DestinationNode.isWalkable = true;
         
        Frontier.Clear();
        reached.Clear();


        bool isRunning = true;

        Frontier.Enqueue(StartNode);
        reached.Add(StartCoordinates, StartNode);

        while(Frontier.Count > 0 && isRunning == true)
        {
            currentSearchNode = Frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNieghbors();


               if(currentSearchNode.coordinates == DestinationCoordinates)
               {
                   isRunning = false;
               }
        }
    }



    void ExploreNieghbors()
    {
        List<Node> Nieghbors = new List<Node>();

         

        foreach (Vector2Int direction in Directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;
          

            if (grid.ContainsKey(neighborCoordinates))
            {
                Nieghbors.Add(grid[neighborCoordinates]);
               
            }


        }

        foreach (Node neighbor in Nieghbors)
        {
          

            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                Frontier.Enqueue(neighbor);

            }

        }

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = DestinationNode;

        path.Add(currentNode);
        currentNode.isPath = true; 

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int Coordinates)
    {
        if (grid.ContainsKey(Coordinates))
        {
            bool previousState = grid[Coordinates].isWalkable;
            grid[Coordinates].isWalkable = false;


            List<Node> newPath = GetNewPath();
            grid[Coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }

}
