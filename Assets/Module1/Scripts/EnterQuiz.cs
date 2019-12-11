using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterQuiz : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("collide");
        if(other.CompareTag("Player"))
        {
            Debug.Log("collide");
            SceneManager.LoadScene("Quiz");
        }
    }
}
