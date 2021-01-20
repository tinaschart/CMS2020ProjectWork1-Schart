using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyController : EnemyController
{
  [SerializeField] private Transform[] m_waypoints = null; //waypoints this enemy will traverse

  private int m_waypointIndex = 0; //current waypoint index

  protected override Vector3 GetNextDestination()
  {
    Vector3 destination = m_waypoints[m_waypointIndex].position; //get waypoint at current index

    m_waypointIndex = (m_waypointIndex + 1) % m_waypoints.Length; //increment index (and possibly resets it via modulo operator)

    return destination;
  }
  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("schuss"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
