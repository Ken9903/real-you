using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTime_Event_Trigger : MonoBehaviour
{
    public ScenarioManager scenarioManager;
    public DataController dataController;
    public FireBase_Ver1 fireBase;
    public UIControl_Ver1 uIControl;

    public LocalNotification_main localNotification_main;


    public float courutine_wait_second = 3f; 

    public bool init = false;  
    public bool CheckTrigger_on = false;

    public Text TimeUI;
    public Text TitleUi;


    public DateTime startTime = new DateTime(); 
    DateTime currentTime = new DateTime();
    public TimeSpan span;

    
    //TriggerTIme
    public int[] triggerTime; 
    public int[] waitTriggerTime; 

    public IEnumerator CheckTrigger()
    {
        while(true) 
        {
            if (scenarioManager.scenario_Main_Num == 10) //엔딩 종료 했을 때.
            {
                Debug.Log("코루틴 무한 대기");
                yield return new WaitForSeconds(1000000000);
                break;
            }
            double checkedTime = CheckTime();
            CheckTrigger_on = true;
            if (checkedTime >= triggerTime[scenarioManager.scenario_Main_Num])
            {
                Debug.Log("시나리오" + scenarioManager.scenario_Main_Num + "번");
                scenarioManager.watch_scenario[scenarioManager.scenario_Main_Num] = true;

                if(scenarioManager.scenario_Main_Num == 1)
                {
                    uIControl.scenario_vote_count("Vote1");                 
                }
                else if(scenarioManager.scenario_Main_Num == 2)
                {
                    uIControl.scenario_vote_count_3("Vote2");
                    uIControl.scenario_vote_count("Vote3");  
                }
                else if (scenarioManager.scenario_Main_Num == 3)
                {
                    uIControl.scenario_vote_count("Vote5"); 
                }
                scenarioManager.scenario_Main_Num++; 

                dataController.SaveGameData();

                break;
            }

            yield return new WaitForSeconds(courutine_wait_second);
        }
        if(scenarioManager.scenario_Main_Num == 1)
        {
            TitleUi.text = "첫째 날, 첫 방송";
        }
        else if(scenarioManager.scenario_Main_Num == 2)
        {
            TitleUi.text = "첫째 날, 두번째 방송";
        }
        else if (scenarioManager.scenario_Main_Num == 3)
        {
            TitleUi.text = "둘째 날, 세번째 방송";
        }
        else if (scenarioManager.scenario_Main_Num == 4)
        {
            TitleUi.text = "둘째 날, 네번째 방송";
        }
        else if (scenarioManager.scenario_Main_Num == 5)
        {
            TitleUi.text = "셋째 날, 마지막 방송";
        }
        Debug.Log("시행 번호" + (scenarioManager.scenario_Main_Num - 1));
        scenarioManager.sceneChange("Scene" + (scenarioManager.scenario_Main_Num - 1));

        if (scenarioManager.scenario_Main_Num == 5)
        {
            StopCoroutine(CheckTrigger());
        }
        StartCoroutine(CheckTrigger());

    }
    void MakeNoti()
    {
        for(int i = 0; i < 5; i++)
        {
            TimeSpan nextTimeSpan = new TimeSpan(0, 0, 0, triggerTime[i]);
            localNotification_main.AddLocalNotification(nextTimeSpan - (DateTime.Now - startTime), triggerTime[i], i, startTime);
        }
        Debug.Log("알람 푸쉬 완료");
        yield return null;

    }


    private void Awake()
    {
        dataController.LoadGameData();

        if(init == false)
        {
            Debug.Log("초기화 시작");
            startTime = DateTime.Now;
            init = true;
            MakeNoti(); //한번 시행
  
            dataController.SaveGameData(); //체크트리거 시작 전에 종료되면 알람이 적용이 안됨
        }
   
        
    }
    // Start is called before the first frame update

    public void StartMain()
    {
        if(scenarioManager.scenario_Main_Num == 5)
        {
            Debug.Log("시나리오 종료");
            return;
        }
        double checkedTime = CheckTime();
        for(int i = 0; i < 5; i++) 
        {
            if(checkedTime >= triggerTime[i] && !scenarioManager.watch_scenario[i]) 
            {
                if(checkedTime > waitTriggerTime[i]) 
                {
                    if(scenarioManager.scenario_Main_Num == 1)
                    {
                        fireBase.CountVote_makeWay_Scenario("Vote1");
                    }
                   else if(scenarioManager.scenario_Main_Num == 2)
                    {

                        fireBase.CountVote_makeWay_Scenario_3("Vote2");
                        fireBase.CountVote_makeWay_Scenario("Vote3"); 
                    }
                    else if(scenarioManager.scenario_Main_Num == 3)
                    {
                        fireBase.CountVote_makeWay_Scenario("Vote5");
     
                    }
                    scenarioManager.notWatch++;

                    scenarioManager.watch_scenario[i] = true;
                    scenarioManager.scenario_Main_Num++;

                    dataController.SaveGameData();
                }
            }
        }
        if (scenarioManager.scenario_Main_Num == 5)
        {
            return;
        }
        StartCoroutine(CheckTrigger());
    }

    public double CheckTime()
    {
        currentTime = DateTime.Now;
        span = currentTime - startTime;

        TimeUI.text = span.ToString(@"dd\:hh\:mm\:ss");

        return span.TotalSeconds;
    }



}
