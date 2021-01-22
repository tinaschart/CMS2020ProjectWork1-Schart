using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static int currentScore;
   public static int highscore;
   
   public static int currentLevel;
   public static int unlockendLevel;
   public void EnterLevel()
   {
      SceneManager.LoadScene("MainScene");
   }

   public void ExitGame()
   {
#if UNITY_EDITOR //the following code is only included in the unity editor
      UnityEditor.EditorApplication.ExitPlaymode();//exits the playmode
#endif
   }
}
