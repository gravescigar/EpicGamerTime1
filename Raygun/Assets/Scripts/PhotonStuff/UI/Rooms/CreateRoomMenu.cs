using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    public void OnClick_CreateRoom()
    {
        if (PhotonNetwork.IsConnected && _roomName.text.Length > 0)
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = 20,
                PlayerTtl = 600000,
                EmptyRoomTtl = 0
            };
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        }
        else
            return;
    }

    public override void OnCreatedRoom()
    {
        print("Created room successfully.");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Failed to create room.");
    }
}
