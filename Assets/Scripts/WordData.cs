using System.Collections.Generic;
using UnityEngine;

public class WordData
{
    public List<string> Words = new List<string>();
    public static WordData FromFile(TextAsset file)
    {
        if (file == null)
            return null;

        return JsonUtility.FromJson<WordData>(file.text);
    }
}
