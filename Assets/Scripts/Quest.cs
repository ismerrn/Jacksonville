using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Store the day the Quest is required (update Inspector Unity)
    public int questDay;

    // Reference the Quest UI associated to this quest
    public GameObject questUI;

    // Reference the Quest's Days Left text
    public TextMeshProUGUI questDaysLeftTxt;


    // ---------------------------------- GAME CONTROLLER ----------------------------------
    // Reference to Game Controller script
    public GameController GameControllerScript;

    // Reference to Calendar script
    public Calendar CalendarScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS ---------------------------------------------------
        // Access the Game Controller script
        GameControllerScript = FindObjectOfType<GameController>();

        // Access the Calendar script
        CalendarScript = FindObjectOfType<Calendar>();

        // Store the Days Left from the Quest UI (1st child)
        questDaysLeftTxt = questUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

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


    // ---------------------------------- UPDATE QUEST UI - DAYS LEFT --------------------------------------
    // Update the Days Left in the Quest UI
    public void UpdateDaysLeft()
    {
        // Reference to store time left to accomplish this quest
        int daysLeftForQuest;

        // Calculate the Days Left for the Quest to expire (+1 because the current day counts too)
        daysLeftForQuest = questDay - CalendarScript.daysUsed + 1;

        // If there's days left in Positive numbers (0, 1, 2, etc.)
        if (daysLeftForQuest >= 0)
        {
            // Update the Days Left text in the Quest UI
            questDaysLeftTxt.text = "" + daysLeftForQuest;
        }

        // If there's days left in Negative numbers
        else
        {
            // Update the Days Left text in the Quest UI
            questDaysLeftTxt.text = "" + 0;
        }
    }
}