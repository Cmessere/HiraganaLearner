using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public TextMeshProUGUI phoneticLetter1;
    public TextMeshProUGUI phoneticLetter2;
    public TextMeshProUGUI phoneticLetter3;
    public TextMeshProUGUI choiceResult;
    public Canvas GameCanvas;
    public Canvas PauseCanvas;

    private string correctPhonem;

    private Codex[] codexVowels = new Codex[] { new Codex("あ", "a"), new Codex("い", "i"), new Codex("う", "u"), new Codex("え", "e"), new Codex("お", "o")};
    void Start()
    {
        Time.timeScale = 1f;
        changeLetter();
    }

    public void changeLetter()
    {
        int correctAnswer = Random.Range(0, 5);
        int wrongAnswer1 = Random.Range(0, 5);
        int wrongAnswer2 = Random.Range(0, 5);

        int randomButton = Random.Range(0, 3);
        japaneeseLetter.text = codexVowels[correctAnswer].Letter;
        correctPhonem = codexVowels[correctAnswer].Sound;

        while (wrongAnswer1 == correctAnswer || wrongAnswer1 == wrongAnswer2)
        {
            wrongAnswer1 = Random.Range(0, 5);
        }
        
        while(wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1)
        {
            wrongAnswer2 = Random.Range(0, 5);
        }

        Debug.Log(correctAnswer + " " + wrongAnswer1 + " " + wrongAnswer2);
        switch (randomButton)
        {
            case 0:
                phoneticLetter1.text = codexVowels[correctAnswer].Sound;
                phoneticLetter2.text = codexVowels[wrongAnswer1].Sound;
                phoneticLetter3.text = codexVowels[wrongAnswer2].Sound;
                break;

            case 1:
                phoneticLetter1.text = codexVowels[wrongAnswer1].Sound;
                phoneticLetter2.text = codexVowels[correctAnswer].Sound;
                phoneticLetter3.text = codexVowels[wrongAnswer2].Sound;
                break;

            case 2:
                phoneticLetter1.text = codexVowels[wrongAnswer1].Sound;
                phoneticLetter2.text = codexVowels[wrongAnswer2].Sound;
                phoneticLetter3.text = codexVowels[correctAnswer].Sound;
                break;

            default:
                break;
        }
    }

    public void checkChoice()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        string buttonText = go.GetComponentInChildren<TextMeshProUGUI>().text;

        if(buttonText == correctPhonem)
        {
            Debug.Log("Correct");
            choiceResult.text = "Correct!";
        }
        else
        {
            Debug.Log("False");
            choiceResult.text = "Wrong...";
        }

        changeLetter();
    }


    public void PauseGame()
    {
        PauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        PauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}



public class Codex
{
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
