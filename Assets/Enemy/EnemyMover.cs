using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    // we want to pass in some waypoints the enemy can follow and then it's going to loop through this list and print out the path in the console


    [SerializeField] List<WayPoint> path = new List<WayPoint>();
   
 
    [SerializeField] [Range(0,5)] float Speed;

    Enemy enemy;

    // Update is called once per frame
    void OnEnable()
    {
        FindPath();
        returnToStart();
        StartCoroutine(FollowPath());
    }
     void Start()
    {
         enemy = FindObjectOfType<Enemy>();
    }

    void FindPath()
    {
        
        path.Clear(); 

        
        GameObject Parent = GameObject.FindGameObjectWithTag("Path"); 

        
        foreach(Transform child in Parent.transform)
        {
            WayPoint waypoint = child.GetComponent<WayPoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
               
            }
        }
    }
 


    void returnToStart()
    {
        //set the position to the first element in our list
        transform.position = path[0].transform.position;
    }
    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
   
    IEnumerator FollowPath()     
    {



      
        foreach (WayPoint waypoint in path)   // loops through the list of waypoints and executes script for each waypoint in path 1 time
        {
            Vector3 startPosition = transform.position;     
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)   // while we are not at our end position do the following
            {
                

                travelPercent += Time.deltaTime * Speed; // update / incriment the travel percent by the change in time multiplied by  the desired speed factor


                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); // lerp / move our enemy from start to end point


                // yield just means give up control and return means go back to the rest of the code
                // yield back to the update function until the end of the frame  has been completed, and then jump back to our coroutine which
                // will continue our while loop and go through these three lines again until travelpercent > 1
                // at which point our while loop will be broken out of and then we will go onto the next waypoint in our path
                yield return new WaitForEndOfFrame();
                
            }

        }



        FinishPath();
       
    }
}
