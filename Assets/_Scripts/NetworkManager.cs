using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {

    public int group = 0;
    public GameObject stanbyCamera;
    private SpawnSpot[] spawnSpots;
    public bool isOfflineMode;
    public Transform teamMenu, mainMenu, teamAReady, teamBReady, trapMenu, tutorial, healthBar;
    public int teamID = 0;
    public string playerName;
    public string traptype;
    // Use this for initialization
    void Start() {
        spawnSpots = FindObjectsOfType<SpawnSpot>();
        
    }


    public void Connect() {
        if (isOfflineMode) {
            PhotonNetwork.offlineMode = true;
            OnJoinedLobby();
            SpawnMyPlayer();
            
            
        } else {
            PhotonNetwork.ConnectUsingSettings("Trap-Master:0.0.1");
        }
    }

    private void OnGUI()
    {
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
        // SpawnMyPlayer();
    }

    void SpawnMyPlayer() {
        var mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayerGO = PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, group);
        stanbyCamera.SetActive(false);
        ((MonoBehaviour) myPlayerGO.GetComponent("FirstPersonController")).enabled = true;
        ((MonoBehaviour) myPlayerGO.transform.GetComponent("PlayerLayingTrap")).enabled = true;
        myPlayerGO.transform.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = true;
        myPlayerGO.transform.Find("FirstPersonCharacter").GetComponent<AudioListener>().enabled = true;
        myPlayerGO.GetComponent<PlayerStatus>().teamID = this.teamID;
        myPlayerGO.GetComponent<PlayerLayingTrap>().traptype = this.traptype;
        healthBar.gameObject.SetActive(true);
        trapMenu.gameObject.SetActive(false);
        Debug.Log("Spawned My Player");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    public void ShowTutorial(bool clicked)
    {
        if (clicked == true)
        {
            tutorial.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
            playerName = gameObject.GetComponent<InputField>().text;
            Debug.Log(playerName);
        }
        else
        {
            tutorial.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }

    }
    public void ReadyToChooseTeam (bool clicked) {
        if (clicked == true)
        {
            teamMenu.gameObject.SetActive(clicked);
            tutorial.gameObject.SetActive(false);
        } else
        {
            teamMenu.gameObject.SetActive(clicked);
            tutorial.gameObject.SetActive(true);
        }
		
	}

    public void WaitingTeamAReady(bool clicked)
    {
        if (clicked == true)
        {
            teamAReady.gameObject.SetActive(clicked);
            teamMenu.gameObject.SetActive(false);
        }
        else
        {
            teamAReady.gameObject.SetActive(clicked);
            teamMenu.gameObject.SetActive(true);
        }

    }
    public void WaitingTeamBReady(bool clicked)
    {
        if (clicked == true)
        {
            teamBReady.gameObject.SetActive(clicked);
            teamMenu.gameObject.SetActive(false);
        }
        else
        {
            teamBReady.gameObject.SetActive(clicked);
            teamMenu.gameObject.SetActive(true);
        }

    }

    public void AChooseTrap(bool clicked)
    {
        if (clicked == true)
        {
            trapMenu.gameObject.SetActive(clicked);
            teamAReady.gameObject.SetActive(false);
            teamID = 1;
            gameObject.GetComponent<TrapMenuManager>().Update();

        }
        else
        {
            trapMenu.gameObject.SetActive(clicked);
            teamAReady.gameObject.SetActive(true);
        }

    }
    public void BChooseTrap(bool clicked)
    {
        if (clicked == true)
        {
            trapMenu.gameObject.SetActive(clicked);
            teamBReady.gameObject.SetActive(false);
            teamID = 2;
            gameObject.GetComponent<TrapMenuManager>().Update();
        }
        else
        {
            trapMenu.gameObject.SetActive(clicked);
            teamBReady.gameObject.SetActive(true);
        }

    }
}
