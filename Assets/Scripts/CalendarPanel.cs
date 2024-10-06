using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalendarPanel : MonoBehaviour
{
    // ---------------------------------- QUESTS ---------------------------------------------------
    // Store every Quest script
    public Quest[] questsScripts;


    // ---------------------------------- CALENDAR DAYS --------------------------------------------
    // Store every Calendar Day chip
    public DayCalendar[] calendarDayChips;


    // ---------------------------------- +DETAILS PANEL -------------------------------------------
    // Store the Quest (+Details) Description Text
    public TextMeshProUGUI questDescriptionTxt;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- STORE -------------------------------------------------------
        // Store all the Quest scripts
        questsScripts = FindObjectsOfType<Quest>();

        // Store all the Calendar Day chips
        calendarDayChips = FindObjectsOfType<DayCalendar>();
    }


    // ---------------------------------- CHECK QUEST TO DISPLAY ---------------------------------------------------------
    // Check which Quest must be displayed in the +Details UI panel
    public void CheckQuestToDisplay()
    {
        // Loop through all the Calendar's Day chips (28)
        for (int i = 0; i < calendarDayChips.Length; i = i + 1)
        {
            // If has been clicked
            if (calendarDayChips[i].isClicked == true)
            {
                // Check if there's a Mark's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Mark")
                {
                    // And update the +Details Panel - Quest info with that quest's data
                    UpdateQuestInfo(i);
                }

                // Check if there's a Tim's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Tim")
                {
                    // And update the +Details Panel - Quest info with that quest's data
                    UpdateQuestInfo(i);
                }

                // Check if there's a Mark's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Felix")
                {
                    // And update the +Details Panel - Quest info with that quest's data
                    UpdateQuestInfo(i);
                }

                // Check if there's a Bobby's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Bobby")
                {
                    // And update the +Details Panel - Quest info with that quest's data
                    UpdateQuestInfo(i);
                }

                // Check if there's a Niggel's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Niggel")
                {
                    // And update the +Details Panel - Quest info with that quest's data
                    UpdateQuestInfo(i);
                }
            }
        }
    }


    // ---------------------------------- UPDATE +DETAILS UI PANEL -------------------------------------------------------
    // Update the Calendar's Quest +Details panel with the selected day info about the Villager's quest
    public void UpdateQuestInfo(int i)
    {
        // Loop through all the quest scripts
        for (int i2 = 0; i2 < questsScripts.Length; i2 = i2 + 1)
        {
            // Look for Quest of the Mission owner
            if (calendarDayChips[i].transform.GetChild(0).tag == questsScripts[i2].gameObject.name)
            {
                // Set the owner's quest description as the +Details panel quest Description text
                questDescriptionTxt.text = "" + questsScripts[i2].questDescription;
            }
        }
    }
}
