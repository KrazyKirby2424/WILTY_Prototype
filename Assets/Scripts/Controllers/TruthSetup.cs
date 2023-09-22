using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class TruthSetup : MonoBehaviour
{
    public static TruthSetup instance;

    [SerializeField]
    TMP_Text playerText;
    [SerializeField]
    TMP_Text inputText;
    List<Player> players;
    Player current;
    int index;

    [SerializeField]
    Transform context;
    private void Awake()
    {
        index= 0;
        if (instance == null)
        {
            instance = this;
        }
        players = GameInfo.instance.GetPlayers();
        current = players[index];
        playerText.text = current.GetName(); //first player
        inputText.text = "Enter truth for " + current.GetName();
    }

    public Player GetCurrent()
    {
        return current;
    }

    public void NextPlayer()
    {
        index++;
        if(index < players.Count)
        {
            current = players[index];
            playerText.text = current.GetName();
            inputText.text = "Enter truth for " + current.GetName();
            foreach (Transform child in context.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
