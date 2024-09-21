using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantryBehaviour : MonoBehaviour
{
    public GameObject[] pIngredientChip;

    public void DeactivateChilds(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i != index)
            {
                pIngredientChip[i].transform.GetChild(0).gameObject.GetComponent<Clicker>().isIngrSelected = false;
            }
        }
    }


}
