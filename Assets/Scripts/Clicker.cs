using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    //public GameObject ingredientChip;
    public SpriteRenderer ingredientChipRenderer;

    public Sprite PTomato;
    public Sprite PCarrot;
    public Sprite PEggplant;
    public Sprite PMushroom;

    public Sprite PTomatoBlocked;
    public Sprite PCarrotBlocked;
    public Sprite PEggplantBlocked;
    public Sprite PMushroomBlocked;

    void Start()
    {
        ingredientChipRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Code here is called when the GameObject is clicked on.
    void OnMouseDown()
    {
        if (ingredientChipRenderer.sprite = PTomato)
        {
            //Change the image for the one blocked
            ingredientChipRenderer.sprite = PTomatoBlocked;
        }

    }
}
