using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CalendarPanel : MonoBehaviour
{
    // ---------------------------------- GAME CONTROLLER ------------------------------------------
    // Access Calendar script
    private Calendar CalendarScript;



    // ---------------------------------- QUESTS ---------------------------------------------------
    // Store every Quest script
    public Quest[] questsScripts;



    // ---------------------------------- CALENDAR DAYS --------------------------------------------
    // Store every Calendar Day chip
    public DayCalendar[] calendarDayChips;



    // ---------------------------------- +DETAILS PANEL -------------------------------------------
    // ------------------------------- Owner Identity -------------------------
    // Store the Quest's (+Details) Owner Icon GO
    public GameObject qOwnerIcon;

    // Store the Quest's (+Details) Owner Name text
    public TextMeshProUGUI qOwnerNameTxt;


    // ------------------------------- Timing ---------------------------------
    // Store the Quest's (+Details) Date text
    public TextMeshProUGUI qDateTxt;

    // Store the Quest's (+Details) Days Left text
    public TextMeshProUGUI qDaysLeftTxt;


    // ------------------------------- Requirements ---------------------------
    // Store the Quest's (+Details) Description text
    public TextMeshProUGUI qDescriptionTxt;


    // ------------------------------- Rewards --------------------------------
    // Store the Quest's (+Details) Reward 1 Icon GO
    public GameObject qReward1Icon;

    // Store the Quest's (+Details) Reward 1 Content text
    public TextMeshProUGUI qReward1Txt;

    // Store the Quest's (+Details) Reward 2 Icon GO
    public GameObject qReward2Icon;

    // Store the Quest's (+Details) Reward 2 Content text
    public TextMeshProUGUI qReward2Txt;



    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS ------------------------------------------------------
        // Access the Calendar script
        CalendarScript = FindObjectOfType<Calendar>();


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
                    // And update the +Details Panel with Mark's quest data (icon, name, date, description, ingredient orders, rewards)
                    UpdateQuestOwnerIdentity(i);
                    UpdateQuestTime(i);
                    UpdateQuestRequirements(i);
                    UpdateQuestRewards(i);
                }

                // Check if there's a Tim's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Tim")
                {
                    // And update the +Details Panel with Tim's quest data (icon, name, date, description, ingredient orders, rewards)
                    UpdateQuestOwnerIdentity(i);
                    UpdateQuestTime(i);
                    UpdateQuestRequirements(i);
                    UpdateQuestRewards(i);
                }

                // Check if there's a Felix's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Felix")
                {
                    // And update the +Details Panel with Felix's quest data (icon, name, date, description, ingredient orders, rewards)
                    UpdateQuestOwnerIdentity(i);
                    UpdateQuestTime(i);
                    UpdateQuestRequirements(i);
                    UpdateQuestRewards(i);
                }

                // Check if there's a Bobby's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Bobby")
                {
                    // And update the +Details Panel with Bobby's quest data (icon, name, date, description, ingredient orders, rewards)
                    UpdateQuestOwnerIdentity(i);
                    UpdateQuestTime(i);
                    UpdateQuestRequirements(i);
                    UpdateQuestRewards(i);
                }

                // Check if there's a Niggel's quest that day
                if (calendarDayChips[i].transform.GetChild(0).tag == "Niggel")
                {
                    // And update the +Details Panel with Niggel's quest data (icon, name, date, description, ingredient orders, rewards)
                    UpdateQuestOwnerIdentity(i);
                    UpdateQuestTime(i);
                    UpdateQuestRequirements(i);
                    UpdateQuestRewards(i);
                }
            }
        }
    }



    // ---------------------------------- UPDATE QUEST OWNER ICON --------------------------------------------------------
    // Update the Quest +Details panel with the quest owner icon + quest owner name
    void UpdateQuestOwnerIdentity(int i)
    {
        // Loop through all the quest scripts
        for (int i2 = 0; i2 < questsScripts.Length; i2 = i2 + 1)
        {
            // Look for Quest of the Mission owner
            if (calendarDayChips[i].transform.GetChild(0).tag == questsScripts[i2].gameObject.name)
            {
                // Update the +Details Quest Owner Icon to the selected quest owner icon
                qOwnerIcon.GetComponent<SpriteRenderer>().sprite = questsScripts[i2].questOwnerIcon;

                // Set the owner's name as the +Details panel quest owner's name
                qOwnerNameTxt.text = "" + questsScripts[i2].questOwnerName;
            }
        }
    }



    // ---------------------------------- UPDATE QUEST DATE --------------------------------------------------------------
    // Update the Quest +Details panel with the Villager's quest date + quest days left
    void UpdateQuestTime(int i)
    {
        // Loop through all the quest scripts
        for (int i2 = 0; i2 < questsScripts.Length; i2 = i2 + 1)
        {
            // Look for Quest of the Mission owner
            if (calendarDayChips[i].transform.GetChild(0).tag == questsScripts[i2].gameObject.name)
            {
                // ---------------------------------- CALCULATE DAYS LEFT ----------------------------------------
                // Reference to store time left to accomplish this quest
                int daysLeftForQuest;

                // Calculate the Days Left for the Quest to expire (+1 because the current day counts too)
                daysLeftForQuest = questsScripts[i2].questDay - CalendarScript.daysUsed + 1;


                // ---------------------------------- UPDATE QUEST DATE ------------------------------------------
                // Set the owner's quest date as the +Details panel quest date text
                qDateTxt.text = "" + questsScripts[i2].questDate;


                // ---------------------------------- UPDATE LEFT DAYS -------------------------------------------
                // If there's days left in Positive numbers (0, 1, 2, etc.)
                if (daysLeftForQuest >= 0)
                {
                    // Update the Days Left text in the Calendar +Details UI Panel
                    qDaysLeftTxt.text = daysLeftForQuest + " days";
                }

                // If there's days left in Negative numbers
                else
                {
                    // Update the Days Left text in the Calendar +Details UI Panel
                    qDaysLeftTxt.text = 0 + " days";
                }
            }
        }
    }



    // ---------------------------------- UPDATE QUEST DESCRIPTION -------------------------------------------------------
    // Update the Quest +Details panel with the Villager's quest description + quest ingredient orders
    void UpdateQuestRequirements(int i)
    {
        // Loop through all the quest scripts
        for (int i2 = 0; i2 < questsScripts.Length; i2 = i2 + 1)
        {
            // Look for Quest of the Mission owner
            if (calendarDayChips[i].transform.GetChild(0).tag == questsScripts[i2].gameObject.name)
            {
                // ---------------------------------- DESCRIPTION ------------------------------------------------
                // Set the owner's quest description as the +Details panel quest Description text
                qDescriptionTxt.text = "" + questsScripts[i2].questDescription;

                // Check & Update the Quest's Orders
                questsScripts[i2].CheckUpdateQuestOrder();
            }
        }
    }



    // ---------------------------------- UPDATE QUEST REWARDS -----------------------------------------------------------
    // Update the Quest +Details panel with the Villager's quest rewards
    void UpdateQuestRewards(int i)
    {
        // Loop through all the quest scripts
        for (int i2 = 0; i2 < questsScripts.Length; i2 = i2 + 1)
        {
            // Look for Quest of the Mission owner
            if (calendarDayChips[i].transform.GetChild(0).tag == questsScripts[i2].gameObject.name)
            {
                // ---------------------------------- REWARD 1 ---------------------------------------------------
                // Update the +Details Quest Reward 1 Icon to the selected quest Reward 1 Icon
                qReward1Icon.GetComponent<SpriteRenderer>().sprite = questsScripts[i2].questReward1Icon;

                // Set the owner's quest Reward 2 Content as the +Details panel quest Reward 2 Content text
                qReward1Txt.text = "" + questsScripts[i2].questReward1Content;


                // ---------------------------------- REWARD 2 ---------------------------------------------------
                // Update the +Details Quest Reward 2 Icon to the selected quest Reward 2 Icon
                qReward2Icon.GetComponent<SpriteRenderer>().sprite = questsScripts[i2].questReward2Icon;

                // Set the owner's quest Reward 2 Content as the +Details panel quest Reward 2 Content text
                qReward2Txt.text = "" + questsScripts[i2].questReward2Content;
            }
        }
    }
}
