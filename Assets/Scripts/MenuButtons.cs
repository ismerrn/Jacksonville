using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // When clicking the Game button, start game
    public void StartGame()
    {
        // Load "Game" scene
        SceneManager.LoadScene("Game");
    }

    // When cliking Exit Game button, quit game app
    public void QuitGame()
    {
        // Print "Exit game" in the Console
        Debug.Log("Exit game");

        // Close game app
        Application.Quit();
    }

    /*public void CreditsGame() //Al apretar el botón de Créditos carga la escena de los Créditos.
    {
        SceneManager.LoadScene("Credits");
    }*/

    /*public void ReturnGame() //Al apretar el botón de Return en los Créditos, carga la escena del Menú otra vez.
    {
        SceneManager.LoadScene("Menú");
    }*/
}
