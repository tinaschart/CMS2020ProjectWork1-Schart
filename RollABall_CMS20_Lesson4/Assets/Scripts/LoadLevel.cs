using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string m_leveLname = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
           
            SceneManager.LoadScene(m_leveLname);
           
    }
}
