using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabler : MonoBehaviour
{


    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.grey;



    TextMeshPro label;
    Vector2Int Coordinate = new Vector2Int();
    WayPoint wayPoint;

    





    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;


        wayPoint = GetComponentInParent<WayPoint>();
        DisplayCoordinate();
    }
  


    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinate();
            updateObjectName();
            label.enabled = true;
        }

        
        SetLabelColor();
        ToggleLabels();
    }


    void SetLabelColor()
    {
        if(wayPoint.IsPlaceable)
          {
            label.color = defaultColor;
          }
        else
          {
            label.color = blockedColor;
          }
    }
    void DisplayCoordinate()
    {                                                               // We divide the parent position by the translation handle snap to be able to run this code inside the game and not just in the editor
                                                                    // we will take this out when it's time to build the game due to anything to do with unity editor in code upon build will not build
        Coordinate.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        Coordinate.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = Coordinate.x + " , " + Coordinate.y;
    }
    void updateObjectName()
    {
        transform.parent.name = Coordinate.ToString();
    }
    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}
