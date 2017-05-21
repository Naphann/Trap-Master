using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TrapMenuManager : MonoBehaviour {
    public Transform trapMenu;
    public float timeLeft = 30.0f;
    public Text text;
    // Use this for initialization

    public void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "Time Left:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            GameObject.Find("_Scripts").GetComponent<NetworkManager>().Connect();
            this.enabled = false;
        }
    }


}
