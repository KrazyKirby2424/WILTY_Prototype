using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AddTruth : MonoBehaviour
{
    [SerializeField]
    TMP_Text input;

    [SerializeField]
    TMP_InputField clear;

    [SerializeField]
    Transform scrollView;

    [SerializeField]
    GameObject prefab;

    public void Add()
    {
        GameObject truth = Instantiate(prefab, scrollView);
        //set player name to input text
        truth.GetComponentInChildren<TMP_Text>().text = input.text;
        truth.GetComponent<TruthUI>().SetPlayer(TruthSetup.instance.GetCurrent());
        //clear text after it is set
        clear.text = "";

    }
}
