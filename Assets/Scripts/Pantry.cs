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
    public Sprite PTomato;
    public Sprite PCarrot;
    public Sprite PEggplant;
    public Sprite PMushroom;

    // ---------------------------------- SCRIPTS -----------------------------------------
    // Access Clicker script
    public Clicker ClickerScript;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- SET ARRAYS ----------------------------------------
        // Add all the sprites to the Pantry's Ingredients (default) array
        pIngredientDefault[0] = PTomato;
        pIngredientDefault[1] = PCarrot;
        pIngredientDefault[2] = PEggplant;
        pIngredientDefault[3] = PMushroom;

        // ---------------------------------- ACCESS --------------------------------------------
        // Access the Clicker script
        ClickerScript = FindObjectOfType<Clicker>();
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

                // Deactivate the none selected ingredients' shapes
                ClickerScript.cIngredientShape[i].SetActive(false);
            }
        }
    }


}
