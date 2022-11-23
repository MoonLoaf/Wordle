using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rows;

    private int _rowIndex = 0;
    private int _letterIndex = 0;

    private TMP_Text _currentLetterText;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        if (Input.anyKeyDown)
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
                if(_letterIndex != 0)
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
        char[] guessedWord = new char[5];

        for (int i = 0; i < 5; i++)
        {
            guessedWord[i] = _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<TMP_Text>().text[0];
        }

        string guessedWordString = new string(guessedWord);
        
        //check word existence
        if (!WordExistence(guessedWordString))
        {
            return false;
        }

        //check if word is correct
        
        int correct = string.Compare(guessedWordString, SelectingWord.SelectedWord, true);
        
        //if word is not correct:
        if (correct != 0)
        {
            Debug.Log("Word is incorrect");

            _rowIndex++;
            _letterIndex = 0;
            return false;
        }
        
        //else
        return true;
    }
    private bool WordExistence(string input)
    {
        return SelectingWord.Instance.Words.Contains(input.ToLower());
    }

    private void WinGameOver()
    {
        Debug.Log("You Win");
    }

    private void LossGameOver()
    {
        Debug.Log("You lose");
    }

}
