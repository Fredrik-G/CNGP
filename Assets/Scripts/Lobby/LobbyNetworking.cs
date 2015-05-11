using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LobbyNetworking
{

    private PhotonPlayer[] _players;

    public void test()
    {
        _players = PhotonNetwork.playerList;
        for (var i = 0; i < _players.Length; i++)
        {
           // TeamMembersText[i].text = _players[i].name;
        }
    }

    public void OnGUI()
    {
        test();
    }

    //public void OnPhotonPlayerConnected(PhotonPlayer player)
    //{
    //    Debug.Log("Player Connected " + player.name);
    //    DisplayTeamMembers();
    //}

    //public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    //{
    //    Debug.Log("Player Disconnected " + player.name);
    //    DisplayTeamMembers();
    //}

    private void DisplayTeamMembers()
    {
        _players = PhotonNetwork.playerList;
        for(var i = 0; i<_players.Length; i ++)
        {
           // TeamMembersText[i].text = _players[i].name;
        }
    }
}
