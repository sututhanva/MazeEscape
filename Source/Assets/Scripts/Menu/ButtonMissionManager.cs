using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMissionManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private GameObject textGO;
    [SerializeField] private GameObject[] _starList;
    [SerializeField] private GameObject missionLock;

    private Text text;
    private int level;

    private void Awake()
    {
        text = textGO.GetComponent<Text>();
    }

    public void SetMission(int starRate, bool isUnlocked)
    {
        if (isUnlocked)
        {
            SetRate(starRate);
            missionLock.SetActive(false);
        }
        else
        {
            SetRate(0);
            missionLock.SetActive(true);
        }
    }
    public void SetRate(int amount)
    {
        for (int i = 0; i < 3; i++)
        {
            _starList[i].SetActive(false);
        }
        for (int i = 0; i < amount; i++)
        {
            _starList[i].SetActive(true);
        }
    }

    public void SetLevel(int missionLevel)
    {
        level = missionLevel;
        text.text = level.ToString();
    }

    public void EnableTutorial()
    {
        tutorialImage.SetActive(true);
        textGO.SetActive(false);
    }

    public void ToMission()
    {
        PlayerPrefs.SetInt("LevelMission", level);
        SceneManager.LoadScene("InGame");
    }
}
