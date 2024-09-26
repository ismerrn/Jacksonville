using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    // ---------------------------------- BOOLS -------------------------------------------------
    // Bool to store and check if each grid item is empty or occupied
    public bool gridIsEmpty = true;

    void OnMouseDown()
    {
        if (GameController.selectedIngr != null)
        {
            Debug.Log("37");

            //
            GameController.selectedIngr.PlaceIngrGrid();
        }
    }
}
