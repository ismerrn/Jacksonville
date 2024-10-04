using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderIngr : MonoBehaviour
{
    // ---------------------------------- OWNER --------------------------------------------
    // Store (in Unity) this quest owner's name
    public string ownerName;

    // Store (in Unity) this quest owner
    public GameObject owner;


    // ---------------------------------- INGREDIENTS --------------------------------------
    // Store (in Unity) this order's index (tomato = 0, carrot = 1, eggplant = 2, mushroom = 3)
    public int ingrIndex;

    // Store Order's Text with the quantity of ingredients needed
    public TextMeshProUGUI ingrQuantity;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- STORE ----------------------------------------------------
        // Store the Order's owner (quest owner)
        owner = GameObject.Find(ownerName);

        // Store the Text associated to this order
        ingrQuantity = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

   
    // ---------------------------------- UPDATE UI TEXT OF EACH ORDER -----------------------------------------------------------
    // When delivering an order update its text
    public void UpdateQuantity()
    {
        if (ingrQuantity != null && owner != null)
        {
            // Update text with the current ingredients needed for that quest
            ingrQuantity.text = "x" + owner.GetComponent<Quest>().questIngredients[ingrIndex];

            // If the order has been delivered completely (0 of that ingredient is needed)
            if (owner.GetComponent<Quest>().questIngredients[ingrIndex] == 0)
            {
                // Active the order's check UI
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
