using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordData", menuName = "CreateWordData")]
public class WordData : ScriptableObject
{
    [SerializeField] private string ShowName;

    [SerializeField] private string HideName;

    public string GetShowName()
    {
        return ShowName;
    }

    public string GetHideName()
    {
        return HideName;
    }
}