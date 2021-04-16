using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 10.0f;
    
    private Vector3 m_dest = Vector3.zero;
    private Vector3 m_dir = Vector3.zero;
    private Vector3 m_nextDir = Vector3.zero;
    private float m_distance = 1f; // look ahead 1 unit

    public void setDest(Vector3 newDest){ m_dest = newDest; }
    public void setDir(Vector3 newDir){ m_dir = newDir; m_nextDir = newDir;}

    // Start is called before the first frame update
    void Start()
    {
        m_dest = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move closer to destination
        Vector3 p = Vector3.MoveTowards(transform.position, m_dest, m_speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(p);

        // up moves up, down is down, right is right, etc .. (uses world space)
        if (Input.GetAxis("Horizontal") > 0) { 
            m_nextDir = Vector3.right; 
        }
        
        if (Input.GetAxis("Horizontal") < 0) { 
            m_nextDir = -Vector3.right; 
        }
        
        if (Input.GetAxis("Vertical") > 0) { 
            m_nextDir = Vector3.forward; 
        }
        
        if (Input.GetAxis("Vertical") < 0) { 
            m_nextDir = -Vector3.forward; 
        }

         if (Vector3.Distance(m_dest, transform.position) < 0.0001f) {
            if (Valid(m_nextDir)) { 
                m_dest = (Vector3)transform.position + m_nextDir;
                m_dir = m_nextDir;    
            } else {   // nextDir NOT valid
                if (Valid(m_dir)) {  // and the prev. direction is valid
                    // continue on that direction
                    m_dest = (Vector3)transform.position + m_dir;   
                }                 
            }
        }

        transform.LookAt(m_dest);
    }

    private bool Valid(Vector3 direction) {
        Vector3 pos = transform.position;
        bool retVal = false; 

        direction += new Vector3(direction.x * m_distance, 0, direction.z * m_distance);
        RaycastHit hit; 
        Physics.Linecast(pos + direction, pos, out hit);
        
        if(hit.collider != null) {
            retVal = hit.collider.tag == "Ghost" || (hit.collider == GetComponent<Collider>());
        } 
        return retVal;
    }
}
