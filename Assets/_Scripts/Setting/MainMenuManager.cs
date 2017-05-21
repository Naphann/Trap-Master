using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public Transform teamMenu, mainMenu, teamAReady, teamBReady, trapMenu, tutorial;
    public int teamID = 0;
    public string playerName;

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
        }
        else
        {
            trapMenu.gameObject.SetActive(clicked);
            teamBReady.gameObject.SetActive(true);
        }

    }
}
