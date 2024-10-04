using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // ---------------------------------- BACKPACK -----------------------------------------
    // Store backpack ingredients
    public int[] backpackIngredients;


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

        // Store the number of each Ingredient needed for the quest
        questIngredients[0] = questTomatos;
        questIngredients[1] = questCarrots;
        questIngredients[2] = questEggplants;
        questIngredients[3] = questMushrooms;
    }


    // ---------------------------------- DELIVER QUEST ITEMS ----------------------------------------------
    // Do the deliver of the quest
    public void DeliverQuest()
    {
        // ---------------------------------- SET ARRAYS -----------------------------------------------
        // Store the number of each Ingredient placed in the Backpack
        backpackIngredients[0] = GameControllerScript.tomatosPlaced;
        backpackIngredients[1] = GameControllerScript.carrotsPlaced;
        backpackIngredients[2] = GameControllerScript.eggplantsPlaced;
        backpackIngredients[3] = GameControllerScript.mushroomsPlaced;

        // Loop through the 4 ingredients
        for (int i = 0; i < 4; i = i + 1)
        {
            // Check 6 times (bc could be up to 6 orders of the same ingredient) if there's ingredients to deliver and the player has them in their backpack
            // And deliver them
            CheckAndDeliver(i);
            CheckAndDeliver(i);
            CheckAndDeliver(i);
            CheckAndDeliver(i);
            CheckAndDeliver(i);
            CheckAndDeliver(i);
        }

        // After delivering ingredients, update the quests' UI
        GameControllerScript.CheckIngrUpdate();
    }


    // ---------------------------------- UPDATE NMB OF INGREDIENTS PLACED AFTER DELIVERY  -----------------
    // Update nmbs of ingredients that are placed in the backpack after delivering them to villagers
    void UpdateIngrPlaced()
    {
        // Store the number of each Ingredient placed in the Backpack
        GameControllerScript.tomatosPlaced = backpackIngredients[0];
        GameControllerScript.carrotsPlaced = backpackIngredients[1];
        GameControllerScript.eggplantsPlaced = backpackIngredients[2];
        GameControllerScript.mushroomsPlaced = backpackIngredients[3];

        // Store the number of each Ingredient needed for the quest
        questTomatos = questIngredients[0];
        questCarrots = questIngredients[1];
        questEggplants = questIngredients[2];
        questMushrooms = questIngredients[3];
    }


    // ---------------------------------- CHECK INGREDIENTS + DELIVER --------------------------------------
    // Check ingredients needed and available, and deliver if possible
    void CheckAndDeliver(int i)
    {
        // If (there's quest's ingredients left to deliver)
        if (questIngredients[i] > 0)
        {
            // And If (there's ingredients left in the backpack)
            if (backpackIngredients[i] > 0)
            {
                // Substract 1 from the backpack and 1 from the quest (there's 1 ingredient less to deliver)
                backpackIngredients[i]--;
                questIngredients[i]--;

                // After delivering ingredients, update how much ingredients are left in the backpack and/or to deliver in the quest
                UpdateIngrPlaced();
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