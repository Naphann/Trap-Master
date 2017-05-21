using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActivation : MonoBehaviour, ITrapActivation {

    public float effectiveRadius;
    public float instadeathRadius;
    private float interval;
    // Use this for initialization
    void Start() {
        gameObject.GetComponent<SphereCollider>().radius = effectiveRadius;
        interval = effectiveRadius - instadeathRadius;
    }

    public void Activate() {
        var center = gameObject.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, effectiveRadius);

        foreach (var hitCollider in hitColliders) {
            var GO = hitCollider.gameObject;
            if (GO.CompareTag("Player")) {
                float damage = calculateDamageFromDistance(GO.transform.position);
                GO.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, damage);
            }
        }
    }

    float calculateDamageFromDistance(Vector3 PlayerPos) {
        float dist = (gameObject.transform.position - PlayerPos).magnitude;
        return 90.0f + (effectiveRadius - dist) * 10 / interval;
    }
}

