using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("collider Oxygen");
        if (Other.gameObject.CompareTag("Oxygen"))
        {
            Debug.Log("Get Oxygen");
            Destroy(this.gameObject);
            Other.GetComponent<PhotonView>().RPC("InCreaseO2", PhotonTargets.All, 30.0f);

        }
    }
}
