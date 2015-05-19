using UnityEngine;
using System.Collections;

public class playerWinMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        Application.LoadLevel("MainScreen2");
    }
}
