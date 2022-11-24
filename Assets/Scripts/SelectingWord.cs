using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectingWord: MonoBehaviour
{
    [SerializeField] private TextAsset _textFile;
    public static WordData Instance;

    private static string _selectedWord;
    public static string SelectedWord => _selectedWord;

    public static List<char> SelectedWordList;


    private void Awake()
    {
        Instance = WordData.FromFile(_textFile);

        _selectedWord = SelectWord(Instance.Words);
        
        Debug.Log(_selectedWord);

        SelectedWordList = new List<char>();
        for (int i = 0; i < _selectedWord.Length; i++)
        {
            SelectedWordList.Add(SelectedWord.ToUpper()[i]);
        }
    }
    private string SelectWord(List<string> words)
    {
        int random = Random.Range(0, Instance.Words.Count);
        return words[random]; 
    }
}
