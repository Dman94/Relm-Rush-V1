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
    [SerializeField] Color ExoploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    GridManager gridManager;


    TextMeshPro label;
    Vector2Int Coordinate = new Vector2Int();
  

    





    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
      

       
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
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(Coordinate);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = ExoploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
    void DisplayCoordinate()
    {                                                            
        if (gridManager == null) { return; }                                                       
        Coordinate.x = Mathf.RoundToInt(transform.parent.position.x /gridManager.UnityGridSize);
        Coordinate.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
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
