using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<List<GameObject>> _rowsList;

    [SerializeField] private List<GameObject> _row1;
    [SerializeField] private List<GameObject> _row2;
    [SerializeField] private List<GameObject> _row3;
    [SerializeField] private List<GameObject> _row4;
    [SerializeField] private List<GameObject> _row5;

    [SerializeField]private KeyDictionary _keyDictionary;

    [SerializeField] private GameObject _endScreen;
    

    private bool _inputEnabled = true;
    private int _rowIndex = 0;
    private int _letterIndex = 0;

    private void Start()
    {
        _endScreen.SetActive(false);

        _rowsList = new List<List<GameObject>>(5);
        _rowsList.Clear();
        
        _rowsList.Add(_row1);
        _rowsList.Add(_row2);
        _rowsList.Add(_row3);
        _rowsList.Add(_row4);
        _rowsList.Add(_row5);
    }
    private void Update()
    {
        if (Input.anyKeyDown && _inputEnabled)
        {
            CheckInput(Input.inputString);
        }
    }
    private void CheckInput(string input)
    {
        foreach (char letter in input)
        {
            if (letter == '\b')
            {
                if (_letterIndex != 0)
                    _letterIndex--;
                
                DeleteLetter();
            }
            else if ((letter == '\n') || letter == '\r')
            {
                if (CheckGuess())
                {
                    WinGameOver();
                }
                else if (_rowIndex > 4)
                {
                    LossGameOver();
                }
            }
            else if(_letterIndex <= 4) //length of RowScript.Letters List
            {
                //Sets Letter to input and adds to letterIndex
                _rowsList[_rowIndex][_letterIndex].GetComponentInChildren<TMP_Text>().text = letter.ToString().ToUpper();
                _letterIndex++;
            }
        }
    }
    private void DeleteLetter()
    {
        _rowsList[_rowIndex][_letterIndex].GetComponentInChildren<TMP_Text>().text = null;
    }
    private bool CheckGuess()
    {
        char[] guessedWordArr = new char[5];

        for (int i = 0; i < 5; i++)
        {
            guessedWordArr[i] = _rowsList[_rowIndex][i].GetComponentInChildren<TMP_Text>().text[0];
        }

        string guessedWord = new string(guessedWordArr);
        
        //check word existence
        if (!WordExistence(guessedWord))
        {
            return false;
        }

        //check if word is correct
        
        int correct = string.Compare(guessedWord, SelectingWord.SelectedWord, true);
        
        //if word is not correct:
        if (correct != 0)
        {
            //check letters against selected word
            for (int i = 0; i < guessedWord.Length; i++)
            {
                if (SelectingWord.SelectedWordList.Contains(guessedWordArr[i]) && SelectingWord.SelectedWordList[i] == guessedWordArr[i])
                {
                    SetLetterColor(guessedWord, i , LetterEnum.Correct);
                }
                else if (SelectingWord.SelectedWordList.Contains(guessedWordArr[i]) && SelectingWord.SelectedWordList[i] != guessedWordArr[i])
                {
                    SetLetterColor(guessedWord, i , LetterEnum.WrongPosition);
                }
                else if(!SelectingWord.SelectedWordList.Contains(guessedWordArr[i]))
                {
                    SetLetterColor(guessedWord, i, LetterEnum.Wrong);
                }
            }
            
            _rowIndex++;
            _letterIndex = 0;
            return false;
        }

        //else
        for (int i = 0; i < guessedWord.Length ; i++)
        {
            SetLetterColor(guessedWord, i, LetterEnum.Correct);
        }
        return true;
    }

    private void SetLetterColor(string key, int i, LetterEnum color)
    {
        _rowsList[_rowIndex][i].GetComponentInChildren<LetterState>().SetLetterColor(color);
        _keyDictionary.KeyboardDict[key:key[i]].GetComponentInChildren<LetterState>().SetLetterColor(color);
    }
    private bool WordExistence(string input)
    {
        return SelectingWord.Instance.Words.Contains(input.ToLower());
    }
    private void WinGameOver()
    {
        _inputEnabled = false;
        
        _endScreen.SetActive(true);

        _endScreen.GetComponentInChildren<TMP_Text>().text = "You Win!";
    }
    private void LossGameOver()
    {
        _inputEnabled = false;
        
        _endScreen.SetActive(true);

        _endScreen.GetComponentInChildren<TMP_Text>().text = "You Lose \n The Correct Word Was: \n \n" + "\"" + SelectingWord.SelectedWord.ToUpper() + "\"";
    }
    public void OnButtonClick()
    {
        CheckGuess();
    }
    public void OnRestartClick()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
