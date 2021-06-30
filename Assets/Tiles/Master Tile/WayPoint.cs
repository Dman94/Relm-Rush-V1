using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;
 
    public bool IsPlaceable {get{return isPlaceable;}}



    void OnMouseDown()
    {
   
       /* if (IsPlaceable)
        {
            Debug.Log("Hello!!");
            towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = false;
        }*/
       
        if (IsPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
 
    
}
