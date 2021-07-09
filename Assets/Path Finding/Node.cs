using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node 
{
    public Vector2Int coordinates;
    public bool isWalkable; // what is available to transform/translate to
    public bool isExplored; // what has been looked at
    public bool isPath;     // is path
    public Node connectedTo; // the nodes the current node is connected to

   


    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }

  

}
