using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxinActivation : MonoBehaviour, ITrapActivation {

    public float effectiveRadius;
    public float toxinHPDamage;
    public float toxinO2Damage;
    private bool isActive;
    private HashSet<GameObject> playersInEffect;
    // Use this for initialization
    void Start() {
        isActive = false;
        playersInEffect = new HashSet<GameObject>();
        gameObject.GetComponent<SphereCollider>().radius = effectiveRadius;
    }

    // Update is called once per frame
    void Update() {        
        if (isActive) {
            Debug.Log("Player taking damage: " + playersInEffect.Count);
            Debug.Log("damage: " + toxinHPDamage);
            foreach (var playerGO in playersInEffect) {                
                playerGO.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, toxinHPDamage * Time.deltaTime);
                playerGO.GetComponent<PhotonView>().RPC("ReduceO2", PhotonTargets.All, toxinO2Damage * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            playersInEffect.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            playersInEffect.Remove(other.gameObject);
        }
    }

    public void Activate() {
        isActive = true;
    }
}
