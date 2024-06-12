using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyboardWord : MonoBehaviour
{
    public InputField inputField;
    public string wordEntered;

    public void OnKeyClicked(string key)
    {
        inputField.text += key;
        wordEntered = key;
    }

    public void DeleteClicked()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
}