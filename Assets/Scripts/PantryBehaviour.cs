using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantryBehaviour : MonoBehaviour
{
    // ---------------------------------- ARRAYS -------------------------------------------
    public GameObject[] pIngredientChip;


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
            }
        }
    }


}
