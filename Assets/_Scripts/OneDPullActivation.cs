using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDPullActivation : MonoBehaviour, ITrapActivation {

    public float effectiveRange = 20;
    public float effectiveWidth = 3;
    public float pullForce;
    private bool isActive;
    private HashSet<GameObject> playersInEffect;
    // Use this for initialization
    void Start() {
        isActive = false;
        playersInEffect = new HashSet<GameObject>();
        var boxCollider = gameObject.GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(effectiveWidth, effectiveRange, effectiveRange);
        boxCollider.center = new Vector3(0, 0, effectiveRange / 2);
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            //Debug.Log("PUlling");
            foreach (var playerGO in playersInEffect) {
                var rb = playerGO.GetComponent<Rigidbody>();
                var gravity = gameObject.transform.position - rb.transform.position;
                var distance = gravity.magnitude;
                gravity.Normalize();
                Vector3 playerVelocity = playerGO.GetComponent<CharacterController>().velocity;
                playerGO.GetComponent<CharacterController>().Move(gravity * Time.deltaTime * pullForce);
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
