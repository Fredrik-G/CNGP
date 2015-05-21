using UnityEngine;

public class HandleStart : MonoBehaviour
{
    public LobbySounds SoundController;

    public void HandleMouseClick()
    {
        SoundController.StartGame();
        Debug.Log("Click");
    }
}