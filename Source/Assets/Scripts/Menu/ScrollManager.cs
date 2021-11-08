using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _scrollRect;
    [SerializeField]
    private ListMissionManager _listMissionManager;
    private int threshold = 1;

    private void Start()
    {
        _listMissionManager.RerenderMissionBtn(0, 80);
    }

    private void Update()
    {
        if (_scrollRect.verticalNormalizedPosition > 0.8)
        {
            _listMissionManager.RerenderMissionBtn(threshold*80, (threshold+1)*80);
            threshold++;
        }
    }
}
