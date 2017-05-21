using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeActivation : MonoBehaviour, ITrapActivation {

    public float effectiveRadius;
    // Use this for initialization
    void Start() {
        gameObject.GetComponent<SphereCollider>().radius = effectiveRadius;
    }

    public void Activate() {
        var center = gameObject.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, effectiveRadius);

        foreach (var hitCollider in hitColliders) {
            var GO = hitCollider.gameObject;
            if (GO.CompareTag("Player")) {
                GO.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, 101.0f);
            }
        }
    }
}
