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

    public TextMeshProUGUI japaneeseLetter;
    public TextMeshProUGUI phoneticLetter;


    //private string[] japaneeseWovels = new string[] { "あ", "い", "う", "え", "お" };
    //private string[] phoneticWovels = new string[] { "a", "i", "u", "e", "o" };

    //private Codex a = new Codex("あ", "a");
    //private Codex i = new Codex("い", "i");
    //private Codex u = new Codex("う", "u");
    //private Codex e = new Codex("え", "e");
    //private Codex o = new Codex("お", "o");

    private Codex[] codexVowels = new Codex[] { new Codex("あ", "a"), new Codex("い", "i"), new Codex("う", "u"), new Codex("え", "e"), new Codex("お", "o")};
    void Start()
    {
        foreach(Codex el in codexVowels) { 
            Debug.Log("Risultato:" + el.ToString());
        }
    }

    public void changeLetter()
    {
        int randomNumber = Random.Range(0, 5);
        japaneeseLetter.text = codexVowels[randomNumber].Letter;
        phoneticLetter.text = codexVowels[randomNumber].Sound;
    }
}

public class Codex
{
    // Constructor that takes no arguments:
    public Codex()
    {
        Letter = "";
        Sound = "";
    }

    public Codex(string letter, string sound)
    {
        Letter = letter;
        Sound = sound;
    }
    public string Letter { get; }
    public string Sound { get; }

    public override string ToString()
    {
        return Letter +" "+ Sound;
    }
}