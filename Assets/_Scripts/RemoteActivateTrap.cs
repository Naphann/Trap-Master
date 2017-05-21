using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteActivateTrap : MonoBehaviour {

    public float activationRange = 30;
    public float delay = 2f;
    public float duration = 1f;
    private float currentRadius = 0.0f;
    private bool isActive = false;
    private bool isDetnotate = false;


    // Use this for initialization
    void Start() {
        currentRadius = 0;
        isDetnotate = false;
    }

    // Update is called once per frame
    void Update() {
        delay -= Time.deltaTime;
        if (currentRadius < activationRange) {
            currentRadius += 1.0f * Time.deltaTime * 5;
            gameObject.GetComponent<SphereCollider>().radius = currentRadius;
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
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void DoEffect(Vector3 activatorPosition) {
        var activationScript = GetComponentInChildren<ITrapActivation>();
        activationScript.Activate();
        isDetnotate = true;
    }
}
