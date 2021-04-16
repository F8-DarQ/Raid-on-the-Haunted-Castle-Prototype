using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour {

    public List<Transform> m_currentPath;
    public Transform m_currentWaypoint;
    public List<Transform> m_flightPaths;
    public int m_currentWaypointIndex;
    public int m_currentPathIndex;
    public float m_speed;

    // Start is called before the first frame update
    void Start() {
        m_currentPathIndex = 0;
        m_currentWaypointIndex = 0;
        ExtractWaypoints();
        m_currentWaypoint = m_currentPath[m_currentWaypointIndex];
        transform.position = m_currentWaypoint.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        // movement
        Vector3 p = Vector3.MoveTowards(transform.position, m_currentWaypoint.position, m_speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(p);
        
        // selects next waypoint after reaching curent waypoint
        if (Vector3.Distance(m_currentWaypoint.position, transform.position) < 0.0001f) {
            m_currentWaypointIndex++;
            if (m_currentWaypointIndex > m_currentPath.Count - 1) {
                m_currentWaypointIndex = 0;
                m_currentPathIndex++;
                if (m_currentPathIndex > m_flightPaths.Count - 1) {
                    m_currentPathIndex--;
                }
            }
            m_currentWaypoint = m_currentPath[m_currentWaypointIndex];
        }
    }

    void ExtractWaypoints() {
        m_currentPath.Clear();
        foreach (Transform child in m_flightPaths[m_currentPathIndex]) {
            m_currentPath.Add(child);
        }
    }
}
