using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReset : MonoBehaviour
{
    [SerializeField] private float threshold = -7;

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(-10, 1, -9);
        }
    }
}
