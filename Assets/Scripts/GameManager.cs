using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public static class GameManagerStatic
{
	public static GameManager gameManager;
}

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        GameManagerStatic.gameManager = this;
    }

    public TextMeshProUGUI currentLetter;


    private string[] wovels = new string[] { "あ", "い", "う", "え", "お" };
    void Start()
    {

    }

    public void changeLetter()
    {
        currentLetter.text = wovels[Random.Range(0, 5)];
    }
}