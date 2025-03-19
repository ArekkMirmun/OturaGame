using UnityEngine;

public class ResetCursor : MonoBehaviour
{
    public void Start()
    {
        //Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
