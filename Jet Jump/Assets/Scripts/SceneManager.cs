using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
 public class SceneManager : MonoBehaviour{


    void Update() {
        Restart();
    }

    void Restart() {
        if(Input.GetKeyDown(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }

 }