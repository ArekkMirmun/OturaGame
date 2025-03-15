using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        // Load the game scene
        SceneManager.LoadScene("Game");
    }
    public void LoadMenuScene()
    {
        // Load the menu scene
        SceneManager.LoadScene("Menu");
    }
    public void LoadCinematicScene()
    {
        // Load the cinematic scene
        SceneManager.LoadScene("Cinematic");
    }
    public void CloseGame()
    {
        // Close the game
        Application.Quit();
    }
}
