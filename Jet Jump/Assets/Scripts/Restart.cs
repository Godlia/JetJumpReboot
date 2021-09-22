using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
 public class Restart : MonoBehaviour{


    void Update() {
        RestartGame();
    }

    void RestartGame() {
        if(Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }

 }