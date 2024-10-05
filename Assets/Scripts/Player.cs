using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ---------------------------------- FLOAT ----------------------------------------------------
    // Store the speed at which the player will execute the path
    public float executionSpeed = 500;

    // Set the time the Player chip will stop in each collision (House, Inn, Event, etc.)
    public float pathStopTime = 2f;


    // ---------------------------------- INT ------------------------------------------------------
    // Store positions of path grids
    private int index = 0;


    // ---------------------------------- BOOL -----------------------------------------------------
    // Check if collided with House or Inns
    public bool hasCollidedHouse = false;
    public bool hasCollidedInn = false;


    // ---------------------------------- GAME CONTROLLER ------------------------------------------
    // Reference to the Game Controller GO
    private GameObject gameController;

    // Access Game Controller script
    private GameController GameControllerScript;

    // Access Calendar script
    private Calendar CalendarScript;


    // ---------------------------------- DELIVERY BUTTON ------------------------------------------
    // Reference to the Game Controller GO
    public Button deliveryButton;


    // ---------------------------------- AT THE START OF THE GAME ------------------------------------------
    void Start()
    {
        // ---------------------------------- ACCESS --------------------------------------------
        // Access Game Controller GO
        gameController = GameObject.Find("Game Controller");

        // Access the Game Controller Script from the Game Controller GO
        GameControllerScript = gameController.GetComponent<GameController>();

        // Access the Calendar Script from the Game Controller GO
        CalendarScript = gameController.GetComponent<Calendar>();
    }


    // ---------------------------------- EACH FRAME -------------------------------------------------------
    void Update()
    {
        // If the Delivery Button has been clicked
        if (deliveryButton.GetComponent<DeliverBut>().isDeliveryClicked == true)
        {
            // If the Player chip doesn't collide in the path with any event (House, Inn, etc.)
            if (!hasCollidedHouse && !hasCollidedInn)
            {
                // Start the Execution Phase
                ExecutionPhase();
            }
        }
    }


    // ---------------------------------- ON COLLISION ------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Player chip collides with a House
        if (collision.CompareTag("House"))
        {
            // Set that it has collided with a House
            hasCollidedHouse = true;

            // Unset the collision in the defined Path Stop Time (1sec)
            Invoke("ResumePath", pathStopTime);

            if (collision.name == "Mark")
            {
                Debug.Log("Toma Mark");
                collision.GetComponent<Quest>().DeliverQuest();
            }

            if (collision.name == "Tim")
            {
                Debug.Log("Toma Tim");
                collision.GetComponent<Quest>().DeliverQuest();
            }

            if (collision.name == "Felix")
            {
                Debug.Log("Toma Felix");
                collision.GetComponent<Quest>().DeliverQuest();
            }
        }


        // If the Player chip collides with an Inn
        else if (collision.CompareTag("Inn"))
        {
            // Set that it has collided with an Inn
            hasCollidedInn = true;

            // Unset the collision in the defined Path Stop Time (1sec)
            Invoke("ResumePath", pathStopTime);
        }
    }

    // ---------------------------------- START THE EXECUTION (RESULT) PHASE ------------------------------------------------------
    public void ExecutionPhase()
    {
        // Lock the Cursor
        //Cursor.lockState = CursorLockMode.Locked;

        // The game enters in the Execution Phase
        GameControllerScript.isExecutionPhase = true;

        // Store the next chip position
        Vector3 destination = GameControllerScript.pathGrid[index].transform.position;

        // Make the Player chip move from first chip to second chip
        transform.position = Vector3.MoveTowards(transform.position, destination, executionSpeed * Time.deltaTime);

        // Calculate the distance between the Player chip's current position and it's next chip
        float distance = Vector3.Distance(transform.position, destination);

        // If it's near, go next
        if (distance <= 0.05)
        {
            // If there's still some path chips left to walk
            if (index < GameControllerScript.pathGrid.Count-1)
            {
                // Go next chip
                index++;
            }

            // What to do when it's there's no path left
            else
            {
                // Este 0 puedes estar rompiendo algo
                index = 0;

                // Set the Delivery Button as not clicked
                deliveryButton.GetComponent<DeliverBut>().isDeliveryClicked = false;

                // Go next day
                CalendarScript.daysUsed++;

                // Make the time pass
                CalendarScript.TimePass();

                // Make the time pass
                CalendarScript.DaysPass();

                // Crear una función (y llamarla) que resetee el día
                GameControllerScript.ResetDay();
            }
        }
    }

    // ---------------------------------- RESTART PATH METHOD ----------------------------------------------------------------------
    // If no collision = restart path
    public void ResumePath()
    {
        // Set no collisions
        hasCollidedHouse = false;
        hasCollidedInn = false;
    }
}
