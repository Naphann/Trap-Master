using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDActivateTrap : MonoBehaviour {

    public float activateRange = 8;
    public float delay = 2f;
    public float duration = 1f;
    private float currentRange;
    private bool isActive;
    private bool isDetnotate;


    // Use this for initialization
    void Start() {
        currentRange = 0;
        isActive = false;
        isDetnotate = false;
    }

    // Update is called once per frame
    void Update() {
        delay -= Time.deltaTime;
        if (currentRange < activateRange) {
            currentRange += 1.0f * Time.deltaTime * 5;
            gameObject.GetComponent<BoxCollider>().size = new Vector3(1, currentRange, currentRange);
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, currentRange / 2);
        }
        if (!isActive && delay <= 0) {
            isActive = true;
        }
        if (isDetnotate) {
            duration -= Time.deltaTime;
            if (duration <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        // send to MasterClient to remove this trap from the map
        // Debug.Log("Trap die");
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (!isDetnotate && other.gameObject.CompareTag("Player")) {
            isDetnotate = true;
            DoEffect();
        }
    }

    private void OnTriggerExit(Collider other) {
        //if ()
    }



    private void DoEffect() {
        //Debug.Log("Do effect");
        var activationScript = GetComponentInChildren<ITrapActivation>();
        activationScript.Activate();
    }
}
