using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCalendar : MonoBehaviour
{
    // ---------------------------------- DAY CHIP STATES ------------------------------------------
    // ---------------------------------- States ------------------------------------------
    private DayCalendar[] calendarChips;

    // ---------------------------------- States ------------------------------------------
    // Check if the chip has been clicked
    public bool isClicked = false;

    // Store the sprite of each day chip state
    public Sprite dayDefault;
    public Sprite dayToday;
    public Sprite daySelected;
    // Sprites from days passed and missions failed/accomplished


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // Store all the day chips in an array
        calendarChips = FindObjectsOfType<DayCalendar>();
    }


    // ---------------------------------- DAY IS CLICKED -----------------------------------------------------------------
    void OnMouseDown()
    {
        // If hasn't been clicked before
        if (isClicked == false)
        {
            Debug.Log("has been clicked");

            // Select it
            SelectChip();
        }
    }


    public void DeselectChip()
    {
        // Access the Sprite Renderer of the clicked chip
        SpriteRenderer daySpriteRenderer = GetComponent<SpriteRenderer>();

        // Set it as not clicked
        isClicked = false;

        // Update its state to Selected (by changing its sprite)
        daySpriteRenderer.sprite = dayDefault;
    }

    public void SelectChip()
    {
        // Access the Sprite Renderer of the clicked chip
        SpriteRenderer daySpriteRenderer = GetComponent<SpriteRenderer>();

        // Set it as clicked
        isClicked = true;

        // Update its state to Selected (by changing its sprite)
        daySpriteRenderer.sprite = daySelected;
    }
}
