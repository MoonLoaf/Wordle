using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WordData _instance;
    
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
                CheckGuess();
                if (_rowIndex <= _rows.Count)
                {
                    _rowIndex++;
                    _letterIndex = 0;
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

    private void CheckGuess()
    {
        char[] guessedWord = new char[5];
        
        for (int i = 0; i < 5; i++)
        {
            guessedWord[i] = _rows[_rowIndex].GetComponent<RowScript>().Letters[i].GetComponentInChildren<TMP_Text>().text[0];
        }
        
        Debug.Log(guessedWord.ToString());

        // if (WordExistence(guessedWord.ToString()))
        // {
        //     int correct = string.Compare(guessedWord.ToString(), SelectingWord.SelectedWord, true);
        //     
        //     Debug.Log(correct);
        //
        //
        // }
    }
    private bool WordExistence(string input)
    {
        return _instance.Words.Contains(input);
    }

}
