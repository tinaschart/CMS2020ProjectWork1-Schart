using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f; //speed modifier
    
    private Rigidbody m_playerRigidbody = null; //reference to the players rigidbody

    private float m_movementX, m_movementY; //input vector components

    private int m_collectablesTotalCount, m_collectablesCounter; //everything we need to count the given collectables

    private Stopwatch m_stopwatch; //takes the time

    public Text scoreText;
    
    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody component

        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length; //find all gameobjects in the scene which are tagged with "Collectable" and count them via Length property 

        scoreText.text = "Score: " + m_collectablesTotalCount.ToString() + " / " + m_collectablesTotalCount.ToString();
        
        m_stopwatch = Stopwatch.StartNew(); //start the stopwatch
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>(); //get the input

        //split input vector in its two components
        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY); //translate the 2d vector into a 3d vector
        
        m_playerRigidbody.AddForce(movement * m_speed); //apply a force to the rigidbody
    }

    private void OnTriggerEnter(Collider other)//executed when the player hits another collider (which is set to 'is trigger')
    {
        if (other.gameObject.CompareTag("Collectable"))//has the other gameobject the tag "Collectable"
        {
            other.gameObject.SetActive(false); //set the hit collectable inactive

            m_collectablesCounter--; //count down the remaining collectables
            scoreText.text = m_collectablesCounter.ToString();
            if (m_collectablesCounter == 0) //have we found all collectables? if so we won!
            {
                UnityEngine.Debug.Log("Congratulations. You successfully outrun the Enemies!");
                UnityEngine.Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables.");
#if UNITY_EDITOR //the following code is only included in the unity editor
                UnityEditor.EditorApplication.ExitPlaymode();//exits the playmode
#endif
            }

            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy")) //has the other gameobject the tag "Enemy" / game over state
        {
            UnityEngine.Debug.Log("Again!");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }
    }
}
