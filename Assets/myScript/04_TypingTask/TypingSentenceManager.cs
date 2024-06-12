using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingSentenceManager : MonoBehaviour
{
    public TextMeshProUGUI targetSentence;
    public TextMeshProUGUI sentenceID;
    public GameObject nextDialog;
    public GameObject keyboard;
    public GameObject canvas;
    // public float startTime;
    // public float endTime;
    // public float typingTime;
    public GameObject startButton;
    public int index = 1;
    public List<string> sentences = new List<string>() {
        "my watch fell in the water",
        "breathing is difficult",
        "my bank account is overdrawn",
        "we are having spaghetti",
        "time to go shopping"
    };


    private void Start()
    {
        ShuffleSentences();
    }

    private void ShuffleSentences()
    {
        for (int i = 0; i < sentences.Count; i++)
        {
            string temp = sentences[i];
            int randomIndex = Random.Range(i, sentences.Count);
            sentences[i] = sentences[randomIndex];
            sentences[randomIndex] = temp;
        }
    }

    public void OnEnterSelected()
    {
        if (index > sentences.Count)
        {
            // endTime = Time.time;
            // typingTime = endTime - startTime;
            nextDialog.SetActive(true);
            keyboard.SetActive(false);
            canvas.SetActive(false);
            return;
        }

        // if (index == 1)
        // {
        //     startTime = Time.time;
        // }
        // else
        // {
        //     endTime = Time.time;
        //     typingTime = endTime - startTime;
        //     startTime = Time.time;
        // }

        sentenceID.text = index.ToString();
        targetSentence.text = sentences[index - 1];
        Logger.targetSentence = sentences[index - 1];
        index += 1;
    }

    public void OnStartSelected()
    {
        sentenceID.text = index.ToString();
        targetSentence.text = sentences[index - 1];
        Logger.targetSentence = sentences[index - 1];
        index += 1;
        startButton.SetActive(false);
    }
}
