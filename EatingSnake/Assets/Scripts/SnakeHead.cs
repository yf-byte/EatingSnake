using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {

    public int step;      //蛇头移动一次的距离
    private int x;         
    private int y;
    private Vector3 headPosition;      //记录蛇头的位置
    public float velocity = 0.35f;     //蛇头移动的速度
    public float addVelocity = 0.025f;

    public List<Transform> bodyList = new List<Transform>();     //用来存储蛇身数据
    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];                 //用来存储蛇身皮肤数据
    private Transform canvas;

    private bool isDie = false;
    private bool isDiao = true;
    private bool isDiao1 = true;
    private bool isDiao2 = true;
    private bool isDiao3 = true;
    private bool isDiao4 = true;
    public GameObject dieEffect;

    public AudioClip eatClip;
    public AudioClip dieClip;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").transform;            //找到canvas组件
        InvokeRepeating("Move",0,velocity);    //重复调用某一个函数，三个参数分别是函数名，开始调用时的时间，多久调用一次（调用的间隔时间）
        x = 0;
        y = step;

        //通过resource.load(string path)加载资源
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh02"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0201"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0202"));
    }



    //处理蛇头的移动，以及不能后转向的bug
    //处理蛇头加速移动的功能
    // Update is called once per frame
    void Update () {

        //实现蛇头移动控制
        if (Input.GetKey(KeyCode.W) && y != -step && MainUIController.Instance.isPause == false && isDie == false) {
            x = 0;
            y = step;
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
        }
        if (Input.GetKey(KeyCode.S) && y != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            x = 0;
            y = -step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.A) && x != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            x = -step;
            y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.D) && x != -step && MainUIController.Instance.isPause == false && isDie == false)
        {
            x = step;
            y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }

        //实现蛇头加速的功能
        if (Input.GetKeyDown(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }


        //实现蛇头阶段加速的功能
            if (MainUIController.Instance.score > 300 && MainUIController.Instance.score < 500 && MainUIController.Instance.isPause == false && isDie == false && isDiao == true)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - addVelocity);
                isDiao = !isDiao;
                Debug.Log("1");
            }
            else if (MainUIController.Instance.score > 500 && MainUIController.Instance.score < 700 && MainUIController.Instance.isPause == false && isDie == false && isDiao1 == true)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - addVelocity * 2);
                isDiao1 = !isDiao1;
                Debug.Log("2");
            }
            else if (MainUIController.Instance.score > 700 && MainUIController.Instance.score < 900 && MainUIController.Instance.isPause == false && isDie == false && isDiao2 == true)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - addVelocity * 3);
                isDiao2 = !isDiao2;
                Debug.Log("3");
             }
            else if (MainUIController.Instance.score > 900 && MainUIController.Instance.score < 1100 && MainUIController.Instance.isPause == false && isDie == false && isDiao3 == true)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - addVelocity * 4);
                isDiao3 = !isDiao3;
                Debug.Log("4");
             }
            else if (MainUIController.Instance.score > 1100 && MainUIController.Instance.isPause == false && isDie == false && isDiao4 == true)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - addVelocity * 5);
                isDiao4 = !isDiao4;
            Debug.Log("5");
        }



        //switch (MainUIController.Instance.score/100) {
        //    case 0:
        //    case 1:
        //    case 2:
        //        break;
        //    case 3:
        //    case 4:
        //        CancelInvoke();
        //        InvokeRepeating("Move", 0, velocity - addVelocity);
        //        break;
        //    case 5:
        //    case 6:
        //        CancelInvoke();
        //        InvokeRepeating("Move", 0, velocity - addVelocity * 2);
        //        break;
        //    case 7:
        //    case 8:
        //        CancelInvoke();
        //        InvokeRepeating("Move", 0, velocity - addVelocity * 3);
        //        break;
        //    case 9:
        //    case 10:
        //        CancelInvoke();
        //        InvokeRepeating("Move", 0, velocity - addVelocity * 4);
        //        break;
        //    default:
        //        CancelInvoke();
        //        InvokeRepeating("Move", 0, velocity - addVelocity * 5);
        //        break;
        //}
        
    }

    //处理蛇头的移动
    public void Move()
    {
        headPosition = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPosition.x+x,headPosition.y+y,headPosition.z);
        if (bodyList.Count > 0) {                          //判断是否有蛇身生成
            for (int i = bodyList.Count - 2; i >= 0; i--) {        //从后面将蛇身向前移动
                bodyList[i + 1].localPosition = bodyList[i].localPosition;      //将蛇身从后往前移动
            }
            bodyList[0].localPosition = headPosition;    //将第一个蛇身的位置设置为蛇头原来的位置
        }
    }

    //完成蛇头吃食物的壮举
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            FoodMaker.Instance.MakeFood((Random.Range(0, 100) < 20) ? true : false);
        }
        else if(collision.tag=="Reward"){
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(0,5)*40);
            Grow();
        }
        else if (collision.tag == "Body")
        {
            Die();
        }
        else {
            if (MainUIController.Instance.hasBorder)
            {
                Die();
            }
            else {
                //实现小蛇穿越边界的功能
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 270, transform.localPosition.y, transform.localPosition.z);  //pc+270
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 330, transform.localPosition.y, transform.localPosition.z);  //pc+330
                        break;
                }
            }

        }

    }

    //生成身体
    void Grow() {
        AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
        int index = (bodyList.Count % 2 == 0) ? 0 : 1;               //根据奇偶性来判断皮肤的生成
        GameObject body = Instantiate(bodyPrefab,new Vector3(500,500,0),Quaternion.identity);      // 将蛇身实例化在场景之外的地方
        body.GetComponent<Image>().sprite = bodySprites[index];                                    //给蛇身穿上皮肤
        body.transform.SetParent(canvas,false);                                                    //设置父物体
        bodyList.Add(body.transform);                                                              //将生成的蛇身添加到集合中
    }

    //处理蛇死亡
    void Die() {
        AudioSource.PlayClipAtPoint(dieClip,Vector3.zero);
        CancelInvoke();       //取消当前场景的游戏主函数的调用，游戏结束
        isDie = true;         //将标志符设为true
        Instantiate(dieEffect);      //实例化死亡特效
        PlayerPrefs.SetInt("lastl", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lasts",MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("bests", 0) < MainUIController.Instance.score) {
            PlayerPrefs.SetInt("bestl", MainUIController.Instance.length);
            PlayerPrefs.SetInt("bests", MainUIController.Instance.score);
        }
        StartCoroutine(Gameover(1.5f));     //开启线程，1.5秒后销毁
    }

    //开启一个线程结束游戏
    IEnumerator Gameover(float t) {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(1);
    }
}
