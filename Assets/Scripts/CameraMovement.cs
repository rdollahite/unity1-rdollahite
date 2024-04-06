using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour

{
     public float offsetX = 0.0f; 
     public float offsetY = 0.0f;
     public float boundaryY = -1.0f;
     public float negativeXBoundary = -5.0f;
     public float positiveXBoundary = 5.0f;
     
     private float yValue;
     private float xValue;

    [SerializeField] Transform character;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Chracter").transform;
        if (character == null) 
        {
            Debug.Log("Character gameobject not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        { 
            // set screen limits
            if (character.position.y < boundaryY) 
            {
                yValue = boundaryY;
            }
            else
            {
                yValue = character.position.y;
            }

            if (character.position.x < negativeXBoundary)
            {
                xValue = negativeXBoundary;
            }
            else if (character.position.x > positiveXBoundary)
            {
                xValue = positiveXBoundary;
            }
            else
            {
                xValue = character.position.x;
            }

            transform.position = new Vector3(xValue + offsetX, yValue + offsetY, transform.position.z);
        }
    }
}
