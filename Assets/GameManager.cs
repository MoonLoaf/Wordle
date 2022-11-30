using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private KeyDictionary _keyDictionary;
    [SerializeField] private List<GameObject> _rows;

    private bool _inputEnabled = true;
    private int _rowIndex = 0;
    private int _letterIndex = 0;
    
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
                DeleteLetter();
                if (_letterIndex != 0)
                    _letterIndex--;
            }
            else if ((letter == '\n') || letter == '\r')
            {
                if (CheckGuess() == true)
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
        _rows[_rowIndex].GetComponent<RowScript>().Letters[_letterIndex - 1].GetComponentInChildren<TMP_Text>().text = null;
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
            Debug.Log("Word is incorrect");
            
            //check letters against selected word
            for (int i = 0; i < guessedWord.Length; i++)
            {
                if (SelectingWord.SelectedWordList.Contains(guessedWordArr[i]) && SelectingWord.SelectedWordList[i] == guessedWordArr[i])
                {
                    _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Correct);
                    _keyDictionary.KeyboardDict[key:guessedWord[i]].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Correct);
                }
                else if (SelectingWord.SelectedWordList.Contains(guessedWordArr[i]) && SelectingWord.SelectedWordList[i] != guessedWordArr[i])
                {
                    _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.WrongPosition);
                    _keyDictionary.KeyboardDict[key:guessedWord[i]].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.WrongPosition);
                }
                else if(!SelectingWord.SelectedWordList.Contains(guessedWordArr[i]))
                {
                    _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Wrong);
                    _keyDictionary.KeyboardDict[key:guessedWord[i]].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Wrong);
                }
            }
            
            _rowIndex++;
            _letterIndex = 0;
            return false;
        }

        //else
        for (int i = 0; i < guessedWord.Length ; i++)
        {
            _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Correct);
            _keyDictionary.KeyboardDict[key:guessedWord[i]].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Correct);
        }
        return true;
    }
    private bool WordExistence(string input)
    {
        return SelectingWord.Instance.Words.Contains(input.ToLower());
    }

    private void WinGameOver()
    {
        _inputEnabled = false;
        Debug.Log("You Win");
    }

    private void LossGameOver()
    {
        _inputEnabled = false;
        Debug.Log("You lose");
    }

    public void OnButtonClick()
    {
        CheckGuess();
    }

}
