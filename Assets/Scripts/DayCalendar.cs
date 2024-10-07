using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCalendar : MonoBehaviour
{
    // ---------------------------------- CALENDAR PANEL -------------------------------------------
    // Store the Calendar Panel script
    private CalendarPanel CalendarPanelScript;

    // ---------------------------------- DAY CHIP STATES ------------------------------------------
    // ---------------------------------- Identity ----------------------------------------
    // Set an ID in the Unity inspector so we can recognize each day (day 01 - Day ID: 1)
    public int dayID;

    // Set an ID in the Unity inspector so we can recognize each week (day 01 - Week ID: 1, day 08 - Week ID: 2)
    public int weekID;


    // ---------------------------------- Store -------------------------------------------
    // Reference to all Calendar chips
    private DayCalendar[] calendarChips;


    // ---------------------------------- States ------------------------------------------
    // Check if the chip has been clicked
    public bool isToday;

    // Check if the chip has been clicked
    public bool isClicked = false;

    // Store the sprite of each day chip state
    public Sprite dayDefault;
    public Sprite dayToday;
    public Sprite daySelected;
    // Sprites from days passed and missions failed/accomplished



    // ---------------------------------- GAME CONTROLLER ------------------------------------------
    // Access Game Controller script
    private GameController GameControllerScript;

    // Access Calendar script
    private Calendar CalendarScript;




    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access the Game Controller Script
        GameControllerScript = FindObjectOfType<GameController>();

        // Access the Calendar Script
        CalendarScript = FindObjectOfType<Calendar>();

        // Access the Calendar Panel script
        CalendarPanelScript = FindObjectOfType<CalendarPanel>();

        // ---------------------------------- STORE ---------------------------------------------
        // Store all the day chips in an array
        calendarChips = FindObjectsOfType<DayCalendar>();

        // Loop through all the Calendar chips
        for (int i = 0; i < calendarChips.Length; i = i + 1)
        {
            // The ones with the Villager icon active (Missions)
            if (calendarChips[i].transform.GetChild(0).gameObject.activeSelf)
            {
                // Add them the tag "Mission Day"
                calendarChips[i].tag = "Mission Day";
            }
        }
    }



    // ---------------------------------- DAY IS CLICKED -----------------------------------------------------------------
    void OnMouseDown()
    {
        // Deselect any chip that was selected before
        GameControllerScript.DeselectCalendarChip();

        // If hasn't been clicked before
        if (isClicked == false)
        {
            // Select it
            SelectCalendarChip();

            // If the Selected Day is a Mission Day
            if (gameObject.tag == "Mission Day")
            {
                // Check which Quest to display in +Details UI panel
                CalendarPanelScript.CheckQuestToDisplay();
            }
        }
    }



    // ---------------------------------- DAY IS SELECTED ----------------------------------------------------------------
    // Unselect last clicked chip + Select the current clicked chip (change its UI to the selected one)
    public void SelectCalendarChip()
    {
        // Unselect last selected chip
        CalendarScript.DeselectCalendarChip();

        // Access the Sprite Renderer of the clicked chip
        SpriteRenderer daySpriteRenderer = GetComponent<SpriteRenderer>();

        // Set it as clicked
        isClicked = true;

        // Store the selected day
        Calendar.selectedDay = this;

        // If it's a Default/Regular day
        if (isToday == false)
        {
            // Update its state to Default Selected (by changing its sprite)
            daySpriteRenderer.sprite = daySelected;
        }
    }
}
