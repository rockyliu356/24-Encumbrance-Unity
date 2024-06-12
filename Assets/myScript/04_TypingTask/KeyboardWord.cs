using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyboardWord : MonoBehaviour
{
    public InputField inputField;

    private void Update()
    {
        Logger.enteredSentence = inputField.text;
    }

    public void OnKeyClicked(string key)
    {
        inputField.text += key;
    }

    public void DeleteClicked()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
}