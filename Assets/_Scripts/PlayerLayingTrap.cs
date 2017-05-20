using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayingTrap : MonoBehaviour {

    private string traptype = "Trap-A";
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            //Fire();
            LayTrap();
        }
    }

    void Fire() {
        Debug.Log("FIRE!!!!");
    }

    void LayTrap() {
        var facing = gameObject.transform.forward;
        var trapPosition = gameObject.transform.position + facing * 2;        
        PhotonNetwork.Instantiate(traptype, trapPosition, Quaternion.identity, 0);
    }
}
