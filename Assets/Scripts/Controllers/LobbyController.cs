using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    TMP_Text input;

    [SerializeField]
    TMP_InputField clear;

    [SerializeField]
    Transform scrollView;

    [SerializeField]
    GameObject prefabUI;

    public static LobbyController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Add()
    {
        GameObject playerUI = Instantiate(prefabUI, scrollView);
        playerUI.GetComponentInChildren<TMP_Text>().text = input.text;
        playerUI.GetComponentInChildren<PlayerUI>().SetObj();

        //clear text after it is set
        clear.text = "";
        CheckSetup();
    }

    [SerializeField]
    Button gameSetup;
    public void CheckSetup()
    {
        if(GameInfo.instance.GetPlayers().Count>1) //2 or more
        {
            gameSetup.gameObject.SetActive(true);
        }
        else
        {
            gameSetup.gameObject.SetActive(false); 
        }
    }

    [SerializeField]
    TMP_Text roundsText;
    private int nextIndex = 1; //default value is 5 and was set in GameInfo
    private int[] rounds = new int[] { 5, 9, 15 }; //5, 9, 15
    public void Rounds()
    {
        if(nextIndex < rounds.Count())
        {
            GameInfo.instance.SetRounds(rounds[nextIndex]);
            roundsText.text = "Rounds: " + rounds[nextIndex];
            nextIndex++;
        }
        else
        {
            nextIndex = 0;
            GameInfo.instance.SetRounds(rounds[nextIndex]);
            roundsText.text = "Rounds: " + rounds[nextIndex];
            nextIndex++;
        }
    }
}
