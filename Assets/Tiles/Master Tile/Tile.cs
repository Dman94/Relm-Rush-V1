using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
     [SerializeField] bool isplaceable;
    public bool isPlaceable { get { return isplaceable; } }
     [SerializeField] Tower towerPrefab;
 
    

       GridManager gridManager;
       PathFinder pathFinder;
       Vector2Int Coordinates = new Vector2Int();
       Node thisNode;

     void Awake()
    {
        isplaceable = true;
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
       
    }





     void Start()
    {
      
        if(gridManager != null)
        {
            Coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isplaceable)
            {
                gridManager.BlockNode(Coordinates);
            }
        }
    }



    void OnMouseDown()
    {
        
       
        if (gridManager.GetNode(Coordinates).isWalkable && !pathFinder.WillBlockPath(Coordinates))
        {

               isplaceable = false;
               bool isSuccessfull = towerPrefab.CreateTower(towerPrefab, transform.position);
             
               if (isSuccessfull)
               {
                   gridManager.BlockNode(Coordinates);
                   pathFinder.NotifyReceivers();
               }

        }
    }
 
    
}
