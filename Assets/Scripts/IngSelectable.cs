using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class IngSelectable : MonoBehaviour
{
    // ---------------------------------- BOOLS -------------------------------------------
    // Store if the GO has been placed
    public bool isPlaced = false;

    // ---------------------------------- RAYCAST -----------------------------------------
    // To specify layers to use in Physics.Raycast
    public LayerMask layerMask;

    // ---------------------------------- GO ----------------------------------------------
    // Reference to Grid Feedback GO
    public GameObject gFeedback;

    // Store the ingredient selected UI/Feedback
    public GameObject pIngrSelected;


    // ---------------------------------- SCRIPTS -----------------------------------------
    // Reference to Game Controller script
    public GameController GameControllerScript;


    // ---------------------------------- ARRAYS ------------------------------------------
    // Store all the ingredient chips sprites when placed in the Grid
    public Sprite[] gIngrPicked;

    // Store all the ingredient chips sprites when placed in the Grid
    public Sprite[] gIngrPlaced;

    // Store pantry containers
    public GameObject[] pContainers;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    private void Start()
    {
        // ---------------------------------- ACCESS ---------------------------------------------------
        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();

        // Access the Ingredient Selected GO (scene)
        pIngrSelected = GameObject.Find("Ingredient Selected"); ;

        // Access the Grid Feedback GO
        gFeedback = GameObject.Find("Grid Feedback");

        // Store pantry containers
        pContainers[0] = GameObject.Find("Pantry 1");
        pContainers[1] = GameObject.Find("Pantry 2");
        pContainers[2] = GameObject.Find("Pantry 3");
        pContainers[3] = GameObject.Find("Pantry 4");
    }


    public void OnMouseDown()
    {
        Debug.Log("1 - Entered the Place Ingredient in Grid method (IngSelectable/PlaceIngrGrid)");

        // ---------------------------------- PICK INGREDIENT FROM GRID --------------------------------
        if (GameController.emptyCursor)
        {
            Debug.Log("2 - Enter Pick item (IngSelectable/PlaceIngrGrid/1erIf)");

            // Set a Pick Up index to check which Ingredient is being picked
            int pickUpIndex = 0;

            // Mark as something has been selected (occupy the cursor) with an ingredient
            GameController.emptyCursor = false;

            // ---------------------------------- EMPTY GRID ITEMS ------------------------------------------
            // Loop through every child of the ingredient
            foreach (Transform child in transform)
            {
                Debug.Log("a");

                // Draw a raycast
                RaycastHit hit;

                // Check if the raycasts hit
                if (Physics.Raycast(child.position, -transform.forward, out hit, 200f, layerMask))
                {
                    Debug.Log("b");

                    // Check if: (Grid Item hitted exists) & (Grid Item hitted is not empty)
                    if (hit.transform.GetComponent<GridItem>() != null && !hit.transform.GetComponent<GridItem>().gridIsEmpty)
                    {
                        Debug.Log("c");

                        // Set the hitted Grid Items as empty
                        hit.transform.GetComponent<GridItem>().gridIsEmpty = true;
                    }
                }

            }

            // Set the Selected Ingredient as the one picked from the grid
            //GameController.selectedIngr = this;


            // ---------------------------------- CHECK WHICH INGREDIENT PICKED ------------------------------
            // If the ingredient selected is a Tomato
            if (this.gameObject.tag == "Tomato")
            {
                Debug.Log("d");

                pickUpIndex = 0;
            }

            // If the ingredient selected is a Carrot
            else if (this.gameObject.tag == "Carrot")
            {
                Debug.Log("e");

                pickUpIndex = 1;
            }

            // If the ingredient selected is a Eggplant
            else if (this.gameObject.tag == "Eggplant")
            {
                Debug.Log("f");

                pickUpIndex = 2;
            }

            // If the ingredient selected is a Mushroom
            else if (this.gameObject.tag == "Mushroom")
            {
                Debug.Log("g");

                pickUpIndex = 3;
            }

            Debug.Log("h");

            Debug.Log(pickUpIndex);

            // ---------------------------------- SIMULATE INGREDIENT PICKED ----------------------------------------------
            // Spawn an Ingredient grid chip in the cursor (Grid Feedback)
            GameControllerScript.SpawnIngredientGrid(pickUpIndex);

            Debug.Log("i");

            // Simulate an ingredient clicked
            pContainers[pickUpIndex].GetComponent<Clicker>().isClicked = true;

            Debug.Log("j");

            // Active the renderer of the ingredient selected
            pIngrSelected.GetComponent<SpriteRenderer>().enabled = true;

            Debug.Log("k");

            // Update the pantry's ingredients chips (block/available to interact, set as selected...)
            pContainers[pickUpIndex].GetComponent<Clicker>().PIngredientUpdate();


            // ---------------------------------- DESTROY INGREDIENT PICKED -----------------------------------------------
            Debug.Log("l");

            // Destroy the Ingredient picked up (to change it for the new one spawned before)
            Destroy(this.gameObject);
        }


        // ---------------------------------- PLACE INGREDIENTS (GRID) -----------------------------------------------------
        else if (GameController.emptyCursor == false && GameController.selectedIngr == this)
        {
            // ---------------------------------- DROP INGREDIENT ------------------------------------------
            // If cursor is occupied & the selected object is this GO
            if (GameController.emptyCursor == false && GameController.selectedIngr == this)
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

                        Debug.Log("Higos con bacon");

                        // Change the parent of this GO (from drawer grid to inventory grid)
                        transform.parent = hit.transform.parent;

                        // Set the Ingredient as placed
                        isPlaced = true;

                        // When placed, there's no selected ingredient
                        GameController.selectedIngr = null;

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
    }


    // ---------------------------------- PLACE INGREDIENTS (GRID) -----------------------------------------------------
    /*public void PlaceIngrGrid()
    {
        // ---------------------------------- DROP INGREDIENT ------------------------------------------
        // If cursor is occupied & the selected object is this GO
        if (GameController.emptyCursor == false && GameController.selectedIngr == this)
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

                    Debug.Log("Higos con bacon");

                    // Change the parent of this GO (from drawer grid to inventory grid)
                    transform.parent = hit.transform.parent;

                    // Set the Ingredient as placed
                    isPlaced = true;

                    // When placed, there's no selected ingredient
                    GameController.selectedIngr = null;

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
    }*/


    // ---------------------------------- CHANGE INGREDIENT SPRITES -----------------------------------------------------
    public void ChangeSprite ()
    {
        // If the ingredient placed is a Tomato
        if (this.gameObject.tag == "Tomato")
        {
            // Check it's a Tomato picked (cursor)
            if (this.GetComponent<SpriteRenderer>().sprite == gIngrPicked[0])
            {
                // Update its sprite to the Tomato placed sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[0];
            }

            // Check if it's a Tomato placed (grid)
            else if (this.GetComponent<SpriteRenderer>().sprite == gIngrPlaced[0])
            {
                // Update its sprite to the Tomato picked sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPicked[0];
            }
        }

        // If the ingredient placed is a Carrot
        else if (this.gameObject.tag == "Carrot")
        {
            // Check it's a Carrot picked (cursor)
            if (this.GetComponent<SpriteRenderer>().sprite == gIngrPicked[1])
            {
                // Update its sprite to the Carrot placed sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[1];
            }

            // Check if it's a Carrot placed (grid)
            else if (this.GetComponent<SpriteRenderer>().sprite == gIngrPlaced[1])
            {
                // Update its sprite to the Carrot picked sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPicked[1];
            }
        }

        // If the ingredient placed is a Eggplant
        else if (this.gameObject.tag == "Eggplant")
        {
            // Check it's a Eggplant picked (cursor)
            if (this.GetComponent<SpriteRenderer>().sprite == gIngrPicked[2])
            {
                // Update its sprite to the Tomato Eggplant sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[2];
            }

            // Check if it's a Eggplant placed (grid)
            else if (this.GetComponent<SpriteRenderer>().sprite == gIngrPlaced[2])
            {
                // Update its sprite to the Eggplant Eggplant sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPicked[2];
            }
        }

        // If the ingredient placed is a Mushroom
        else if (this.gameObject.tag == "Mushroom")
        {
            // Check it's a Mushroom picked (cursor)
            if (this.GetComponent<SpriteRenderer>().sprite == gIngrPicked[3])
            {
                // Update its sprite to the Mushroom placed sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPlaced[3];
            }

            // Check if it's a Mushroom placed (grid)
            else if (this.GetComponent<SpriteRenderer>().sprite == gIngrPlaced[3])
            {
                // Update its sprite to the Mushroom placed sprite
                this.GetComponent<SpriteRenderer>().sprite = gIngrPicked[3];
            }
        }
    }
}
