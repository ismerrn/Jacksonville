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

    /*public void CreditsGame() //Al apretar el bot�n de Cr�ditos carga la escena de los Cr�ditos.
    {
        SceneManager.LoadScene("Credits");
    }*/

    /*public void ReturnGame() //Al apretar el bot�n de Return en los Cr�ditos, carga la escena del Men� otra vez.
    {
        SceneManager.LoadScene("Men�");
    }*/
}
