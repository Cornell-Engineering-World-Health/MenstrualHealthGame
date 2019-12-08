using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizOptionHandler : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject correctPanel;
    public GameObject incorrectPanel;
    int correctCounter;

    public void Correct() {
        Debug.Log("correct");
        correctCounter += 1;
        if (correctCounter == 2) {
            correctPanel.SetActive(true);
            questionPanel.SetActive(false);
        }
    }

    public void Incorrect()
    {
        Debug.Log("incorrect");
        incorrectPanel.SetActive(true);
        questionPanel.SetActive(false);
    }
}
