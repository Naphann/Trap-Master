using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashActivation : MonoBehaviour, ITrapActivation {

    public float effectiveRadius;
    // Use this for initialization
    void Start() {
        gameObject.GetComponent<SphereCollider>().radius = effectiveRadius;
    }

    public void Activate() {
        var center = gameObject.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, effectiveRadius);
        //Debug.Log("FLash!!!!");
        foreach (var hitCollider in hitColliders) {
            float intensity = 1f;
            var GO = hitCollider.gameObject;
            var playerFacing = GO.transform.forward;
            var diffVector = gameObject.transform.position - GO.transform.position;
            var angle = Vector3.Angle(playerFacing, diffVector);
            intensity = angle > 135 ? 0f : (135 - angle) / 135 + 0.15f;        
            if (GO.CompareTag("Player")) {
                GO.GetComponent<PhotonView>().RPC("FlashScreen", PhotonTargets.All, intensity);
            }
        }
    }
}
