using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null; //reference to our player object
    [SerializeField] private float      m_detectionRadius       = 4f; //observed radius of the enemy

    [SerializeField] private Material m_idleMaterial    = null; //material which is attached while in 'idle mode'
    [SerializeField] private Material m_chasingMaterial = null; //material which is attached while in 'chasing mode'

    private NavMeshAgent m_agent = null; //the reference to the NavMeshAgent component
    private Vector3      m_initialPosition = Vector3.zero; //member to store the initial position of this enemy

    protected virtual Vector3 GetNextDestination()
    {
        return m_initialPosition; //standard enemy should just return to its initial position when in 'idle mode'
    }
    
    private void Start()
    {
        m_agent           = gameObject.GetComponent<NavMeshAgent>(); //get the NavMeshAgent component
        m_initialPosition = gameObject.transform.position; //get the initial position
    }

    private void Update()
    {
        if (Vector3.Distance(m_playerObject.transform.position, gameObject.transform.position) < m_detectionRadius) //distance from player to enemy is smaller than the given detection radius - therefore the enemy is in 'chasing mode' 
        {
            m_agent.GetComponent<Renderer>().material = m_chasingMaterial; //set the corresponding material
            m_agent.SetDestination(m_playerObject.transform.position); //chase the player - set its position as destination point
            return;
        }

        //enemy is in 'idle mode'
        m_agent.GetComponent<Renderer>().material = m_idleMaterial; //set the corresponding material
        if(m_agent.remainingDistance < 0.5f) //if the agent is close to its set goal it targets a new one
            m_agent.SetDestination(GetNextDestination());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("schuss"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
