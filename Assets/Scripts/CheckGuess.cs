using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckGuess : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerGuess;
    [SerializeField] private Button _enter;


    private bool CheckPlayerGuess(char[] input)
    {
        char[] selectedWord = StringToArray(SelectingWord.SelectedWord);
        
        if(input.Length != selectedWord.Length){ return false;}
        for(int i=0;i<input.Length - 1;i++)
        {
            if(input[i] != selectedWord[i]){ return false;}
        }
        return true;
    }
    private char[] StringToArray(string input)
    {
        char[] array = new char[input.Length];
        
        for (int i = 0; i < input.Length; i++)
        {
            array[i] = input[i];
        }

        return array;
    }
    
    
    
    
    
}
