using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] [Range(0, 5)] float Speed;

    List<Node> path = new List<Node>();
   
 
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    // Update is called once per frame
    void OnEnable()
    {
        RecalculatePath();
        returnToStart();
        StartCoroutine(FollowPath());
    }
     void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath()
    {
        
        path.Clear();

        path = pathFinder.GetNewPath();
    }
 


    void returnToStart()
    {
        //set the position to the first element in our list
        transform.position = gridManager.GetWorldPositionFromCoordinates(pathFinder.StartCoordinates);
    }
    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
   
    IEnumerator FollowPath()     
    {

      
       for(int i = 0; i < path.Count; i++)  // loops through the list of waypoints and executes script for each waypoint in path 1 time
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetWorldPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)   // while we are not at our end position do the following
            {
                travelPercent += Time.deltaTime * Speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); 
                yield return new WaitForEndOfFrame();
            }

        }

    FinishPath();
    }
}
