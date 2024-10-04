using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // ---------------------------------- BACKPACK -----------------------------------------
    // Store backpack ingredients
    public int[] backpackIngredients;

    // Store all the backpack ingredients and its quantities
    public int backpackTomatos = 0;
    public int backpackCarrots = 0;
    public int backpackEggplants = 0;
    public int backpackMushrooms = 0;


    // ---------------------------------- QUEST --------------------------------------------
    // Storequest ingredients
    public int[] questIngredients;

    // Store all the quest ingredients and its quantities
    public int questTomatos;
    public int questCarrots;
    public int questEggplants;
    public int questMushrooms;

    // Store the Quest UI associated to this quest
    public GameObject questUI;


    // ---------------------------------- GAME CONTROLLER ----------------------------------
    // Reference to Game Controller script
    public GameController GameControllerScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS ---------------------------------------------------
        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();

        // ---------------------------------- SET ARRAYS -----------------------------------------------
        // Store the number of each Ingredient placed in the Backpack
        backpackIngredients[0] = backpackTomatos;
        backpackIngredients[1] = backpackCarrots;
        backpackIngredients[2] = backpackEggplants;
        backpackIngredients[3] = backpackMushrooms;

        // Store the number of each Ingredient needed for the quest
        questIngredients[0] = questTomatos;
        questIngredients[1] = questCarrots;
        questIngredients[2] = questEggplants;
        questIngredients[3] = questMushrooms;
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // Store the number of each type of Ingredients already placed in the backpack inventory
        backpackTomatos = GameControllerScript.tomatosPlaced;
        backpackCarrots = GameControllerScript.carrotsPlaced;
        backpackEggplants = GameControllerScript.eggplantsPlaced;
        backpackMushrooms = GameControllerScript.mushroomsPlaced;

        // Loop through the 4 ingredients
        /*for (int i = 0; i < 4; i = i + 1)
        {
            // If the player has same number of X ingredient in the backpack as needed in this quest
            if (questIngredients[i] >= backpackIngredients[i])
            {
                // Activate the check of that order
                //pIngredientSelected.GetComponent<SpriteRenderer>().enabled = true;
            }

            // If (there's no ingredients left to deliver)
            else
            {
                // Deactivate the check of that order
                //pIngredientSelected.GetComponent<SpriteRenderer>().enabled = false;
            }
        }*/
    }


    // ---------------------------------- DELIVER QUEST ITEMS ----------------------------------------------
    // Do the deliver of the quest
    public void DeliverQuest()
    {
        // Loop through the 4 ingredients
        for (int i = 0; i < 4; i = i + 1)
        {
            // If (there's ingredients left to deliver)
            if (questIngredients[i] > 0)
            {
                // And If (there's ingredients left in the backpack)
                if (backpackIngredients[i] > 0)
                {
                    // Substract 1 from the backpack and 1 from the quest (there's 1 ingredient less to deliver)
                    backpackIngredients[i]--;
                    questIngredients[i]--;
                }
            }

            // If (there's no ingredients left to deliver)
            /*else
            {
                // Check quest UI order
                // Deactivate the renderer of the ingredient selected (as none is selected for now)
                //pIngredientSelected.GetComponent<SpriteRenderer>().enabled = true;
            }*/
        }
    }
}