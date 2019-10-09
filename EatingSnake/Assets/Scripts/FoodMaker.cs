using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour {

    public int xlimt = 19;  //pc是21
    public int ylimt = 11;
    public int offset = 13;
    public GameObject foodPrefab;
    public Sprite[] foodSprites;     //用于存储食物皮肤
    private Transform foodHolder;    //归类生成的食物
    public GameObject rewardPrefab;

    //单例模式
    private static FoodMaker _instance;
    public static FoodMaker Instance {
        get {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeFood(bool isReward) {
        int index = Random.Range(0,foodSprites.Length);       //随机生成的皮肤序号
        GameObject food = Instantiate(foodPrefab);            // 实例化食物
        food.GetComponent<Image>().sprite = foodSprites[index];   //将食物皮肤赋值给当前生成的食物
        food.transform.SetParent(foodHolder,false);               //将生成的食物赋给空物体，方便管理
        int x = Random.Range(-xlimt+offset,xlimt);                //随机生成坐标
        int y = Random.Range(-ylimt, ylimt);
        food.transform.localPosition = new Vector3(x * 30, y * 30, 0);   //给当前生成的物体赋值随机坐标
        if (isReward) {
            GameObject reward = Instantiate(rewardPrefab);            // 实例化食物
            reward.transform.SetParent(foodHolder, false);               //将生成的食物赋给空物体，方便管理
            x = Random.Range(-xlimt + offset, xlimt);                //随机生成坐标
            y = Random.Range(-ylimt, ylimt);
            reward.transform.localPosition = new Vector3(x * 30, y * 30, 0);   //给当前生成的物体赋值随机坐标
        }
    }

}
