using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string m_leveLname = null;
    public float _lifes; 
    public PlayerController script;
      public                   Slider       slider;

    void Start()
    {
        script = GameObject.FindObjectOfType<PlayerController>();
    }
     void Update()
     {
        //_lifes = script.lifes;  //  Update our score continuously.
     }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            
            SceneManager.LoadScene(m_leveLname);
            Debug.Log("Leben" + Data.Health);
            //slider.value = _lifes;

        }
           
    }
}
