using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordDataBase", menuName = "WordDataBase")]
public class WordDataBase : ScriptableObject
{

    [SerializeField] private List<WordData> WordDataLists = new List<WordData>();

    public List<WordData> GetWordDataLists()
    {
        return WordDataLists;
    }
}