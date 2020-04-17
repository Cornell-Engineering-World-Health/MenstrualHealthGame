using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCManager : MonoBehaviour
{
    public Question question;

    // The question image. TODO: Accept a video option.
    [SerializeField]
    private Sprite questionSprite;

    // The answer button to duplicate
    // TODO: figure out how to flex size 
    [SerializeField]
    public GameObject prefabButton;

    // The questionImage object that holds questionSprite
    [SerializeField]
    private GameObject questionImage;

    //Linked to AnswerPanel to organize buttons in sub-container as opposed to Canvas
    [SerializeField]
    public RectTransform ParentPanel;

    //The index of the answer selected by user
    private int answerSelected;

    // Initializes state of question & answer choices
    void Start()
    {
        // Sets the questionText field
        questionImage.GetComponent<Image>().sprite = questionSprite;
        // No answer selected yet
        answerSelected = -1;

        //Loops through the answerChoices array to extract the answers, create buttons accordingly, and inject the answers into the button text
        for (int i = 0; i < question.answerChoices.Length; i++)
        {
            //Instantiates a button out of the prefab. Set parent to the Answer Panel.
            //TODO: research localScale
            GameObject goButton = (GameObject)Instantiate(prefabButton);
            goButton.transform.SetParent(ParentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);

            //TODO: why do we need this
            Button tempButton = goButton.GetComponent<Button>();
            int tempInt = i;

            //Sets the text of the button to the answers & adds a listener to check for the user-selected answer.
            tempButton.GetComponentInChildren<Image>().sprite = question.answerChoices[tempInt];
            tempButton.onClick.AddListener(() => UserSelect(tempInt));
        }


    }

    //User select method
    void UserSelect(int buttonIndex)
    {
        // Stop previous audio, if any
        if (answerSelected >= 0) FindObjectOfType<AudioManager>().Stop(answerSelected);

        // Play button audio 
        FindObjectOfType<AudioManager>().Play(buttonIndex);

        // Record index of button (answer) selected
        answerSelected = buttonIndex;

        // For debugging purposes
        Debug.Log("correct answer = " + question.correctAnswerIndex);
        Debug.Log("answer selected = " + answerSelected);
    }

    public void nextButton()
    {
        //need to keep counter of tries
        //compare answerSelected to correct answer index (first iteration)
        //true: confirmation recording, video/image
        //false: feedback recording – try again

        //compare answers (second iteration)
        //true: confirmation recording, video/img, (automatically shifts to next scene?)
        //false: feedback recording – explanations, (automatically shifts to next scene?)
    }

    /*TODO:
    -add replay button function (need to research how to change scenes)
    -add previous button function
    -instead of changing text, shift all text to images. deal with animations later
    -add audio feature:
    -audio manager holds all audio sources. at beginning, play the question & answers.
    -answer choice audio plays when user clicks
    -replay button replays all audios
    -only one try
     */

}
