using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {
    private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

    // Update is called once per frame
    void Update() {
        if (!photonView.isMine) {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 20);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 20);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else {
            correctPlayerPos = (Vector3) stream.ReceiveNext();
            correctPlayerRot = (Quaternion) stream.ReceiveNext();
        }
    }
}