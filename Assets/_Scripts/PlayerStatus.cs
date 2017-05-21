using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public float MaxHP;
    public float MaxO2;
    public float maxFlashDuration;
    public CanvasGroup myCG;
    private float currentHP;
    private float currentO2;
    private bool isFlash;
    private float flashIntensity;
    // Use this for initialization
    void Start() {
        currentHP = MaxHP;
        currentO2 = MaxO2;
        isFlash = false;
        myCG.alpha = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (isFlash) {
            Debug.Log("current intensity " + myCG.alpha);
            float minus = Time.deltaTime / maxFlashDuration;
            if (myCG.alpha > 0.95f)
                minus /= 10;
            else if (myCG.alpha > .9f)
                minus /= 3;
            else
                minus *= 3;
            myCG.alpha -= minus;
            if (myCG.alpha <= 0) {
                isFlash = false;
                myCG.alpha = 0f;
            }
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
        Debug.Log("Flash from Player with intensity = " + intensity);
        isFlash = true;
        myCG.alpha = intensity;
    }
}
