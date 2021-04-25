using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDataManager : MonoBehaviour
{
    private WordDataBase WordDataBase;

    private Dictionary<WordData, int> numOfWordData = new Dictionary<WordData, int>();

    void Start()
    {
        for (int i = 0; i < WordDataBase.GetWordDataLists().Count; i++)
        {
            numOfWordData.Add(WordDataBase.GetWordDataLists()[i], i);
            Debug.Log(WordDataBase.GetWordDataLists()[i].GetShowName() + ": " + WordDataBase.GetWordDataLists()[i].GetHideName());
        }
    }
}
