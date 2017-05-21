using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityActivation : MonoBehaviour, ITrapActivation {

    public float effectiveRadius;
    public float minForce;
    public float maxForce;
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
            foreach (var playerGO in playersInEffect) {
                var rb = playerGO.GetComponent<Rigidbody>();
                var gravity = gameObject.transform.position - rb.transform.position;
                var distance = gravity.magnitude;
                gravity.Normalize();
                
                Vector3 playerVelocity = playerGO.GetComponent<CharacterController>().velocity;
                if (distance < effectiveRadius / 2) {
                    playerGO.GetComponent<CharacterController>().Move(gravity * Time.deltaTime * maxForce);
                } else if (distance < effectiveRadius) {
                    playerGO.GetComponent<CharacterController>().Move(gravity * Time.deltaTime * minForce);
                }

                //Debug.Log("pulling with force = " + gravity.ToString());
                
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
