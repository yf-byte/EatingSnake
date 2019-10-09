using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {
    public int score = 0;
    public int length = 0;
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public List<int> scoreList;

    public Image bgImage;
    public Color tempColor;

    public bool isPause = false;
    public Image pauseImage;
    public Sprite[] pauseSprites;

    public bool hasBorder = true;



    //单例模式
    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    //实时更新ui界面
    public void UpdateUI(int i = 5, int l = 1) {
        score += i;
        length += l;
        scoreList.Add(score);
        scoreText.text = "得分\n" + score;
        lengthText.text = "长度\n" + length;
    }
    

    //实现游戏阶段增加
    void Update()
    {
        switch (score/100) {
            case 0:
            case 1:
            case 2:
                break;
            case 3:
            case 4:
                ColorUtility.TryParseHtmlString("#CCEEFFFF",out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 2;
                break;
            case 5:
            case 6:
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 3;
                break;
            case 7:
            case 8:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 4;
                break;
            case 9:
            case 10:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段" + 5;
                break;
            default:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "无尽阶段";
                break;
        }
    }


    //实现游戏暂停
    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;      //将时间设置为0，当前游戏即可暂停
            pauseImage.sprite = pauseSprites[1];
        }
        else {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprites[0];
        }
    }

    //实现返回开始界面
    public void Home() {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("border", 1) == 0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform) {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
}
