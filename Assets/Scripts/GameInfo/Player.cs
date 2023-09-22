using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    //Represents a player object
    public string player_Name;
    public List<string> truths = new List<string>();

    public void SetName(string name)
    {
        player_Name = name;
    }
    private void Awake()
    {
        GameInfo.instance.AddPlayer(this);
    }
    public void AddTruth(string truth)
    {
        truths.Add(truth);
    }
    public void RemoveTruth(string truth)
    {
        truths.Remove(truth);
    }
    public List<string> GetTruths()
    {
        return truths;
    }
    public string GetName()
    {
        return player_Name;
    }
}
