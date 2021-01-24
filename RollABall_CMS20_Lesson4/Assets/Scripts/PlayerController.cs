using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private                  GameObject[] players;
    [SerializeField] private float        m_speed = 1f; //speed modifier
    public                   Camera       TDcam;
    private                  Rigidbody    m_playerRigidbody = null; //reference to the players rigidbody
    public                   bool         onGround;
    public                   RawImage     targetImage;
    private                  float        m_movementX, m_movementY; //input vector components
    public                   Slider       slider;
    public                   static           float        lifes;
    public                   float        maxLifes;
    private                  int          m_collectablesTotalCount, m_collectablesCounter; //everything we need to count the given collectables

    private Stopwatch  m_stopwatch; //takes the time
    public  GameObject schuss;      //schuss Object
    public  Text       scoreText;
    public  GameObject gameOverText;
    private AudioSource Audio;

    private float moveHorizontal;
    private float moveVertical;

    private void Start()
    {
          Audio = GetComponent<AudioSource>();
       Audio.enabled = true;
        slider.maxValue   = maxLifes;
        lifes             = maxLifes;
        onGround          = true;
        m_playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody component

        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length; //find all gameobjects in the scene which are tagged with "Collectable" and count them via Length property 
        TDcam.targetTexture      = (RenderTexture) targetImage.texture;
        scoreText.text           = "collectables: " + Data.Coins.ToString() + " / 15" ;

        m_stopwatch = Stopwatch.StartNew(); //start the stopwatch
    }

    // Update is called once per frame
    void Update()
    {

        slider.value = Data.Health;


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
            obj                                    = Instantiate(schuss, transform.position, transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = transform.forward * 9;
        }
          if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)) /*&& iceBool == true*/)
        {
            Audio.enabled = true;
        }

        else
        {
            Audio.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical   = Input.GetAxis("Vertical");
        }

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        m_playerRigidbody.AddForce(movement * m_speed);

        transform.LookAt(new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z + moveVertical));

        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            // if (Keyboard.current.spaceKey.isPressed)
            {
                m_playerRigidbody.velocity = new Vector3(m_playerRigidbody.velocity.x, 5f, m_playerRigidbody.velocity.z);
 onGround                   = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject obj;
            obj                                    = Instantiate(schuss, transform.position, transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = transform.forward * 20;
            Debug.Log("schuss");
        }

        transform.LookAt(new Vector3(transform.position.x + m_movementX, transform.position.y, transform.position.z + m_movementY));
    }
void Awake()
{
    //Load the saved score (this value will be saved even if you restart the app)
    lifes = PlayerPrefs.GetInt("Lifes");
}
    private void OnTriggerEnter(Collider other) //executed when the player hits another collider (which is set to 'is trigger')
    {
        if (other.gameObject.CompareTag("Collectable")) //has the other gameobject the tag "Collectable"
        {
            other.gameObject.SetActive(false); //set the hit collectable inactive

            Data.Coins++; //count down the remaining collectables
            scoreText.text = "collectables: " + Data.Coins.ToString() + " /  15";
            /*if (m_collectablesCounter == 0) //have we found all collectables? if so we won!
            {
                UnityEngine.Debug.Log("Congratulations. You successfully outrun the Enemies!");
                gameOverText.SetActive(true);
                StartCoroutine(waitALittleBit());

                UnityEngine.Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables.");
            }

            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
            }*/
        }
        else if (other.gameObject.CompareTag("Enemy")) //has the other gameobject the tag "Enemy" / game over state
        {
            lifes -= 1;
            Data.Health = lifes;
             Debug.Log("Leben" + Data.Health);
            if (lifes == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Data.Health = 3;
            }

            
            // UnityEngine.Debug.Log("Game Over!");
            //gameOverText.SetActive(true);

            /*#if UNITY_EDITOR
                        UnityEditor.EditorApplication.ExitPlaymode();
            #endif*/
        }
        else if (other.gameObject.CompareTag("life"))
            {
                if(Data.Health <=2)
                {
                    
                Data.Health += 1;
               
                other.gameObject.SetActive(false);
                Debug.Log("LIFES: " + lifes);
                }
                else{
                  other.gameObject.SetActive(false);  
                }
            }
           
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("boden"))
        {
            onGround = true;
        }
         else if (other.gameObject.CompareTag("frozen"))
            {
                
               m_speed = m_speed * 1.3f;
               Debug.Log("Speed: " + m_speed);
               
            }
    }

    private IEnumerator waitALittleBit()
    {
        yield return new WaitForSeconds(5);
#if UNITY_EDITOR                                      //the following code is only included in the unity editor
        UnityEditor.EditorApplication.ExitPlaymode(); //exits the playmode
#endif
    }

    public void OnMoveVector2(Vector2 touch)
    {
        moveHorizontal = touch.x;
        moveVertical   = touch.y;
    }
}

