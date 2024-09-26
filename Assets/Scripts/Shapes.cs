using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour
{
    // ---------------------------------- ARRAYS -------------------------------------------------
    // Array to store all Backpack's grid items/spaces
    public GameObject[] bShapes;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- SET ARRAYS ----------------------------------------
        // Store all the Shapes in the array
        bShapes[0] = transform.GetChild(0).gameObject;
        bShapes[1] = transform.GetChild(1).gameObject;
        bShapes[2] = transform.GetChild(2).gameObject;
        bShapes[3] = transform.GetChild(3).gameObject;
    }
}
