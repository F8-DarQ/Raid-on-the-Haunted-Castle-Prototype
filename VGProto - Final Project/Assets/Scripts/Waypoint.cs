using UnityEngine;

public class Waypoint : MonoBehaviour
{
  private void OnDrawGizmos() {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
    }
}
