using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo instance;

    private int rounds = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private List<Player> players = new List<Player>();

    public void AddPlayer(Player p)
    {
        players.Add(p);
    }
    public void RemovePlayer(Player p)
    {
        players.Remove(p);
    }
    public List<Player> GetPlayers()
    {
        return players;
    }

    public void SetRounds(int r)
    {
        rounds = r;
    }
    public int GetRounds()
    {
        return rounds;
    }

    public void Clear()
    {
        Destroy(gameObject);
    }
}

//Class to represent a player object
//GameInfo will be a list of player objects