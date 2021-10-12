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
    public TextMeshProUGUI leftLetter;
    public TextMeshProUGUI middleLetter;
    public TextMeshProUGUI rightLetter;
    public TextMeshProUGUI choiceResult;
    public TextMeshProUGUI correctAttemptsText;
    public TextMeshProUGUI totalAttemptsText;

    public Canvas GameCanvas;
    public Canvas PauseCanvas;

    private string correctPhonem;
    private int totalAttempts;
    private int correctAttempts;
    private int lastRandomLetter;
    private int lowerRandomRange;
    private int upperRandomRange;
    private int upgradeCount;
    private int upgradeStep;

    private Codex[] letterCodex = new Codex[] 
    { new Codex("あ", "a"), new Codex("い", "i"), new Codex("う", "u"), new Codex("え", "e"), new Codex("お", "o"), 
      new Codex("か", "ka"), new Codex("き", "ki"), new Codex("く", "ku"), new Codex("け", "ke"), new Codex("こ", "ko"), 
      new Codex("さ", "sa"), new Codex("し", "shi"), new Codex("す", "su"), new Codex("せ", "se"), new Codex("そ", "so"),
      new Codex("た", "ta"), new Codex("ち", "chi"), new Codex("つ", "tsu"), new Codex("て", "te"), new Codex("と", "to"),
      new Codex("な", "na"), new Codex("に", "ni"), new Codex("ぬ", "nu"), new Codex("ね", "ne"), new Codex("の", "no"),
      new Codex("は", "ha"), new Codex("ひ", "hi"), new Codex("ふ", "fu"), new Codex("へ", "he"), new Codex("ほ", "ho"),
      new Codex("ま", "ma"), new Codex("み", "mi"), new Codex("む", "mu"), new Codex("め", "me"), new Codex("も", "mo"),
      new Codex("ら", "ra"), new Codex("り", "ri"), new Codex("る", "ru"), new Codex("れ", "re"), new Codex("ろ", "ro"),
      new Codex("や", "ya"), new Codex("ゆ", "yu"), new Codex("よ", "yo"),
      new Codex("わ", "wa"), new Codex("ゐ", "wi"), new Codex("ゑ", "we"), new Codex("を", "wo"), new Codex("ん", "n")};

    //0 - 4 vowels
    //5 - 9 K
    //10 - 14 S
    //15 - 19 T
    //20 - 24 N
    //25 - 29 H
    //30 - 34 M
    //35 - 39 R
    //40 - 42 Y
    //43 - 47 W + N

    void Start()
    {
        totalAttempts = 0;
        correctAttempts = 0;
        lastRandomLetter = 0;
        Time.timeScale = 1f;
        lowerRandomRange = 0;
        upperRandomRange = 5;
        upgradeCount = 0;
        upgradeStep = 0;

        totalAttemptsText.text = totalAttempts.ToString();
        correctAttemptsText.text = correctAttempts.ToString();
        ChangeLetter();
    }

    public void ChangeLetter()
    {
        int correctAnswer = GetRandomCorrectAnswer();

        int randomButton = Random.Range(0, 3);
        japaneeseLetter.text = letterCodex[correctAnswer].Letter;
        correctPhonem = letterCodex[correctAnswer].Sound;

        int wrongAnswer1 = Random.Range(lowerRandomRange, upperRandomRange);
        int wrongAnswer2 = Random.Range(lowerRandomRange, upperRandomRange);

        while (wrongAnswer1 == correctAnswer || wrongAnswer1 == wrongAnswer2)
        {
            wrongAnswer1 = Random.Range(lowerRandomRange, upperRandomRange);
        }

        while (wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1)
        {
            wrongAnswer2 = Random.Range(lowerRandomRange, upperRandomRange);
        }


        switch (randomButton)
        {
            case 0:
                leftLetter.text = letterCodex[correctAnswer].Sound;
                middleLetter.text = letterCodex[wrongAnswer1].Sound;
                rightLetter.text = letterCodex[wrongAnswer2].Sound;
                break;

            case 1:
                leftLetter.text = letterCodex[wrongAnswer1].Sound;
                middleLetter.text = letterCodex[correctAnswer].Sound;
                rightLetter.text = letterCodex[wrongAnswer2].Sound;
                break;

            case 2:
                leftLetter.text = letterCodex[wrongAnswer1].Sound;
                middleLetter.text = letterCodex[wrongAnswer2].Sound;
                rightLetter.text = letterCodex[correctAnswer].Sound;
                break;

            default:
                break;
        }
    }

    private int GetRandomCorrectAnswer()
    {
        int correctAnswer = Random.Range(lowerRandomRange, upperRandomRange);

        while (correctAnswer == lastRandomLetter)
        {
            correctAnswer = Random.Range(lowerRandomRange, upperRandomRange);
        }
        lastRandomLetter = correctAnswer;
        return correctAnswer;
    }

    public void CheckChoice()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        string buttonText = go.GetComponentInChildren<TextMeshProUGUI>().text;
        totalAttempts++;

        if (buttonText == correctPhonem)
        {
            choiceResult.text = "Correct!";
            correctAttempts++;
            upgradeCount++;
        }
        else
        {
            choiceResult.text = "Wrong...";
            upgradeCount--;
        }

        totalAttemptsText.text = totalAttempts.ToString();
        correctAttemptsText.text = correctAttempts.ToString();

        IncreaseLetterRange();

        ChangeLetter();
    }

    private void IncreaseLetterRange()
    {
        Debug.Log("step" + upgradeStep);
        if (upgradeCount == 20 && upgradeStep < 8)
        {
            if (upgradeStep != 7)
            {
                upperRandomRange += 5;
                lowerRandomRange += 5;
            }
            else
            {
                upperRandomRange += 3;
                lowerRandomRange += 3;
            }
            upgradeCount = 0;
            upgradeStep++;
        }
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
