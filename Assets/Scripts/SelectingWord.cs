using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SelectingWord: MonoBehaviour
{
    [SerializeField] private TextAsset _textFile;
    public WordData Instance;

    private static string _selectedWord;
    public static string SelectedWord => _selectedWord;


    private void Awake()
    {
        Instance = WordData.FromFile(_textFile);

        _selectedWord = SelectWord(Instance.Words);
        
        Debug.Log(_selectedWord);
    }
    private string SelectWord(List<string> words)
    {
        int random = Random.Range(0, Instance.Words.Count);
        return words[random]; 
    }
}
