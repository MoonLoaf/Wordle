using System.Collections.Generic;
using UnityEngine;

public class WordData
{
    public static WordData FromFile(TextAsset file)
    {
        if (file == null)
            return null;

        return JsonUtility.FromJson<WordData>(file.text);
    }
}
