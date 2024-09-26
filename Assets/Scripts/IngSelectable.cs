using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngSelectable : MonoBehaviour
{
    // ---------------------------------- RAYCAST -----------------------------------------
    // To specify layers to use in Physics.Raycast
    public LayerMask layerMask;


    // ---------------------------------- SCRIPTS -----------------------------------------
    // Access Game Controller script
    public GameController GameControllerScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    private void Start()
    {
        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();
    }


    // ---------------------------------- PLACE INGREDIENTS (GRID) -----------------------------------------------------
    public void PlaceIngrGrid()
    {
        Debug.Log("1");

        if (GameController.emptyCursor)
        {
            //
            Debug.Log("2");
        }

        // If cursor is occupied & the selected object is this GO
        else if (GameController.emptyCursor == false && GameController.selectedIngr == this)
        {
            Debug.Log("3");

            // Store if the GO is placeable
            bool ingCanBPlaced = true;

            // Loop through all the childs of the transform
            foreach (Transform child in transform)
            {
                Debug.Log("4");

                // Draw the raycast
                RaycastHit hit;

                // Draw raycast in X direction (out hit = save hit, what we collide with)
                if (Physics.Raycast(child.position, -transform.forward, out hit, 20f, layerMask))
                {
                    Debug.Log("5");

                    // If the Grid you collide with is not empty
                    if (!hit.transform.GetComponent<GridItem>().gridIsEmpty)
                    {
                        Debug.Log("6");

                        // You can't place the object
                        ingCanBPlaced = false;

                        // If not able to fulfill, exits foreach and goes to the condition associated
                        break;
                    }
                }

                // If the raycast doesn't collide with anything
                else
                {
                    Debug.Log("7");

                    // You can't place the object
                    ingCanBPlaced = false;

                    // If not able to fullfill, exits foreach and goes to the condition associated
                    break;
                }

            }

            // If the GO is placeable --> already placing the GO
            if (ingCanBPlaced)
            {
                Debug.Log("8");

                // The GO is not selected anymore
                //objectIsSelected = false;

                // Cursor is empty (can select another GO)
                GameController.emptyCursor = true;

                // Empty the selected object (none is selected)
                GameController.selectedIngr = null;

                // 
                GameObject mainChild = null;

                // Loop through all the childs of the transform
                foreach (Transform child in transform)
                {
                    Debug.Log("9");

                    // If the child looped has the tag "Main Hijo"
                    if (child.CompareTag("Main Child"))
                    {
                        Debug.Log("10");

                        // Store this GO in the mainChild
                        mainChild = child.gameObject;

                        // Exit foreach
                        break;
                    }
                }

                // Draw raycast
                RaycastHit hit;

                // Draw raycast in X direction (out hit = save hit, what we collide with) only for the Main Child
                if (Physics.Raycast(mainChild.transform.position, -transform.forward, out hit, 20f, layerMask))
                {
                    Debug.Log("12");

                    // Position of Grid hitted --> to place the GO
                    transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, -10);

                    // Change the parent of this GO (from drawer grid to inventory grid)
                    transform.parent = hit.transform.parent;

                }

                // Loop through all the childs of the transform
                foreach (Transform child in transform)
                {
                    Debug.Log("13");

                    // Draw another raycast
                    RaycastHit hit2;

                    // Draw the raycast from each child (out hit = save hit, what we collide with)
                    if (Physics.Raycast(child.position, -transform.forward, out hit2, 20f, layerMask))
                    {
                        Debug.Log("14");

                        // Store the Grid occupied as not empty
                        hit2.transform.GetComponent<GridItem>().gridIsEmpty = false;
                    }
                }
            }
        }
    }

}
