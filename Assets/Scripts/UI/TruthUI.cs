using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TruthUI : MonoBehaviour
{
    [SerializeField]
    private GameObject truth;

    Player player;

    public void SetPlayer(Player p)
    {
        player = p;
        player.AddTruth(this.GetComponentInChildren<TMP_Text>().text);
    }

    public void Remove()
    {
        player.RemoveTruth(this.GetComponentInChildren<TMP_Text>().text);
        Destroy(truth);
    }
}
