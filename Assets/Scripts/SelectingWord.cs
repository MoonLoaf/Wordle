using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectingWord: MonoBehaviour
{
    [SerializeField] private TextAsset _textFile;
    private WordData _instance;

    private static string _selectedWord;
    public static string SelectedWord => _selectedWord;


    private void Awake()
    {
        _instance = WordData.FromFile(_textFile);

        _selectedWord = SelectWord(_instance.Words);
        
        Debug.Log(_selectedWord);
    }
    private string SelectWord(List<string> words)
    {
        int random = Random.Range(0, _instance.Words.Count);
        return words[random]; 
    }
}
