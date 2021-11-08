using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "ListMission", menuName = ("Assets/ListMission"))]
public class ListMission : ScriptableObject
{
    [SerializeField]
    public int size;
    [SerializeField]
    public List<Mission> _list;

    public int sumStar = 0;
    
    [NonSerialized]
    public string dataPath;

    private Random _random = new Random();
    
    [SerializeField] private GameEvent changeListMissionEvent;

    private void OnEnable()
    {
        dataPath = Application.dataPath + "/DataBase/Mission/listMission.json";
        if (File.Exists(dataPath))
        {
            LoadDataFromJson();
        }
        else
        {
            CreatMissions();
        }
    }

    public void SaveDataToJson()
    {
        string json = JsonConvert.SerializeObject(_list, Formatting.Indented);
        File.WriteAllText(dataPath, json);
    }

    public void LoadDataFromJson()
    {
        if (File.Exists(dataPath))
        {
            var json = File.ReadAllText(dataPath);
            _list = JsonConvert.DeserializeObject<List<Mission>>(json);
            CountingStar();
            changeListMissionEvent.Raise();
        }
    }

    private void CountingStar()
    {
        sumStar = 0;
        foreach (var mission in _list)
        {
            sumStar += mission.rate;
        }
    }

    public void RandomDataMissions()
    {
        int missionUnlock = _random.Next(0, size);
        int i = 0;
        sumStar = 0;
        foreach (var mission in _list)
        {
            int star = 0;
            bool isUnlocked = false;
            if (i < missionUnlock)
            {
                star = _random.Next(1, 3);
                isUnlocked = true;
            }
            else if (i == missionUnlock - 1)
            {
                star = 0;
                isUnlocked = true;
            }
            else
            {
                star = 0;
                isUnlocked = false;
            }
            mission.Init(i,star, isUnlocked);
            sumStar += star;
            i++;
        }
        changeListMissionEvent.Raise();
    }
    public void GenerateRandomMissions()
    {
        for (int i = 1; i <= size; i++)
        {
            Mission mission = ScriptableObject.CreateInstance<Mission>();
            _list.Add(mission);
        }
    }

    public void CreatMissions()
    {
        _list = new List<Mission>();
        GenerateRandomMissions();
        RandomDataMissions();
    }

    public void ResetAllMissions()
    {
        foreach (var mission in _list)
        {
            mission.rate = 0;
        }

        sumStar = 0;
        changeListMissionEvent.Raise();
    }
    
}