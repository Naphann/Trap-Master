using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public int group = 0;
    public GameObject stanbyCamera;
    private SpawnSpot[] spawnSpots;
    public bool isOfflineMode;
    // Use this for initialization
    void Start() {
        spawnSpots = FindObjectsOfType<SpawnSpot>();
        Connect();
    }

    void Connect() {
        if (isOfflineMode) {
            PhotonNetwork.offlineMode = true;
            OnJoinedLobby();
        } else {
            PhotonNetwork.ConnectUsingSettings("Trap-Master:0.0.1");
        }
    }

    private void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby() {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("OnJoinedRandomRoomFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() {
        Debug.Log("Yay Joined Room");
        SpawnMyPlayer();
    }

    void SpawnMyPlayer() {
        var mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayerGO = PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, group);
        stanbyCamera.SetActive(false);
        ((MonoBehaviour) myPlayerGO.GetComponent("FirstPersonController")).enabled = true;
        ((MonoBehaviour) myPlayerGO.transform.GetComponent("PlayerLayingTrap")).enabled = true;
        myPlayerGO.transform.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = true;
        myPlayerGO.transform.Find("FirstPersonCharacter").GetComponent<AudioListener>().enabled = true;
        Debug.Log("Spawned My Player");
    }
}
