using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCarrier : MonoBehaviour
{
    public static NameCarrier instance;
    public NameChecker nameChecker;

    private static string lifetimePlayerName = "Player";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public string GetPlayerName()
    {
        return lifetimePlayerName;
    }

    public void SetPlayerName(string name)
    {
        lifetimePlayerName = name;
    }
}
