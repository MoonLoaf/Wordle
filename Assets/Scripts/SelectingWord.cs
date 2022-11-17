using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectingWord: MonoBehaviour
{
    [SerializeField] private TextAsset _textFile;
    public WordData Instance;
    public List<string> wordList;


    private string[] _wordArray;
    private string _selectedWord;
    public static char[] SelectedWordArray;

    private void Awake()
    {
        Instance = WordData.FromFile(_textFile);

        Debug.Log(SelectedWord(wordList));
    }
    private string SelectedWord(List<string> words)
    {
        int random = Random.Range(0, _wordArray.Length);
        return words[random]; 
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
