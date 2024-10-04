using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    // ---------------------------------- QUEST --------------------------------------------
    // Store the Ingredients for the 3 Orders
    public GameObject[] ingredientsOrders;

    // Store the order's ingredients
    public GameObject ingrOrder1;
    public GameObject ingrOrder2;
    public GameObject ingrOrder3;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- SET ARRAYS -----------------------------------------------
        // Store the number of each Ingredient placed in the Backpack
        //ingredientsOrders[0] = ingrOrder1;
        //ingredientsOrders[1] = ingrOrder2;
        //ingredientsOrders[2] = ingrOrder3;
    }
}
