using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderManager : MonoBehaviour
{
    [SerializeField] private ListMission _listMission;

    [SerializeField] private Text sumStarTxt;

    private void Start()
    {
        UpdateSumStar();
    }

    public void UpdateSumStar()
    {
        sumStarTxt.text = _listMission.sumStar.ToString();
    }
}
