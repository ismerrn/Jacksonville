using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    // ---------------------------------- TIME MANAGEMENT ------------------------------------------
    // ---------------------------------- Checkers ----------------------------------------
    // Store the total Seasons of a Year (4)
    public int seasonsInYear = 4;

    // Store the total Weeks of a Season (4)
    public int weeksInSeason = 4;

    // Store the total Days of a Week (7)
    public int daysInWeek = 7;

    // Store the 4 different types of seasons
    public string[] seasons;


    // ---------------------------------- Counters ----------------------------------------
    // Store the number of Days past/used in each "run"
    public int daysUsed = 0;

    // Store the number of Weeks past/used in each "run"
    public int weeksUsed = 0;

    // Store the number of Seasons past/used in each "run"
    public int seasonsUsed = 0;

    // Store the number of Years past/used in each "run"
    public int yearsUsed = 0;

    // Store the activeSeason
    public GameObject activeSeason;



    // ---------------------------------- COMPACTED CALENDAR ---------------------------------------
    // ---------------------------------- Texts -------------------------------------------
    // Store the Current Day Text from the Compacted Calendar
    public TextMeshProUGUI currentDayTxt;



    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- SET VALUES -------------------------------------------------
        // Start at Day 1
        daysUsed++;

        // Start at Week 1
        weeksUsed++;

        // Start at Year 1
        yearsUsed++;

        // Store the Season's names in the array Seasons
        seasons = new string[4] {"Spring", "Summer", "Autumn", "Winter"};
    }


    // ---------------------------------- UPDATE TIME ELEMENTS ---------------------------------------------
    public void TimePass()
    {
        // ---------------------------------- MAKE TIME PASS ---------------------------------------------
        // Loop 4 times
        for (int i = 0; i < seasons.Length; i = i + 1)
        {
            // Check which season is active
            if (activeSeason.tag == seasons[i])
            {
                // Make the time pass according to that season
                // If it's still the same season (the 4 weeks of the season haven't passed yet)
                if (weeksUsed <= weeksInSeason)
                {
                    // And If the player has played 7 days
                    if (daysUsed == daysInWeek)
                    {
                        // Go next week
                        weeksUsed++;

                        // Reset days of week spent (start at day 1)
                        daysUsed = 1;
                    }
                }

                // If it's next season (the 4 weeks of the season have passed)
                else
                {
                    // If it's not Winter
                    if (seasonsUsed < seasonsInYear)
                    {
                        // Go next season
                        activeSeason.tag = seasons[i++];
                        seasonsUsed++;

                        // Reset weeks of season spent (start at week 1)
                        weeksUsed = 1;

                        // Reset days of week spent (start at day 1)
                        daysUsed = 1;
                    }

                    // If it's winter
                    else
                    {
                        // Go next year
                        yearsUsed++;

                        // Reset seasons
                        activeSeason.tag = seasons[0];
                        seasonsUsed = 1;

                        // Reset weeks of season spent (start at week 1)
                        weeksUsed = 1;

                        // Reset days of week spent (start at day 1)
                        daysUsed = 1;
                    }
                }
            }
        }
    }
}
