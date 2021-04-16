using UnityEngine;

public class GhostKill : MonoBehaviour {
    void OnTriggerEnter (Collider otherObject) {
        if (otherObject.gameObject.CompareTag ("Player")) {
            Vector3 newPosition = new Vector3 (-8, 0, -3);
            otherObject.gameObject.transform.position = newPosition;
            otherObject.gameObject.GetComponent<PlayerMovement>().setDest(newPosition);
            otherObject.gameObject.GetComponent<PlayerMovement>().setDir(Vector3.zero);
        }
    }
}