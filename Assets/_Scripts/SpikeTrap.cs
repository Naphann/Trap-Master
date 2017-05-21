using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

    public float pullRadius = 2;
    public float pullForce = 1;

    public void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;
            if (collider.name != "Ground")
            {
                // apply force on target towards me
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }
        }
    }
}

