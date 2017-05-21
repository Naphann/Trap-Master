using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    public float MaxHP;
    public float MaxO2;
    public float maxFlashDuration;
    private float currentHP;
    private float currentO2;

    // for ui
    public int teamID;
    public CanvasGroup showHPCG;
    public CanvasGroup showO2CG;
    public CanvasGroup showTeamCG;
    private Text showHP;
    private Text showOxy;
    private Text showTeam;
    private float nextUpdateUI;
    private float lastUpdateUI;

    // var for flash screen    
    public CanvasGroup flashPanel;
    private bool isFlash;
    private float flashIntensity;

    // variables for moving by outside force
    private Vector3 moveDir;
    private float moveDist;
    private float moveSpan;

    // Use this for initialization
    void Start() {
        currentHP = MaxHP;
        currentO2 = MaxO2;
        isFlash = false;
        flashPanel.alpha = 0.0f;

        showHP = showHPCG.GetComponent<Text>();
        showOxy = showO2CG.GetComponent<Text>();
        showTeam = showTeamCG.GetComponent<Text>();

        nextUpdateUI = 0f;
        lastUpdateUI = 0f;
    }

    // Update is called once per frame
    void Update() {
        if (nextUpdateUI <= lastUpdateUI) {
            showHP.text = "HP : " + ((int)currentHP).ToString();
            showOxy.text = "OXYGEN : " + ((int)currentO2).ToString();
            showTeam.text = "TEAM: " + teamID.ToString();
            nextUpdateUI += 0.5f;
        }
        lastUpdateUI += Time.deltaTime;
        if (isFlash) {
            //Debug.Log("current intensity " + myCG.alpha);
            float minus = Time.deltaTime / maxFlashDuration;
            if (flashPanel.alpha > 0.95f)
                minus /= 10;
            else if (flashPanel.alpha > .9f)
                minus /= 3;
            else
                minus *= 3;
            flashPanel.alpha -= minus;
            if (flashPanel.alpha <= 0) {
                isFlash = false;
                flashPanel.alpha = 0f;
            }
        }
        if (moveSpan >= 0) {
            moveSpan -= Time.deltaTime;
            gameObject.GetComponent<CharacterController>().Move(moveDir * Time.deltaTime);
        }
        currentO2 -= Time.deltaTime;

    }

    [PunRPC]
    public void TakeDamage(float damage) {
        currentHP -= damage;
        Debug.Log("Take Damage!!! current HP: " + currentHP);
        if (currentHP <= 0) {
            Die();
        }
    }

    [PunRPC]
    public void ReduceO2(float amt) {
        currentO2 -= amt;
        if (currentO2 <= 0) {
            Die();
        }
    }

    public void Die() {
        if (GetComponent<PhotonView>().isMine) {
            if (gameObject.CompareTag("Player")) {
                NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();

                nm.stanbyCamera.SetActive(true);
                nm.respawnTimer = 5f;
            }
        }

        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void FlashScreen(float intensity) {
        //Debug.Log("Flash from Player with intensity = " + intensity);
        isFlash = true;
        flashPanel.alpha = intensity;
    }

    [PunRPC]
    public void MoveByForce(Vector3 dir, float distance, float timespan) {
        moveDir = dir * distance / timespan;
        moveSpan = timespan;
        Debug.Log("call MoveByForce with params" + dir.ToString() + " " + distance + " " + timespan);
    }
}
