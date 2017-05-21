using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public float MaxHP;
    public float MaxO2;
    private float currentHP;
    private float currentO2;
	// Use this for initialization
	void Start () {
        currentHP = MaxHP;
        currentO2 = MaxO2;
	}
	
	// Update is called once per frame
	void Update () {        
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
}
