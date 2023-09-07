using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    public NameChecker nameChecker;

    public void StartGame()
    {
        if(nameChecker != null)
        {
            if(NameCarrier.instance != null)
                NameCarrier.instance.SetPlayerName(nameChecker.GetPlayerNameTextFromInputField());
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
