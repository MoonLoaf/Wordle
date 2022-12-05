using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private KeyDictionary _keyDictionary;
    [SerializeField] private List<GameObject> _rows;

    [SerializeField] private GameObject _endScreen;
    

    private bool _inputEnabled = true;
    private int _rowIndex = 0;
    private int _letterIndex = 0;

    private void Start()
    {
        _endScreen.SetActive(false);
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
                _rows[_rowIndex].GetComponent<RowScript>().Letters[_letterIndex].GetComponentInChildren<TMP_Text>().text = letter.ToString().ToUpper();
                _letterIndex++;
            }
        }
    }
    private void DeleteLetter()
    {
        _rows[_rowIndex].GetComponent<RowScript>().Letters[_letterIndex].GetComponentInChildren<TMP_Text>().text = null;
    }
    private bool CheckGuess()
    {
        char[] guessedWordArr = new char[5];

        for (int i = 0; i < 5; i++)
        {
            guessedWordArr[i] = _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<TMP_Text>().text[0];
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
        _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<LetterState>().SetLetterColor(color);
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

        _endScreen.GetComponentInChildren<TMP_Text>().text = "You Lose \n The Correct Word Was: \n \n" + "'" + SelectingWord.SelectedWord.ToUpper() + "'";
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
