using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{


    Queue<Node> Frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();


    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    GridManager gridManager;

    [SerializeField] Node currentSearchNode;
    Node StartNode;
    Node DestinationNode;



    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }



    Vector2Int[] Directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };







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
        List<Node> Nieghbors = new List<Node>(); //instantiate a new list of reached nodes

         

        foreach (Vector2Int direction in Directions) //for each direction in the array of Directions
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction; // intatsaniate the neighbors coordinates to be our current position plus the direction (this will sum up to take a step on our grid)
          

            if (grid.ContainsKey(neighborCoordinates)) // if our grid dictionary contains the neighboring nodes coordinates
            {
                Nieghbors.Add(grid[neighborCoordinates]); // add the node to our list of Neighbors
            }


        }

        foreach (Node neighbor in Nieghbors) // for every node in our list of Neighbors
        {
          

            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable) // check our dictionary of reached nodes if it contains the coordinates to the current search node and if it is walkable
            {
                neighbor.connectedTo = currentSearchNode; // assign the current neighboring node being explored as the current search node
                reached.Add(neighbor.coordinates, neighbor); // add the node to our reached lst
                Frontier.Enqueue(neighbor);  // add this node to our queue
                 
            }

        }

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>(); // instantiate a new list of nodes to be our path
        Node currentNode = DestinationNode; // instantiate the current node to be our destination

        path.Add(currentNode); // add the destiination to our list of nodes to be our path
        currentNode.isPath = true; // assign this to be a true statement

        while(currentNode.connectedTo != null) // while the destination has a path to the start
        {
            currentNode = currentNode.connectedTo; // take a step from the destination to the starting node
            path.Add(currentNode);  //  add the current node to our path list
            currentNode.isPath = true; // assign the added node to be true in our list
        }

        path.Reverse(); // flip the list since it is built backwards

        return path; // return our built path
    }

    public bool WillBlockPath(Vector2Int Coordinates)
    {
        if (grid.ContainsKey(Coordinates))
        {
            bool previousState = grid[Coordinates].isWalkable;
            grid[Coordinates].isWalkable = false;


            List<Node> newPath = GetNewPath();
            //grid[Coordinates].isWalkable = previousState;

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
