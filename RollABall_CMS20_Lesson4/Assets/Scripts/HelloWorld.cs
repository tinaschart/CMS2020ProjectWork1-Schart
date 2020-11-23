using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [SerializeField] private int    m_threshold   = 10;
    [SerializeField] private string m_message = "Hello World";
    
    private void Start()
    {
        LogHelloWorld(m_message);
    }
    
    private void LogHelloWorld(string message)
    {
        if (m_threshold > 10)
            Debug.Log(message);
    }
}
