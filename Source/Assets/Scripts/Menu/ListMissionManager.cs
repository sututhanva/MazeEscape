using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class ListMissionManager : MonoBehaviour
{
    [SerializeField] private ListMission _listMission;

    private int minRender = 0;
    private int maxRender = 80;

    private GameObject missionButton;
    // Start is called before the first frame update

    public void SaveListMission()
    {
        _listMission.SaveDataToJson();
    }
    
    public void LoadListMission()
    {
        _listMission.LoadDataFromJson();
        ClearMissionBtn(minRender, maxRender);
        RerenderMissionBtn(minRender, maxRender);
    }

    public void GenerateStateMissions()
    {
        _listMission.RandomDataMissions();
        ClearMissionBtn(minRender, maxRender);
        RerenderMissionBtn(minRender, maxRender);
    }

    public void ResetStateMission()
    {
        _listMission.ResetAllMissions();
        ClearMissionBtn(minRender, maxRender);
        RerenderMissionBtn(minRender, maxRender);
    }
    

    public void RerenderMissionBtn(int index, int max)
    {
        if (index < minRender)
        {
            minRender = index;
        }

        if (max > maxRender)
        {
            maxRender = max;
        }
        if(index > _listMission.size) return;
        int maxCurrent = max < _listMission.size ? max : _listMission.size;
        List<Mission> list = _listMission._list;
        int row = index/4;
        for (int i = index; i < maxCurrent; i++)
        {
            missionButton = ObjectPools.Instance.GetPooledObject();
            ObjectPools.Instance.AddPooledUsage(missionButton);

            missionButton.SetActive(true);

            if (list[i].id > 0)
            {
                missionButton.GetComponentInChildren<ButtonMissionManager>().SetLevel(list[i].id);
                missionButton.GetComponentInChildren<ButtonMissionManager>().SetMission(list[i].rate,list[i].isUnlocked);
            }
            else
            {
                missionButton.GetComponentInChildren<ButtonMissionManager>().SetMission(list[i].rate,list[i].isUnlocked);
                missionButton.GetComponentInChildren<ButtonMissionManager>().EnableTutorial();
            }

            if (i % 4 == 0 && i != index)
            {
                row += 1;
            }
            
            if (row % 2 == 0)
            {
                missionButton.transform.SetSiblingIndex(i);
                Debug.Log(i);
            }
            else
            {
                int siblingIndex = (row+1)*4-1  - i + (row)*4;
                missionButton.transform.SetSiblingIndex(siblingIndex);
                Debug.Log(siblingIndex);
            }
        }
    }

    public void ClearMissionBtn(int index, int max)
    {
        for (int i = index; i < max; i++)
        {
            ObjectPools.Instance.GetPooledUsage()[i].SetActive(false);
        }
    }
    
}
