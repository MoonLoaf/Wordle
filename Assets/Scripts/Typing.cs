using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    [SerializeField]private TMP_Text _textInput;
    private string _selectedWord;

    private void Start()
    {
        _textInput.maxVisibleCharacters = 5;
    }
    private void Update()
    {
    }
}
