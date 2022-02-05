using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSystem : MonoBehaviour
{

    public Text timeText;

    public int count;
    private bool cangoal, goalnow = false, StartGoalLine = false;
    private float time;
    private float seconds, minutes;
    private GameObject[] checkPoints;
    public int countRound = 0;
    public float[] roundTimes;

    void Start()
    {
        checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
    }

    void Update()
    {
        timer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")//チェックポイントに触れた
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            count += 1;
        }
        if (other.gameObject.tag == "Line")//スタートラインに触れた
        {
            StartGoalLine = true;
            if (count == 2)//チェックポイントをすべて通ったか
            {
                Debug.Log("GOAL!");

                // 再度表示する
                foreach(GameObject point in checkPoints)
                {
                    point.SetActive(true);
                }
                goalnow = true;
            }
        }
    }

    void timer()
    {
        if (StartGoalLine)
        {
            time += Time.deltaTime;
            minutes = time / 60f;
            seconds = time % 60f;
        }


        if (!goalnow)
        {
            //ゴールしてない
            timeText.text = "Time " + ((int)minutes).ToString("00") + " : " + ((int)seconds).ToString("00");
        }
        else
        {
            //ゴールした
            roundTimes[countRound] = time;
            countRound++;
            time = 0;
            count = 0;
            goalnow = false;
            if(countRound == roundTimes.Length)
            {
                StartGoalLine = false;
            }
        } 
    }
}