using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridmanager;


    private void Start()
    {
        gridmanager = FindObjectOfType<GridManager>();
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);


            coordinates = gridmanager.GetCoordinatesFromPosition(transform.position);
            gridmanager.BlockNode(coordinates);
        }
       
        return false;
    }
   
}


/*
public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;

    public bool IsPlaceable { get { return isPlaceable; } }



    void OnMouseDown()
    {

        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }


}
*/