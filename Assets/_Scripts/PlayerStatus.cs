using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public float MaxHP;
    public float MaxO2;
    public float maxFlashDuration;
    private float currentHP;
    private float currentO2;

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
    }

    // Update is called once per frame
    void Update() {
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
    }

    [PunRPC]
    public void TakeDamage(float damage) {
        currentHP -= damage;
        Debug.Log("Take Damage!!! current HP: " + currentHP);
    }

    [PunRPC]
    public void ReduceO2(float amt) {
        currentO2 -= amt;
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
