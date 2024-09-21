using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    // ---------------------------------- ARRAYS -------------------------------------------------
    // Array to store all Backpack's grid items/spaces
    public GameObject[] bGridItems;


    // ---------------------------------- SCRIPTS ------------------------------------------------
    // Access Clicker script
    public Clicker ClickerScript;

    // Access Grid Item script
    public GridItem GridItemScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- SET ARRAYS ----------------------------------------
        // Store in the bGridItems array all the GO with the tag Grid item
        bGridItems = GameObject.FindGameObjectsWithTag("Grid item");

        // ---------------------------------- ACCESS --------------------------------------------
        // Access the Clicker script
        ClickerScript = FindObjectOfType<Clicker>();

        // Access the Grid Item script
        GridItemScript = FindObjectOfType<GridItem>();
    }


    // ---------------------------------- INGREDIENT DETECTION -----------------------------------------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        // LOOP through each GRID ITEM of the ARRAY bGridItems
        for (int i = 0; i < bGridItems.Length; i = i + 1)
        {
            // And IF the Grid Item which COLLIDED with this TRIGGER is TAGGED as "Shape" & that GRID ITEM is EMPTY
            if (collision.gameObject.CompareTag("Shape") && GridItemScript.isEmpty == true)
            {
                // Put the ingredient in that grid item

                // Occupy this item
                GridItemScript.isEmpty = false;
            }
        }
    }
}
