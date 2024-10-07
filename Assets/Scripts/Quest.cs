using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // ---------------------------------- GAME CONTROLLER ------------------------------------------
    // Reference to Game Controller script
    public GameController GameControllerScript;

    // Reference to Calendar script
    public Calendar CalendarScript;



    // ---------------------------------- BACKPACK -------------------------------------------------
    // Store backpack ingredients
    public int[] backpackIngredients;



    // ---------------------------------- QUEST ----------------------------------------------------
    // ---------------------------------- Quest info --------------------------------------
    // ------------------------------- Set Quest ------------------------------
    // Store the day the Quest is required (update Inspector Unity)
    public int questDay;

    // Reference the Quest UI associated to this quest
    public GameObject questUI;


    // ------------------------------- Owner's Identity -----------------------
    // Reference to Quest Owner's Icon
    public Sprite questOwnerIcon;

    // Reference to Quest Owner's Name
    public string questOwnerName;

    // ------------------------------- Quest's Timing -------------------------
    // Add this before a String to add a Text box UI in the Inspector so you can add enter spaces and +
    [TextArea]

    // Reference to Quest's date
    public string questDate;

    // ------------------------------- Quest's Narrative ----------------------
    // Add this before a String to add a Text box UI in the Inspector so you can add enter spaces and +
    [TextArea]

    // Store the quest description text
    public string questDescription;

    // ------------------------------- Quest's Orders -------------------------
    // Storequest ingredients
    public int[] questIngredients;

    // Store all the quest ingredients and its quantities
    public int questTomatos;
    public int questCarrots;
    public int questEggplants;
    public int questMushrooms;

    // Store origin/starting number of ingredients required for the quest
    public int[] qIngredientsRequired;

    // Store the starting requirements of each ingredient for this quest
    public int qTomatosRequired;
    public int qCarrotsRequired;
    public int qEggplantsRequired;
    public int qMushroomsRequired;

    // Store the type of ingredients wanted and in which order
    public int ingr1Index;
    public int ingr2Index;
    public int ingr3Index;

    // ------------------------------- Rewards --------------------------------
    // Store Reward 1: Icon + Content (text explaining reward)
    public Sprite questReward1Icon;
    public string questReward1Content;

    // Store Reward 1: Icon + Content (text explaining reward)
    public Sprite questReward2Icon;
    public string questReward2Content;


    // ---------------------------------- Connectors --------------------------------------
    // Reference the Quest's Days Left text
    public TextMeshProUGUI questDaysLeftTxt;

    // Reference the Quest's Days Left text
    public TextMeshProUGUI questDaysLeftCalendarTxt;

    // Store the Quest's (+Details) Order 1 ingredient icon + quantity text
    public GameObject qOrder1Icon;
    public TextMeshProUGUI qOrder1Txt;

    // Store the Quest's (+Details) Order 2 ingredient icon + quantity text
    public GameObject qOrder2Icon;
    public TextMeshProUGUI qOrder2Txt;

    // Store the Quest's (+Details) Order 3 ingredient icon + quantity text
    public GameObject qOrder3Icon;
    public TextMeshProUGUI qOrder3Txt;



    // ---------------------------------- INGREDIENTS ----------------------------------------------
    // Store every type of ingredients
    public Sprite[] typeIngredients;

    // Store ingredients sprites
    public Sprite tomatoIngr;
    public Sprite carrotIngr;
    public Sprite eggplantIngr;
    public Sprite mushroomIngr;



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


        // ---------------------------------- STORE ----------------------------------------------------
        // Store origin/starting number of each Ingredient needed for the quest
        qTomatosRequired = questTomatos;
        qCarrotsRequired = questCarrots;
        qEggplantsRequired = questEggplants;
        qMushroomsRequired = questMushrooms;

        // Store ingredients sprites
        typeIngredients = new Sprite[4] { tomatoIngr, carrotIngr, eggplantIngr, mushroomIngr };

        // Store the origin Ingredients required for each quest
        qIngredientsRequired = new int[4] {qTomatosRequired, qCarrotsRequired, qEggplantsRequired, qMushroomsRequired};

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

    // ---------------------------------- CHECK & UPDATE QUEST'S ORDERS --------------------------------------------------
    // Check the Quest Orders and then Update it in the Calendar
    public void CheckUpdateQuestOrder()
    {
        // Loop through all the ingredients (4)
        for (int i = 0; i < qIngredientsRequired.Length; i = i + 1)
        {
            // ---------------------------------- ORDER 1 ----------------------------------------------------
            // Check the ingredient of the Order 1
            if (ingr1Index == i)
            {
                // Update the Ingredient icon with the Villager's quest info
                qOrder1Icon.GetComponent<SpriteRenderer>().sprite = typeIngredients[i];

                // Update Ingredient Quantity text with the Villager's quest info
                qOrder1Txt.text = "x" + qIngredientsRequired[i];
            }

            // ---------------------------------- ORDER 2 ----------------------------------------------------
            // Check the ingredient of the Order 2
            else if (ingr2Index == i)
            {
                // Update the Ingredient icon with the Villager's quest info
                qOrder2Icon.GetComponent<SpriteRenderer>().sprite = typeIngredients[i];

                // Update Ingredient Quantity text with the Villager's quest info
                qOrder2Txt.text = "x" + qIngredientsRequired[i];
            }

            // ---------------------------------- ORDER 3 ----------------------------------------------------
            // Check the ingredient of the Order 3
            else if (ingr3Index == i)
            {
                // Update the Ingredient icon with the Villager's quest info
                qOrder3Icon.GetComponent<SpriteRenderer>().sprite = typeIngredients[i];

                // Update Ingredient Quantity text with the Villager's quest info
                qOrder3Txt.text = "x" + qIngredientsRequired[i];
            }
        }
    }
}