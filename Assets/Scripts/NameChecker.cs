using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameChecker : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_Text warningText;

    private void Start()
    {
        playerNameInputField.characterLimit = 16;
    }

    private void CustomChangeEvent()
    {
        if (playerNameInputField != null)
        {
            if (playerNameInputField.text != string.Empty)
            {
                char[] listAsChar = playerNameInputField.text.ToCharArray();

                if (listAsChar.Length > playerNameInputField.characterLimit)
                {
                    warningText.enabled = true;
                }
                else
                {
                    warningText.enabled = false;
                }
            }
        }
    }

    public string GetPlayerName()
    {
        return playerNameInputField.text;
    }
}
