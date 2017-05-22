using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapMenuManager : MonoBehaviour {
    public Transform trapMenu;
    public float timeLeft = 30.0f;
    public Text text;
    public string traptype;

    // Use this for initialization

    public void Update() {
        this.enabled = true;
        if (text != null && trapMenu != null) {
            timeLeft -= Time.deltaTime;
            text.text = "Time Left:" + Mathf.Round(timeLeft);
            if (timeLeft < 0) {

                GameObject.Find("_Scripts").GetComponent<NetworkManager>().traptype = this.traptype;
                GameObject.Find("_Scripts").GetComponent<NetworkManager>().Connect();
                this.enabled = false;
            }
        }
    }

    public void Clicked(bool clicked)
    {
        if (clicked)
        {
            this.traptype = EventSystem.current.currentSelectedGameObject.name;
            Debug.Log("select trap " + traptype);
            Update();
        }
    }


}
