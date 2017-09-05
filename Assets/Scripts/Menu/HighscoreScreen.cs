using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class HighscoreScreen : MonoBehaviour
{

    private string highscoreFileName = "Highscores_save.txt";

    private Dictionary<string, float> nameList = new Dictionary<string, float>();
    KeyValuePair<string, float> lastkey;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (!File.Exists(highscoreFileName))
        {
            CreateANewFile();
        }
        else
        {
            ParseFileToDictio();
        }
        List<float> sortingByTime = new List<float>();


        foreach (KeyValuePair<string, float> pair in nameList)
        {
            sortingByTime.Add(pair.Value);
        }
        sortingByTime.Sort();
        int idx = 0;

        StartCoroutine(PrintOnScreen(idx, sortingByTime));
    }

    void CreateANewFile()
    {
        File.Create(highscoreFileName);
    }
    void ParseFileToDictio()
    {
        string[] linesInFile = File.ReadAllLines(highscoreFileName);

        foreach (string line in linesInFile)
        {
            string[] parsedLine = line.Split('|');
            nameList.Add(parsedLine[0], (float)Convert.ToDouble(parsedLine[1]));
        }
    }

    void PrintPos(int i)
    {
        GameObject timeText = GameObject.Find("Pos (" + (GetNum(i) + 1).ToString() + ")");
        if (timeText && timeText.GetComponent<Text>())
        {
            timeText.GetComponent<Text>().text = (i+1).ToString();
        }
    }
    void PrintName(int i, List<float> sortingByTime)
    {
        GameObject nameText = GameObject.Find("Name (" + (GetNum(i) + 1).ToString() + ")");

        if (nameText && nameText.GetComponent<Text>())
        {
            foreach (KeyValuePair<string, float> pair in nameList)
            {
                if (pair.Value == sortingByTime[i] && pair.Key != lastkey.Key)
                {
                    nameText.GetComponent<Text>().text = pair.Key;
                    lastkey = pair;

                    return;
                }
            }
        }
    }
    void PrintTime(int i, List<float> sortingByTime)
    {
        GameObject timeText = GameObject.Find("Time (" + (GetNum(i) + 1).ToString() + ")");
        if (timeText && timeText.GetComponent<Text>())
        {
            timeText.GetComponent<Text>().text = ParseTimeToString(sortingByTime[i]);
        }
    }

    string ParseTimeToString(float time)
    {
        int min = (int)time / 6000;
        int sec;
        if (min > 0)
            sec = (int)((time - min * 6000) / 100);
        else
            sec = (int)time / 100;
        int dsec = (int)(time - (sec * 100) - (min * 6000));

        string result = ParseToTime(min) + ":" + ParseToTime(sec) + ":" + ParseToTime(dsec);

        return result;

    }

    string ParseToTime(int value)
    {
        if (value < 10)
        {
            if (value <= 0)
                return "00";
            else
                return "0" + value.ToString();
        }
        else
            return value.ToString();
    }
    int GetNum(int i)
    {
        if (i >= 10)
            return i % 10;
        else
            return  i;
    }
    IEnumerator PrintOnScreen(int idx, List<float> sortingByTime)
    {
        while (true)
        {
            for (int i = idx; i < (idx + 10); i++)
            {
                PrintPos(i);
                PrintName(i, sortingByTime);
                PrintTime(i, sortingByTime);
            }
            idx += 10;
            yield return new WaitForSeconds(5f);

            if (idx >= 40)
                idx = 0;
        }
    }
}
