using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    private GameObject player;
    //Represents a reference to the player object instance
    public Player obj;
    /* Can't set through awake because awake was called once instantiated and the text was not set from LobbyController before instantiate the player obj
    private void Awake()
    {
        Debug.Log(this + " name is: " + this.GetComponentInChildren<TMP_Text>().text);
        GameObject player = Instantiate(prefab, GameInfo.instance.transform);
        obj = player.GetComponent<Player>();
        obj.SetName(this.GetComponentInChildren<TMP_Text>().text);
        Debug.Log(obj + " name is: " + obj.GetName());
    }
    */
    public void SetObj()
    {
        GameObject player = Instantiate(prefab, GameInfo.instance.transform);
        obj = player.GetComponent<Player>();
        obj.SetName(this.GetComponentInChildren<TMP_Text>().text);
    }

    public void Remove()
    {
        GameInfo.instance.RemovePlayer(obj);
        Destroy(obj.gameObject);
        Destroy(player);
        LobbyController.instance.CheckSetup();
    }
}
