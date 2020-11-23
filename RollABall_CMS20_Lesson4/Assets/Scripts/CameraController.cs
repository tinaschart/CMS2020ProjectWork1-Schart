using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null; //reference to our player object

    private Vector3 m_offset = Vector3.zero; //member to store the initial offset of camera and player

    private void Start()
    {
        m_offset = gameObject.transform.position - m_playerObject.transform.position; //store the positional offset of our player object and the camera (on which this script is attached to)
    }

    private void Update()
    {
        gameObject.transform.position = m_playerObject.transform.position + m_offset; //update the cameras position every frame - with the corresponding offset
    }
}
