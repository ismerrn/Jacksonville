using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HexGridItem : MonoBehaviour
{
    // ---------------------------------- BOOLS -------------------------------------------
    // Store if the Grid has been selected (last one)
    public bool isActive;


    // ---------------------------------- FLOAT -------------------------------------------
    // The Maximum Distance of the Adjacent Chips for any chip is 160px
    private float maxDistNeighborChips = 160f;


    // ---------------------------------- RAYCAST -----------------------------------------
    // To specify layers to use in Physics.Raycast
    public LayerMask layerMask;


    // ---------------------------------- SCRIPTS -----------------------------------------
    // Access Game Controller script
    public GameController GameControllerScript;


    // ---------------------------------- SPRITES -----------------------------------------
    // Hex Grid states' sprites
    public Sprite HexGridDef;
    public Sprite HexGridActive;
    public Sprite HexGridAdjacent;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();
    }


    // ---------------------------------- HEX GRID IS CLICKED -----------------------------------------------
    void OnMouseDown()
    {
        // If this hex grid is not the active one
        if (!isActive)
        {
            // And If the player has some steps left
            if (GameControllerScript.stepsLeft > 0)
            {
                // If this grid chip is adjacent to the active one (check the distance between this hex grid and the active one)
                if (Vector3.Distance(GameController.activeGrid.transform.position, transform.position) < maxDistNeighborChips)
                {
                    // Substract 1 step (used)
                    GameControllerScript.stepsLeft--;

                    // Set the last active hex grid as not active
                    GameController.activeGrid.isActive = false;

                    // Set the selected grid chip as Active
                    SetActive();

                    // Set the chips as selectable
                    GameControllerScript.SetSelectableAll();
                }
            }
        }


        // If the clicked grid chip is the origin one
        if (this.tag == "Start")
        {
            // ---------------------------------- SET PATH TO DEFAULT -------------------
            // Loop through the path chips
            for (int i = 0; i < GameControllerScript.pathGrid.Count; i = i + 1)
            {
                // If the grid chip isn't the start of the path
                if (GameControllerScript.pathGrid[i].tag != "Start")
                {
                    // Set all the path chips as default (not active/selectable)
                    GameControllerScript.pathGrid[i].SetDefault();
                }
            }

            // ---------------------------------- PATH ----------------------------------
            // Restore the player's left steps
            GameControllerScript.stepsLeft = GameControllerScript.stepsTotal;

            // Delete all the path grid chips from the path
            GameControllerScript.pathGrid.Clear();

            // Set the origin/start Active Grid chip to be the first Active Grid again
            GameController.activeGrid = GameObject.FindGameObjectWithTag("Start").GetComponent<HexGridItem>();

            // Add the origin grid chip to the path
            GameControllerScript.pathGrid.Add(GameController.activeGrid);

            // Set the origin chip's adjacents
            GameControllerScript.SetSelectableAll();
        }
    }


    // ---------------------------------- SET GRID CHIP TO DEFAULT ----------------------------------------------
    public void SetDefault()
    {
        // Set this as not active chip
        isActive = false;

        // Update the sprite of the clicked hex grid to default sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = HexGridDef;
    }


    // ---------------------------------- SET GRID CHIP TO ACTIVE -----------------------------------------------
    public void SetActive()
    {
        // Set the clicked hex grid as active
        isActive = true;

        // Store the clicked hex grid as active
        GameController.activeGrid = this;

        // Update the sprite of the clicked hex grid to active sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = HexGridActive;

        // Store this new Active Grid in the Path (to store the path)
        GameControllerScript.pathGrid.Add(this);
    }
}
