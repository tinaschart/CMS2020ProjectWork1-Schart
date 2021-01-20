using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f; //speed modifier
    public Camera TDcam;
    private Rigidbody m_playerRigidbody = null; //reference to the players rigidbody
    public bool onGround;
    public RawImage targetImage;
    private float m_movementX, m_movementY; //input vector components
    public Slider slider;
      private float lifes;
    public float maxLifes;
    private int m_collectablesTotalCount, m_collectablesCounter; //everything we need to count the given collectables

    private Stopwatch m_stopwatch; //takes the time
    public GameObject schuss; //schuss Object
    public Text scoreText;
    public GameObject gameOverText;
    
    private void Start()
    {
        slider.maxValue = maxLifes;
        lifes = maxLifes;
        onGround = true;
        m_playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody component
 
        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length; //find all gameobjects in the scene which are tagged with "Collectable" and count them via Length property 
        TDcam.targetTexture = (RenderTexture)targetImage.texture;
        scoreText.text = "collectables: " + m_collectablesTotalCount.ToString() + " / " + m_collectablesTotalCount.ToString();
        
        m_stopwatch = Stopwatch.StartNew(); //start the stopwatch
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>(); //get the input

        //split input vector in its two components
        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }
    
   // Update is called once per frame
    void Update()
    {slider.value = lifes;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        m_playerRigidbody.AddForce(movement * m_speed);
       
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_speed = m_speed * 2f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_speed = m_speed / 2f;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("bla");
            GameObject obj;
            obj = Instantiate(schuss, transform.position, transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = transform.forward * 9;
        }
        transform.LookAt(new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z + moveVertical));
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY); //translate the 2d vector into a 3d vector
       
        m_playerRigidbody.AddForce(movement * m_speed); //apply a force to the rigidbody
    if (onGround)
        {

            if (Input.GetButtonDown("Jump"))
            //if (Keyboard.current.spaceKey.isPressed)
            {
                 m_playerRigidbody.velocity = new Vector3( m_playerRigidbody.velocity.x, 5f,  m_playerRigidbody.velocity.z);
                onGround = false;
            }
        }
         if (Keyboard.current.aKey.isPressed)
        {
            GameObject obj;
            obj = Instantiate(schuss, transform.position, transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = transform.forward * 20;
            Debug.Log("schuss");
        }
        transform.LookAt(new Vector3(transform.position.x + m_movementX, transform.position.y, transform.position.z + m_movementY));
    }

    private void OnTriggerEnter(Collider other)//executed when the player hits another collider (which is set to 'is trigger')
    {
        if (other.gameObject.CompareTag("Collectable"))//has the other gameobject the tag "Collectable"
        {
            other.gameObject.SetActive(false); //set the hit collectable inactive

            m_collectablesCounter--; //count down the remaining collectables
            scoreText.text = "collectables: " + m_collectablesCounter.ToString() + " / " + m_collectablesTotalCount.ToString();
            if (m_collectablesCounter == 0) //have we found all collectables? if so we won!
            {
                UnityEngine.Debug.Log("Congratulations. You successfully outrun the Enemies!");
                gameOverText.SetActive(true);
                StartCoroutine(waitALittleBit());
                
                UnityEngine.Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables.");

            }

            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy")) //has the other gameobject the tag "Enemy" / game over state
        {lifes -= 1;
        if (lifes == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
           // UnityEngine.Debug.Log("Game Over!");
            //gameOverText.SetActive(true);
            
/*#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif*/
        }
    }
public void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("boden"))
        {
            onGround = true;
        }
    }
    private IEnumerator waitALittleBit()
    {
        yield return new WaitForSeconds(5);
#if UNITY_EDITOR //the following code is only included in the unity editor
        UnityEditor.EditorApplication.ExitPlaymode();//exits the playmode
#endif
    }
    
    
}

