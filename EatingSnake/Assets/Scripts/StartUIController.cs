using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Collections;

public class StartUIController : MonoBehaviour {
    public Text lastText;
    public Text bestText;

    public Toggle blue;
    public Toggle yellow;
    public Toggle border;
    public Toggle noBorder;

    public Image serarchInformation;

    public bool isActive = false;


    void Start()
    {
        lastText.text = "上次：长度" + PlayerPrefs.GetInt("lastl", 0) + "，分数" + PlayerPrefs.GetInt("lasts",0);
        bestText.text = "最好：长度" + PlayerPrefs.GetInt("bestl", 0) + "，分数" + PlayerPrefs.GetInt("bests", 0);

        if (PlayerPrefs.GetString("sh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
        else {
            yellow.isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }

        if (PlayerPrefs.GetInt("border", 1) == 1)
        {
            border.isOn = true;
            PlayerPrefs.SetInt("border", 1);
        }
        else {
            noBorder.isOn = true;
            PlayerPrefs.SetInt("Border", 0);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void BlueSelected(bool isOn) {
        if (isOn) {
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02","sb0102");
        }
    }

    public void YellowSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }
    }

    public void BorderSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border",1);
        }
    }

    public void NoBorderSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 0);
        }
    }

    //查看历史分数
    public void SearchInformation() {
        isActive = !isActive;
        serarchInformation.gameObject.SetActive(isActive);
    }

    //退出游戏
    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("退出游戏");
    }
}
