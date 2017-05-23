using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayingTrap : MonoBehaviour {

    // if using gun = true 
    // otherwise using traps
    public bool isHoldingGun = true;
    // for Traps
    public string traptype = "Trap-A";
    private HashSet<GameObject> trapsLaid;

    // for O2 Gun
    public CanvasGroup crossHair;    
    public float O2GunRange = 30f;
    public float hitForce = 25f;
    private float gunCooldown;

    private void Start() {
        trapsLaid = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        gunCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Submit")) {
            isHoldingGun = ! isHoldingGun;
        }
        if (isHoldingGun) {
            crossHair.alpha = 1f;
            if (Input.GetButtonDown("Fire1")) {
                Fire();
            }
        } else {
            crossHair.alpha = 0f;
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
    }

    void LayTrap() {
        var facing = gameObject.transform.forward;
        var trapPosition = gameObject.transform.position + facing * 2;
        var trapGO = PhotonNetwork.Instantiate(traptype, trapPosition, Quaternion.identity, 0);
        trapsLaid.Add(trapGO);
    }

    void Fire() {
        if (gunCooldown > 0) {
            return;
        }
        gunCooldown = 1.0f;
        gameObject.GetComponent<PhotonView>().RPC("ReduceO2", PhotonTargets.All, 20.0f);
        Vector3 rayOrigin = gameObject.transform.position;
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, O2GunRange);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != this.transform)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("was shoot");
                    var playerGO = hit.collider.gameObject;
                    float dist, span;
                    dist = hitForce;
                    span = 0.5f;
                    //playerGO.GetComponent<CharacterController>().Move(-hit.normal * hitForce);
                    playerGO.GetComponent<PhotonView>().RPC("MoveByForce", PhotonTargets.All, gameObject.transform.forward, dist, span);
                }
            }
        }

    }

    void RecoilFromFiring() {
        float distance = hitForce / 2;
        float timespan = 0.25f;
        Vector3 dir = gameObject.transform.forward;
        gameObject.GetComponent<PhotonView>().RPC("MoveByForce", PhotonTargets.All, -dir, distance, timespan);
        //var pushbackForce = -gameObject.transform.forward * hitForce / 2;
        //gameObject.GetComponent<CharacterController>().Move(pushbackForce * Time.deltaTime * 10);
    }

}