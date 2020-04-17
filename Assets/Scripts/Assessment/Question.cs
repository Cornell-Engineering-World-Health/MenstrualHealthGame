using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]

//Need a way to store images within answer choices
public class Question
{
    public AudioClip clip;
    public string question;
    public Sprite[] answerChoices;
    public int correctAnswerIndex;
}
