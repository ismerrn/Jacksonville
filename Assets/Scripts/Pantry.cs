using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantry : MonoBehaviour
{
    // ---------------------------------- ARRAYS -------------------------------------------
    // Array to store all the Ingredient chips GOs
    public GameObject[] pIngredientChip;

    // Array to store all the ingredient chips with the default sprite
    public Sprite[] pIngredientDefault;

    // Ingredient chips default sprite
    public Sprite PTomatoDef;
    public Sprite PCarrotDef;
    public Sprite PEggplantDef;
    public Sprite PMushroomDef;


    // ---------------------------------- SCRIPTS -----------------------------------------
    // Reference to Clicker script
    public Clicker ClickerScript;

    // Reference to IngSelectable script
    public IngSelectable IngSelectableScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- SET ARRAYS ----------------------------------------
        // Add all the sprites to the Pantry's Ingredients (default) array
        pIngredientDefault[0] = PTomatoDef;
        pIngredientDefault[1] = PCarrotDef;
        pIngredientDefault[2] = PEggplantDef;
        pIngredientDefault[3] = PMushroomDef;

        // ---------------------------------- ACCESS --------------------------------------------
        // Access the Clicker script
        ClickerScript = FindObjectOfType<Clicker>();
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access the Clicker script
        IngSelectableScript = FindObjectOfType<IngSelectable>();
    }


    // ---------------------------------- DEACTIVATE INGREDIENTS ------------------------------------------
    public void UnblockIngredients(int index)
    {
        // Loop through all the 4 Ingredient chips
        for (int i = 0; i < 4; i++)
        {
            // If the ingredient hasn't been clicked (Clicker script)
            if (i != index)
            {
                // Mark it as an ingredient NOT SELECTED
                pIngredientChip[i].transform.parent.GetComponent<Clicker>().isIngrSelected = false;

                // Set the sprites of the none selected ingredients as default ones
                pIngredientChip[i].GetComponent<SpriteRenderer>().sprite = pIngredientDefault[i];
            }

            // If some Ingredient has been selected (clicker) and placed
            else if (i == index && IngSelectableScript.isPlaced == true)
            {
                // Reset the sprite of the Selected Ingredient (pantry) to default (= clickable again)
                pIngredientChip[i].GetComponent<SpriteRenderer>().sprite = pIngredientDefault[i];

                // Set the Selected Ingredient UI as blank
                ClickerScript.pIngredientSelected.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }


}
