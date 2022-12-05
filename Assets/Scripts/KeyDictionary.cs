using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyDictionary : MonoBehaviour
{
    public Dictionary<char, GameObject> KeyboardDict;

    private void Start()
    {
        KeyboardDict = new Dictionary<char, GameObject>();

        AddDescendants(transform, KeyboardDict);
    }
    private void AddDescendants(Transform parent, Dictionary<char, GameObject> dict)
    {
        foreach (Transform child in parent)
        {
            dict.Add(child.GetComponentInChildren<TMP_Text>().text[0], child.gameObject);
        }
    }
}
