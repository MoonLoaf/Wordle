using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<List<GameObject>> _rowList;

    [SerializeField] private List<GameObject> _row1Letters;
    [SerializeField] private List<GameObject> _row2Letters;
    [SerializeField] private List<GameObject> _row3Letters;
    [SerializeField] private List<GameObject> _row4Letters;
    [SerializeField] private List<GameObject> _row5Letters;

    private bool _inputEnabled = true;
    private int _rowIndex = 0;
    private int _letterIndex = 0;

    private TMP_Text _currentLetterText;

    private void Start()
    {
        _rowList = new List<List<GameObject>>(5);
        _rowList.Clear();
        
        _rowList.Add(_row1Letters);
        _rowList.Add(_row2Letters);
        _rowList.Add(_row3Letters);
        _rowList.Add(_row4Letters);
        _rowList.Add(_row5Letters);
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
                _rowList[_rowIndex][_letterIndex].GetComponentInChildren<TMP_Text>().text = letter.ToString().ToUpper();
                _letterIndex++;
            }
        }
    }
    private void DeleteLetter()
    {
        _rowList[_rowIndex][_letterIndex - 1].GetComponentInChildren<TMP_Text>().text = null;
    }
    private bool CheckGuess()
    {
        char[] guessedWordArr = new char[5];

        for (int i = 0; i < 5; i++)
        {
            guessedWordArr[i] = _rowList[_rowIndex][i].GetComponentInChildren<TMP_Text>().text[0];
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
                    _rowList[_rowIndex][i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Correct);
                }
                else if (SelectingWord.SelectedWordList.Contains(guessedWordArr[i]) && SelectingWord.SelectedWordList[i] != guessedWordArr[i])
                {
                    _rowList[_rowIndex][i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.WrongPosition);
                }
                else if(!SelectingWord.SelectedWordList.Contains(guessedWordArr[i]))
                {
                    _rowList[_rowIndex][i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Wrong);
                }
            }
            
            _rowIndex++;
            _letterIndex = 0;
            return false;
        }

        //else
        for (int i = 0; i < guessedWord.Length ; i++)
        {
            _rowList[_rowIndex][i].GetComponentInChildren<LetterState>().SetLetterColor(LetterEnum.Correct);
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
