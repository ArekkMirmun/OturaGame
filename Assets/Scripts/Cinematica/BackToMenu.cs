using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void OnBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
