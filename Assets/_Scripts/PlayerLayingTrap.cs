using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayingTrap : MonoBehaviour {

    public string traptype = "Trap-A";
    private HashSet<GameObject> trapsLaid;

    private void Start() {
        trapsLaid = new HashSet<GameObject>();        
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            LayTrap();
        }
        if (Input.GetButtonDown("Fire2") && traptype == "Remote Trap") {
            List<GameObject> removeList = new List<GameObject>();
            foreach (var trapGO in trapsLaid) {
                if ((trapGO.transform.position - gameObject.transform.position).magnitude <= 30f) {
                    trapGO.GetComponent<PhotonView>().RPC("DoEffect", PhotonTargets.All, gameObject.transform.position);
                    removeList.Add(trapGO);
                }
            }
            foreach (var trapGO in removeList) {
                trapsLaid.Remove(trapGO);
            }
        }
    }

    void LayTrap() {
        var facing = gameObject.transform.forward;
        var trapPosition = gameObject.transform.position + facing * 2;
        var trapGO = PhotonNetwork.Instantiate(traptype, trapPosition, Quaternion.identity, 0);
        trapsLaid.Add(trapGO);
    }
}
