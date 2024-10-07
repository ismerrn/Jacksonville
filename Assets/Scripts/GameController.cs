using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ---------------------------------- GAME PHASES ----------------------------------------------
    // Check if the game has entered the Execution Phase
    public bool isExecutionPhase = false;



    // ---------------------------------- CAMERA ---------------------------------------------------
    // Reference to the Main Camera (with the Canvas as a child object)
    public GameObject mainCamera;

    // Reference to the Main Camera's position
    private Vector3 mainCameraPos;

    // Set offsets for the camera position in each scene (Menu, Calendar, Backpack, Map, Execution)
    private Vector3 calendarCamOffset;
    private Vector3 backpackCamOffset;
    private Vector3 mapCamOffset;

    // Check in which screen is the camera focusing on
    public bool isInCalendar = false;
    public bool isInBackpack = false;
    public bool isInMap = false;



    // ---------------------------------- MONEY ----------------------------------------------------
    // Store the Player's money quantity
    public int playerMoney = 0;

    // Store the current player Money (UI text)
    public TextMeshProUGUI playerMoneyTxt;



    // ---------------------------------- CALENDAR -------------------------------------------------
    // ---------------------------------- Compacted ---------------------------------------
    // Store the Current Day Text from the Compacted Calendar
    public TextMeshProUGUI currentDayTxt;

    // Store the Current Week Day Text from the Compacted Calendar
    public TextMeshProUGUI currentWeekDayTxt;


    // ---------------------------------- General -----------------------------------------
    // Store the current Day
    public GameObject activeDay;

    // Store the last current Day
    public GameObject lastActiveDay;

    // Store the sprite for the default past day + past quest days (one for the quest completed, and other for the quest not completed)
    public Sprite pastDay;
    public Sprite pastQuestCompleted;
    public Sprite pastQuestNotCompleted;

    // Store the Calendar Script
    public Calendar CalendarScript;

    // Store the Calendar Panel Script
    private CalendarPanel CalendarPanelScript;

    // Store every Calendar chip
    public DayCalendar[] calendarChips;

    // Store the sprite of each day chip state
    public Sprite dayDefault;
    public Sprite dayToday;
    public Sprite daySelected;
    // Sprites from days passed and missions failed/accomplished



    // ---------------------------------- BACKPACK -------------------------------------------------
    // ---------------------------------- Ingredients -------------------------------------
    // Store the Selected Ingredient (clicked) (puede que NO sea static??)
    public static IngSelectable selectedIngr;

    // Store the pantry ingredient selected UI/Feedback
    public GameObject pIngredientSelected;

    // Store the shape selected UI/Feedback
    public GameObject gFeedback;

    // Store the number of each type of Ingredients already placed in the backpack inventory
    public int tomatosPlaced = 0;
    public int carrotsPlaced = 0;
    public int eggplantsPlaced = 0;
    public int mushroomsPlaced = 0;

    // Array to store all the ingredients ordered in quests
    public OrderIngr[] ingrOrders;


    // ---------------------------------- Mouse ------------------------------------------
    // Check if the Cursor has some Ingredient clicked or not
    public static bool emptyCursor = true;

    // Store the Mouse position each frame
    private Vector3 mousePos;

    // Set offsets for the mouse position
    private Vector3 offsetMouse;
    private Vector3 offsetMouseTomato;
    private Vector3 offsetMouseCarrot;
    private Vector3 offsetMouseEggplant;
    private Vector3 offsetMouseMushroom;


    // ---------------------------------- Arrays ------------------------------------------
    // Array to store all the cursor ingredients' shapes
    public GameObject[] gChipsFeedback;

    // Array to store all the Clicker scripts in the screne
    public Clicker[] ClickerScripts;

    private IngSelectable IngSelectableScript;


    // ---------------------------------- Raycast -----------------------------------------
    // To specify layers to use in Physics.Raycast
    public LayerMask layerMask;



    // ---------------------------------- MAP ------------------------------------------------------
    // ---------------------------------- Steps -------------------------------------------
    // Store the number of Steps (Total --> each round, Left --> not used this round)
    public int stepsTotal = 3;
    public int stepsLeft = 3;

    // Store Order's Text with the quantity of ingredients needed
    public TextMeshProUGUI stepsLeftTxt;


    // ---------------------------------- Grid --------------------------------------------
    // Store the Active Hex Grid
    public static HexGridItem activeGrid;

    // Store all the hex grid chips from the player's path
    public List<HexGridItem> pathGrid = new List<HexGridItem>();

    // Hex Grid states' sprites
    public Sprite HexGridDef;
    public Sprite HexGridActive;
    public Sprite HexGridAdjacent;

    // Store all the Hex Grid Items
    public HexGridItem[] allHexGrids;

    // The Maximum Distance of the Adjacent Chips for any chip is 160px
    private float maxDistNeighborChips = 160f;


    // ---------------------------------- Player ------------------------------------------
    // Reference to the Player chip GO
    private GameObject playerChip;

    // Reference to the Player Script
    private Player PlayerScript;


    // ---------------------------------- Buttons -----------------------------------------
    // Store the Delivery button
    public GameObject deliveryButton;



    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- MOUSE -------------------------------------------------------
        // Store the offset for the Ingredient Selected chip when following the cursor
        offsetMouse = new Vector3(130, -80, 1); // new Vector3(100, -150, 1)

        // Store the offset for each ingredient when following the cursor
        offsetMouseTomato = new Vector3(-125, -15, 1); // new Vector3(-135, 5, 1)
        offsetMouseCarrot = new Vector3(-50, 50, 1); // new Vector3(-70, 65, 1)
        offsetMouseEggplant = new Vector3(-125, -15, 1); // new Vector3(-135, 5, 1)
        offsetMouseMushroom = new Vector3(-195, -15, 1); // new Vector3(-210, 5, 1)


        // ---------------------------------- CAMERA ------------------------------------------------------
        // Store the offset for each camera position in each scene
        backpackCamOffset = new Vector3(0, 0, -4);
        mapCamOffset = new Vector3(3000, 0, -4);
        calendarCamOffset = new Vector3(6000, 0, -4);

        // The game starts with the Calendar screen
        isInCalendar = true;


        // ---------------------------------- QUEST ORDERS ------------------------------------------------
        // Store all the Order Ingredients
        ingrOrders = FindObjectsOfType<OrderIngr>();


        // ---------------------------------- STEPS UI ----------------------------------------------------
        // At the start set the steps UI to the steps total
        stepsLeftTxt.text = "" + stepsTotal;


        // ---------------------------------- CALENDAR ----------------------------------------------------
        // Store all the day chips in an array
        calendarChips = FindObjectsOfType<DayCalendar>();

        // Reference to the Calendar Panel Script
        CalendarPanelScript = FindObjectOfType<CalendarPanel>();


        // ---------------------------------- PLAYER CHIP (MAP) -------------------------------------------
        // Access Player chip GO
        playerChip = GameObject.Find("Player chip");

        // Access the Player Script from the Player chip GO
        PlayerScript = playerChip.GetComponent<Player>();


        // ---------------------------------- MAP PATH ----------------------------------------------------
        // Store all the Hex Grids in its array
        allHexGrids = FindObjectsOfType<HexGridItem>();

        // Set the origin/start Active Grid chip to be the first Active Grid
        activeGrid = GameObject.FindGameObjectWithTag("Start").GetComponent<HexGridItem>();

        // Update the sprite of first Active Grid to be active
        activeGrid.GetComponent<SpriteRenderer>().sprite = HexGridActive;

        // Add the origin Active Grid chip to the path list
        pathGrid.Add(activeGrid);

        // Set the origin chip's adjacents
        SetSelectableAll();
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // ---------------------------------- MOVE WITH CURSOR ------------------------------------------
        // Track mouse position (related to the transform of the "World")
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // ---------------------------------- BACKPACK SCREEN ---------------------------------------
        // If the Backpack is focusing Map screen
        if (isInBackpack == true)
        {
            // ---------------------------------- ON RIGHT CLICK ----------------------------------
            // If pressed right click (only in the frame clicked) + the game's not in the Execution phase
            if (Input.GetMouseButtonDown(1) && isExecutionPhase == false)
            {
                // And If some ingredient has been selected
                if (emptyCursor == false && selectedIngr != null)
                {
                    // Unselect that ingredient
                    UnselectIngredient();
                }
            }


            // ---------------------------------- WHEN NOTHING IS SELECTED ------------------------
            // If nothing is selected
            if (emptyCursor == true)
            {
                // Loop through all the ClickerScripts on the scene
                for (int i = 0; i < ClickerScripts.Length; i = i + 1)
                {
                    // If one of this pantries has been clicked
                    if (ClickerScripts[i].isClicked == true)
                    {
                        // Deselect that ingredient
                        ClickerScripts[i].UnselectIngrUI(i);
                    }
                }
            }
        }


        // ---------------------------------- MAP SCREEN ---------------------------------------------
        // If the Camera is focusing Map screen
        else if (isInMap == true)
        {
            // ---------------------------------- ON RIGHT CLICK ----------------------------------
            // If pressed right click (only in the frame clicked) + the game's not in the Execution phase
            if (Input.GetMouseButtonDown(1) && isExecutionPhase == false)
            {
                // ---------------------------------- SET PATH TO DEFAULT -------------------
                // Loop through the path chips
                for (int i = 0; i < pathGrid.Count; i = i + 1)
                {
                    // If the grid chip isn't the start of the path
                    if (pathGrid[i].tag != "Start")
                    {
                        // Set all the path chips as default (not active/selectable)
                        pathGrid[i].SetDefault();
                    }
                }

                // ---------------------------------- PATH ----------------------------------
                // Restore the player's left steps
                stepsLeft = stepsTotal;

                // Reset the Steps Left text
                stepsLeftTxt.text = "" + stepsLeft;

                // Delete all the path grid chips from the path
                pathGrid.Clear();

                // Set the origin/start Active Grid chip to be the first Active Grid again
                activeGrid = GameObject.FindGameObjectWithTag("Start").GetComponent<HexGridItem>();

                // Add the origin grid chip to the path
                pathGrid.Add(activeGrid);

                // Set the origin chip's adjacents
                SetSelectableAll();
            }
        }
    }



    // ---------------------------------- FRAME-RATE INDEPENDENT 4 PHYSICS CALCULATIONS --------------------
    void FixedUpdate()
    {
        // ---------------------------------- MOVE WITH CURSOR ------------------------------------------
        // Set the ingredient selected's position to follow the mouse (with an offset)
        pIngredientSelected.transform.position = mousePos + offsetMouse;

        // If there's a Selected Ingredient
        if (selectedIngr != null)
        {
            // Set the Ingredient Selected position (following the mouse)
            SetOffsetIngr();
        }
    }



    // ---------------------------------- ASSIGN THE TODAY CHIP --------------------------------------------
    public void AssignToday()
    {
        // Loop through all the Calendar chips (28)
        for (int i = 0; i < calendarChips.Length; i = i + 1)
        {
            // Find the week & day currently happening
            if (calendarChips[i].GetComponent<DayCalendar>().weekID == CalendarScript.weeksUsed && calendarChips[i].GetComponent<DayCalendar>().dayID == CalendarScript.daysUsed)
            {
                // Store the last "current day"
                lastActiveDay = activeDay;

                // Set as a Today chip
                calendarChips[i].GetComponent<DayCalendar>().isToday = true;

                // Store that day chip as the active day
                activeDay = calendarChips[i].gameObject;

                // Update its Sprite to Today sprite
                DayUpdateSprite(i);
            }

            // For the rest of the days
            else
            {
                // Set as a Default chip
                calendarChips[i].GetComponent<DayCalendar>().isToday = false;

                // Update its Sprite to Default sprite
                DayUpdateSprite(i);
            }
        }
    }



    // ---------------------------------- ASSIGN THE CALENDAR CHIP UI --------------------------------------
    // Depending of the state of the Calendar chip (Today, Default or Selected), assign it a different UI display
    public void DayUpdateSprite(int i)
    {
        // If it's a Today chip
        if (calendarChips[i].gameObject == activeDay && calendarChips[i].GetComponent<DayCalendar>().isToday == true)
        {
            // And If it's a Mission day
            if (activeDay.tag == "Mission Day")
            {
                // Deactivate the Default Villager icon
                activeDay.transform.GetChild(0).gameObject.SetActive(false);

                // Activate the Today Villager icon
                activeDay.transform.GetChild(1).gameObject.SetActive(true);

                // Set it as Today (by changing its sprite)
                activeDay.GetComponent<SpriteRenderer>().sprite = dayToday;
            }

            // Or If it's a Regular day
            else
            {
                // Set it as Today (by changing its sprite)
                activeDay.GetComponent<SpriteRenderer>().sprite = dayToday;
            }
        }

        // If it's a Default chip
        else
        {
            // If it hasn't been selected
            if (calendarChips[i].GetComponent<DayCalendar>().isClicked == false)
            {
                // And If it's a Mission day
                if (calendarChips[i].tag == "Mission Day")
                {
                    // Activate the Default Villager icon
                    calendarChips[i].transform.GetChild(0).gameObject.SetActive(true);

                    // Deactivate the Today Villager icon
                    calendarChips[i].transform.GetChild(1).gameObject.SetActive(false);

                    // Set it as Today (by changing its sprite)
                    calendarChips[i].GetComponent<SpriteRenderer>().sprite = dayDefault;
                }

                // Or If it's a Regular day
                else
                {
                    // Set it as Today (by changing its sprite)
                    calendarChips[i].GetComponent<SpriteRenderer>().sprite = dayDefault;
                }
            }
            
            // If it has been selected
            else
            {
                // And If it's a Mission day
                if (calendarChips[i].tag == "Mission Day")
                {
                    // Activate the Default Villager icon
                    calendarChips[i].transform.GetChild(0).gameObject.SetActive(true);

                    // Deactivate the Today Villager icon
                    calendarChips[i].transform.GetChild(1).gameObject.SetActive(false);

                    // Set it as Today (by changing its sprite)
                    calendarChips[i].GetComponent<SpriteRenderer>().sprite = daySelected;
                }

                // Or If it's a Regular day
                else
                {
                    // Set it as Today (by changing its sprite)
                    calendarChips[i].GetComponent<SpriteRenderer>().sprite = daySelected;
                }
            }
        }

    }



    // ---------------------------------- DESELECT CALENDAR CHIPS ------------------------------------------
    // Deselect Calendar chips when another has been clicked
    public void DeselectCalendarChip()
    {
        // Loop through all the Calendar chips (28)
        for (int i = 0; i < calendarChips.Length; i = i + 1)
        {
            // Find the selected one
            if (calendarChips[i].isClicked == true)
            {
                // Access its Sprite Renderer
                SpriteRenderer daySpriteRenderer = calendarChips[i].GetComponent<SpriteRenderer>();

                // Set it as not clicked
                calendarChips[i].isClicked = false;

                // If it's not a today chip
                if (calendarChips[i].GetComponent<DayCalendar>().isToday == false)
                {
                    // Update its state to Default (by changing its sprite)
                    daySpriteRenderer.sprite = dayDefault;
                }
            }
        }
    }



    // ---------------------------------- SPAWN INGREDIENT COPIES ------------------------------------------
    public void SpawnIngredientGrid(int i)
    {
        // Ingredient selected
        emptyCursor = false;


        // ---------------------------------- DESTROY CHILDS ------------------------------------------
        // Destroy previous childs
        foreach (Transform child in gFeedback.transform)
        {
            Destroy(child.gameObject);
        }


        // Create a copy of that Ingredient Grid chip as a Child of gFeedback GO
        selectedIngr = Instantiate(gChipsFeedback[i], gFeedback.transform).GetComponent<IngSelectable>();
    }


    // ---------------------------------- CHANGE INGREDIENT SPRITES -----------------------------------------------------
    public void SetOffsetIngr()
    {
        // If the ingredient selected is a Tomato
        if (selectedIngr.gameObject.tag == "Tomato")
        {
            // Set the position of the Tomato selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseTomato;
        }

        // If the ingredient selected is a Carrot
        else if (selectedIngr.gameObject.tag == "Carrot")
        {
            // Set the position of the Carrot selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseCarrot;
        }

        // If the ingredient selected is a Eggplant
        else if (selectedIngr.gameObject.tag == "Eggplant")
        {
            // Set the position of the Eggplant selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseEggplant;
        }

        // If the ingredient selected is a Mushroom
        else if (selectedIngr.gameObject.tag == "Mushroom")
        {
            // Set the position of the Mushroom selected with an offset regarding the mouse position
            gFeedback.transform.position = mousePos + offsetMouseMushroom;
        }
    }


    // ---------------------------------- UNSELECT INGREDIENT -------------------------------------------------------------
    public void UnselectIngredient()
    {
        // ---------------------------------- DESTROY CHILDS ------------------------------------------
        // Delete the ingredient shape UI in the cursor
        foreach (Transform child in gFeedback.transform)
        {
            Destroy(child.gameObject);
        }

        // Deselect ingredient
        selectedIngr = null;

        // Set the cursor as empty
        emptyCursor = true;

        // Deactivate the renderer of the ingredient selected (as none is selected for now)
        pIngredientSelected.GetComponent<SpriteRenderer>().enabled = false;
    }


    // ---------------------------------- SET CHIPS AS SELECTABLE -----------------------------------------------------------
    public void SetSelectableAll()
    {
        // Loop throught all the Hex Grid chips
        for (int i = 0; i < allHexGrids.Length; i = i + 1)
        {
            // The looped chip is not from the path
            if (!pathGrid.Contains(allHexGrids[i]))
            {
                // If the looped grid chip is adjacent to the active one (check the distance between this hex grid and the active one)
                if (Vector3.Distance(activeGrid.transform.position, allHexGrids[i].gameObject.transform.position) < maxDistNeighborChips)
                {
                    // If the chips aren't the active chip
                    if (activeGrid != allHexGrids[i])
                    {
                        // Update the active's adjacent grid chips to selectable
                        allHexGrids[i].gameObject.GetComponent<SpriteRenderer>().sprite = HexGridAdjacent;
                    }
                }

                else
                {
                    // Update the sprite of the chips outside of the range as Default
                    allHexGrids[i].gameObject.GetComponent<SpriteRenderer>().sprite = HexGridDef;
                }
            }
        }
    }


    // ---------------------------------- SET DEFAULT WHEN NO STEPS LEFT ---------------------------------------------------------
    public void NoStepsLeft()
    {
        // Loop throught all the Hex Grid chips
        for (int i = 0; i < allHexGrids.Length; i = i + 1)
        {
            // The looped chip is not from the path
            if (!pathGrid.Contains(allHexGrids[i]))
            {
                // Update the sprite of all the chips as Default
                allHexGrids[i].gameObject.GetComponent<SpriteRenderer>().sprite = HexGridDef;
            }
        }
    }


    // ---------------------------------- GIVE INFO TO UPDATE ORDER INGR ---------------------------------------------------------
    // Give the information to update de Order Ingredient UI (text)
    public void CheckIngrUpdate()
    {
        // Loop through all the Order Ingredients
        for (int i = 0; i < ingrOrders.Length; i = i + 1)
        {
            // Update their info to update their UIs
            ingrOrders[i].GetComponent<OrderIngr>().UpdateQuantity();
        }
    }



    // ---------------------------------- RESET DAY ------------------------------------------------------------------------------
    // Reset the day as new one
    public void ResetDay()
    {
        // ---------------------------------- CHANGE GAME PHASE ---------------------------------------
        // The game enters in the Execution Phase
        isExecutionPhase = false;


        // ---------------------------------- UPDATE COMPACTED CALENDAR UI ----------------------------
        if (CalendarScript.daysUsed < 10)
        {
            // Update the Current Day text
            currentDayTxt.text = "0" + CalendarScript.daysUsed;
        }

        else
        {
            // Update the Current Day text
            currentDayTxt.text = "" + CalendarScript.daysUsed;
        }


        // Update the Week Day text
        currentWeekDayTxt.text = "" + CalendarScript.activeWeekDay.tag;


        // ---------------------------------- RESET STEPS AVAILABLE -----------------------------------
        // Update Calendar's +Detail Panel with the current left days
        CalendarPanelScript.CheckQuestToDisplay();


        // ---------------------------------- RESET STEPS AVAILABLE -----------------------------------
        // At the start of the day reset steps
        stepsLeft = stepsTotal;

        // Update the Steps UI with the Steps Left
        stepsLeftTxt.text = "" + stepsLeft;


        // ---------------------------------- UPDATE MONEY UPGRADES -----------------------------------
        // Update the Player's Money UI Text
        playerMoneyTxt.text = "" + playerMoney;


        // ---------------------------------- RESET CAMERA TO MAIN POS --------------------------------
        // Set the Camera in the Calendar Screen
        CameraToCalendar();


        // ---------------------------------- RESET MAP -----------------------------------------------
        // Reactivate the deliver button
        deliveryButton.SetActive(true);

        // Reset path array
        pathGrid.Clear();

        // Go through each Hex Grid
        for (int i = 0; i < allHexGrids.Length; i = i + 1)
        {
            // And update their sprite to the default one
            allHexGrids[i].SetDefault();
        }

        // Set the origin/start Active Grid chip to be the first Active Grid
        activeGrid = GameObject.FindGameObjectWithTag("Start").GetComponent<HexGridItem>();

        // Update the sprite of first Active Grid to be active
        activeGrid.GetComponent<SpriteRenderer>().sprite = HexGridActive;

        // Add the origin Active Grid chip to the path list
        pathGrid.Add(activeGrid);

        // Reset the Player chip to the Start path position
        playerChip.transform.position = activeGrid.transform.position;


        // ---------------------------------- EMPTY INVENTORY -----------------------------------------
        // Empty the inventory of all the ingredients that remain stored in it
        EmptyInventory();


        // ---------------------------------- REASSIGN TODAY CHIP -------------------------------------
        // Assign the new Today day chip
        AssignToday();


        // ---------------------------------- SET PAST DAY'S UI ---------------------------------------
        // Update past day's UI
        UpdatePastDayUI();

    }



    // ---------------------------------- UPDATE PAST DAY'S UI ------------------------------------------------------------------
    public void UpdatePastDayUI()
    {
        // Reference to Day's 3rd GO child
        GameObject PastDayUI = lastActiveDay.transform.GetChild(2).gameObject;

        // Reference to Past Day GO's Sprite Renderer
        SpriteRenderer PastDaySR = PastDayUI.GetComponent<SpriteRenderer>();

        // Reference to Past Day's Script (Day Calendar)
        DayCalendar ChipDayScript = lastActiveDay.GetComponent<DayCalendar>();


        // If it's a mission day
        if (lastActiveDay.tag == "Mission Day")
        {
            // Active the Past Day GO
            PastDayUI.SetActive(true);


            // If the Day's Quest has been completed
            if (ChipDayScript.questOwnerScript.isQuestCompleted == true)
            {
                // Set its sprite to the one past quest day with the mission completed
                PastDaySR.sprite = pastQuestCompleted;
            }

            // If the Day's Quest is uncompleted
            else
            {
                // Set its sprite to the past quest day with the mission uncompleted
                PastDaySR.sprite = pastQuestNotCompleted;
            }
        }

        // If it's a regular day
        else
        {
            // Active the Past Day GO
            PastDayUI.SetActive(true);

            // Set its sprite to the past default day
            PastDaySR.sprite = pastDay;
        }
    }



    // ---------------------------------- EMPTY INVENTORY -----------------------------------------------------------------------
    public void EmptyInventory()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Store all the Ingredients that are currently in the Backpack
        IngSelectable[] ingrInBackpack = FindObjectsOfType<IngSelectable>();

        // Store all the Grid chips of the Backpack
        GridItem[] backpackGridChips = FindObjectsOfType<GridItem>();

        // ---------------------------------- EMPTY BACKPACK'S INGREDIENTS PLACED ---------------
        // Empty Inventory (destroy all the ingredients that are currently in Backpack)
        for (int i = 0; i < ingrInBackpack.Length; i = i + 1)
        {
            Destroy(ingrInBackpack[i].gameObject);
        }


        // ---------------------------------- EMPTY BACKPACK GRID CHIPS -------------------------
        // Empty all the Backpack's grid chips
        for (int i = 0; i < backpackGridChips.Length; i = i + 1)
        {
            backpackGridChips[i].GetComponent<GridItem>().gridIsEmpty = true;
        }


        // ---------------------------------- EMPTY COUNTERS ------------------------------------
        // Empty all the Backpack's Ingredients counters
        tomatosPlaced = 0;
        carrotsPlaced = 0;
        eggplantsPlaced = 0;
        mushroomsPlaced = 0;

        // Empty all the Backpack's Ingredients counters
        /*for (int i = 0; i < ingredientsPlaced.Length; i = i + 1)
        {
            Debug.Log("Vacia los contadores");

            ingredientsPlaced[i] = 0;
        }*/
    }


    // ---------------------------------- CAMERA FOCUS CALENDAR -----------------------------------------------------------------
    // Move Camera to Calendar screen position
    public void CameraToCalendar()
    {
        // If the game is not in the Execution phase
        if (isExecutionPhase == false)
        {
            // ---------------------------------- EMPTY CURSOR UI -----------------------------------
            // If there's an ingredient selected
            if (emptyCursor == false)
            {
                // Unselect Ingredient
                UnselectIngredient();
            }


            // ---------------------------------- CHANGE CAMERA POSITION ----------------------------
            // Set the Main Camera's position as the Calendar screen position
            mainCamera.transform.position = calendarCamOffset;


            // ---------------------------------- SET CAMERA BOOLS ----------------------------------
            // Store that Camera is focusing the Calendar screen
            isInCalendar = true;

            // Store that Camera isn't focusing in the Backpack screen
            isInBackpack = false;

            // Store that Camera isn't focusing in the Map screen
            isInMap = false;
        }
    }


    // ---------------------------------- CAMERA FOCUS BACKPACK -----------------------------------------------------------------
    // Move Camera to Backpack screen position
    public void CameraToBackpack()
    {
        // If the game is not in the Execution phase
        if (isExecutionPhase == false)
        {
            // ---------------------------------- EMPTY CURSOR UI -----------------------------------
            // If there's an ingredient selected
            if (emptyCursor == false)
            {
                // Unselect Ingredient
                UnselectIngredient();
            }


            // ---------------------------------- CHANGE CAMERA POSITION ----------------------------
            // Set the Main Camera's position as the Calendar screen position
            mainCamera.transform.position = backpackCamOffset;


            // ---------------------------------- SET CAMERA BOOLS ----------------------------------
            // Store that Camera is focusing the Calendar screen
            isInCalendar = false;

            // Store that Camera isn't focusing in the Backpack screen
            isInBackpack = true;

            // Store that Camera isn't focusing in the Map screen
            isInMap = false;
        }
    }


    // ---------------------------------- CAMERA FOCUS MAP ----------------------------------------------------------------------
    // Move Camera to Map screen position
    public void CameraToMap()
    {
        // If the game is not in the Execution phase
        if (isExecutionPhase == false)
        {
            // ---------------------------------- EMPTY CURSOR UI -----------------------------------
            // If there's an ingredient selected
            if (emptyCursor == false)
            {
                // Unselect Ingredient
                UnselectIngredient();
            }


            // ---------------------------------- CHANGE CAMERA POSITION ----------------------------
            // Set the Main Camera's position as the Calendar screen position
            mainCamera.transform.position = mapCamOffset;


            // ---------------------------------- SET CAMERA BOOLS ----------------------------------
            // Store that Camera is focusing the Calendar screen
            isInCalendar = false;

            // Store that Camera isn't focusing in the Backpack screen
            isInBackpack = false;

            // Store that Camera isn't focusing in the Map screen
            isInMap = true;
        }
    }



    // ---------------------------------- RESTART GAME --------------------------------------------------------------------------
    // When restart button clicked, restart game from 0
    public void RestartGame()
    {
        // Restart Game (by restarting the scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // ---------------------------------- QUIT GAME -----------------------------------------------------------------------------
    // When quit button clicked, quit game
    public void QuitGame()
    {
        // Close the game
        Application.Quit();
    }
}

