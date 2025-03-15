using UnityEngine;

public class ChurchDoor : MonoBehaviour
{
    public GameObject interactableIndicator;
    public bool isInteractable = false;
    public SceneController sceneController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Get player instance
            Player player = Player.Instance;
            
            //Set the church door to the player
            player.churchDoor = this;
            
            interactableIndicator.SetActive(true);
            isInteractable = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //Get player instance
            Player player = Player.Instance;
            
            //Set the church door to null
            player.churchDoor = null;
            
            interactableIndicator.SetActive(false);
            isInteractable = false;
        }
    }
    
    public void Interact()
    {
        if (isInteractable)
        {
            sceneController.LoadCinematicScene();
        }
    }
}
