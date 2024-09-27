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
    // Reference to Game Controller script
    public GameController GameControllerScript;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Store all the ingredient chips sprites when placed in the Grid with a size of 4
    public Sprite[] gIngrPlaced;


    // ---------------------------------- BOOLS -------------------------------------------
    // Store if the GO has been placed
    public bool isPlaced = false;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    private void Start()
    {
        // ---------------------------------- ACCESS SCRIPTS -------------------------------------------
        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();
    }


    // ---------------------------------- PLACE INGREDIENTS (GRID) -----------------------------------------------------
    public void PlaceIngrGrid()
    {
        Debug.Log("1 - Entered the Place Ingredient in Grid method (IngSelectable/PlaceIngrGrid)");

        // ---------------------------------- PICK INGREDIENT ------------------------------------------
        if (GameController.emptyCursor)
        {
            //
            Debug.Log("2 - Enter Pick item (IngSelectable/PlaceIngrGrid/1erIf)");
        }

        // ---------------------------------- DROP INGREDIENT ------------------------------------------
        // If cursor is occupied & the selected object is this GO
        else if (GameController.emptyCursor == false && GameController.selectedIngr == this)
        {
            Debug.Log("3 - Enter drop item (IngSelectable/PlaceIngrGrid/1erElseIf)");

            // Store if the GO is placeable
            bool ingCanBPlaced = true;

            // Loop through all the childs of the transform
            foreach (Transform child in transform)
            {
                Debug.Log("4");

                // Draw the raycast
                RaycastHit hit;

                // Draw raycast in X direction (out hit = save hit, what we collide with)
                if (Physics.Raycast(child.position, -transform.forward, out hit, 200f, layerMask))
                {
                    Debug.Log("5 - Draw raycast (IngSelectable/PlaceIngrGrid/1erElseIf)");

                    // If the Grid you collide with is not empty
                    if (!hit.transform.GetComponent<GridItem>().gridIsEmpty)
                    {
                        Debug.Log("6 - Detect if the raycast (4) collides with an occupied grid item (IngSelectable/PlaceIngrGrid/1erElseIf)");

                        // You can't place the object
                        ingCanBPlaced = false;

                        // If not able to fulfill, exits foreach and goes to the condition associated
                        break;
                    }
                }

                // If the raycast doesn't collide with anything
                else
                {
                    Debug.Log("7 - Tell the Ingredient can't b placed when raycast doesn't collide");

                    // You can't place the object
                    ingCanBPlaced = false;

                    // If not able to fullfill, exits foreach and goes to the condition associated
                    break;
                }

            }


            // ---------------------------------- PLACE INGREDIENT ------------------------------------------
            // If the GO is placeable --> already placing the GO
            if (ingCanBPlaced)
            {
                Debug.Log("8 - Start placing ingredient (IngSelectable/PlaceIngrGrid/2ºIf)");

                // The GO is not selected anymore
                //objectIsSelected = false;

                // Cursor is empty (can select another GO)
                GameController.emptyCursor = true;

                // Empty the selected object (none is selected)
                GameController.selectedIngr = null;

                // Set a GO called mainChild (none is selected)
                GameObject mainChild = null;

                // Loop through all the childs of the transform
                foreach (Transform child in transform)
                {
                    Debug.Log("9");

                    // If the child looped has the tag "Main Hijo"
                    if (child.CompareTag("Main Child"))
                    {
                        Debug.Log("10 - Detect what point of the raycast is the Main Child (IngSelectable/PlaceIngrGrid/2ºIf)");

                        // Store this GO in the mainChild
                        mainChild = child.gameObject;

                        // Exit foreach
                        break;
                    }
                }

                // Draw raycast
                RaycastHit hit;

                // Draw raycast in X direction (out hit = save hit, what we collide with) only for the Main Child
                if (Physics.Raycast(mainChild.transform.position, -transform.forward, out hit, 200f, layerMask))
                {
                    Debug.Log("12 - Draw a Raycast from the Main Child to see where to place the Ingredient in the Grid (IngSelectable/PlaceIngrGrid/2ºIf)");

                    // Position of Grid hitted --> to place the GO
                    transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, 0);

                    // Change the parent of this GO (from drawer grid to inventory grid)
                    transform.parent = hit.transform.parent;

                    // Set the Ingredient as placed
                    isPlaced = true;

                    // When placed, change its sprite to the placed sprite
                    ChangeSprite();
                }

                // Loop through all the childs of the transform
                foreach (Transform child in transform)
                {
                    Debug.Log("13");

                    // Draw another raycast
                    RaycastHit hit2;

                    // Draw the raycast from each child (out hit = save hit, what we collide with)
                    if (Physics.Raycast(child.position, -transform.forward, out hit2, 200f, layerMask))
                    {
                        Debug.Log("14 - Occupy the Grid Items where the Ingredient has been placed (IngSelectable/PlaceIngrGrid/2ºIf)");

                        // Store the Grid occupied as not empty
                        hit2.transform.GetComponent<GridItem>().gridIsEmpty = false;
                    }
                }
            }
        }
    }


    // ---------------------------------- CHANGE INGREDIENT SPRITES -----------------------------------------------------
    public void ChangeSprite ()
    {
        // If the ingredient placed is a Tomato
        if (this.gameObject.tag == "Tomato")
        {
            // Update its sprite to the Tomato placed sprite
            this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[0];
        }

        // If the ingredient placed is a Carrot
        else if (this.gameObject.tag == "Carrot")
        {
            // Update its sprite to the Carrot placed sprite
            this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[1];
        }

        // If the ingredient placed is a Eggplant
        else if (this.gameObject.tag == "Eggplant")
        {
            // Update its sprite to the Tomato Eggplant sprite
            this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[2];
        }

        // If the ingredient placed is a Mushroom
        else if (this.gameObject.tag == "Mushroom")
        {
            // Update its sprite to the Mushroom placed sprite
            this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[3];
        }
    }
}
