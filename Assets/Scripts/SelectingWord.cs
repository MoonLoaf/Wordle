using UnityEngine;
using Random = UnityEngine.Random;

public class SelectingWord: MonoBehaviour
{
    [SerializeField] private TextAsset _textFile;
    private string[] _wordArray;
    private string _selectedWord;
    public static char[] SelectedWordArray;

    private void Awake()
    {
        _wordArray = (_textFile.text.Split('\n'));

        int random = Random.Range(0, _wordArray.Length);
        _selectedWord = _wordArray[random];

        SelectedWordArray = StringToArray(_selectedWord);
        Debug.Log(_selectedWord);
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
