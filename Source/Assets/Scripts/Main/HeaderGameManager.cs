using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderGameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ButtonFindingObj;
    
    
    [SerializeField]
    private GameObject ButtonAutoMoveObj;

    private Button ButtonFinding;
    private Button ButtonAutoMove;

    private void Awake()
    {
        ButtonFinding = ButtonFindingObj.GetComponent<Button>();
        ButtonAutoMove = ButtonAutoMoveObj.GetComponent<Button>();
    }

    public void TurnButton()
    {
        ButtonAutoMove.interactable = !ButtonAutoMove.interactable;
    }
}
