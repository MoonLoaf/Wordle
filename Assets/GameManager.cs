using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rows;

    private int _rowIndex;
    private int _letterIndex;

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
            }
            else if ((letter == '\n') || letter == '\r')
            {
                CheckGuess();
            }
            else
            {
                //letter = _rows[_rowIndex].GetComponent<RowScript>().Letters[_letterIndex].GetComponent<TMP_Text>().ToString().ToUpper();
            }
        }
        
        
    }
    private void DeleteLetter()
    {
        //remove input text from current letter
    }

    private void CheckGuess()
    {
        //Check player guess against selected word
    }

}
