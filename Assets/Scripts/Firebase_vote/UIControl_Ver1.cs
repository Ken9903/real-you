using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;
using PixelCrushers.DialogueSystem;

public class UIControl_Ver1 : MonoBehaviour
{
    public Vote_Slider voteSlider;
    public FireBase_Ver1 firebase;
    public GameObject ResultUI;

    public Text Result_Txt;
    public Image PieChart;
    public Image[] pieCharts;

    public Button Trigger;

    


    // Agree ��ư�� �Ҵ�� �Լ�
    // Vote2�� Debug�̸�
    public void Agree(string voteName)
    {
          firebase.SendVote(voteName, true);
    }

    // Disagree ��ư�� �Ҵ�� �Լ�
    public void Disagree(string voteName)
    {
          firebase.SendVote(voteName, false);
    }

    public void Select1(string voteName)
    {
        firebase.SendVote_More3(voteName, 1);
    }

    public void Select2(string voteName)
    {
        firebase.SendVote_More3(voteName, 2);
    }

    public void Select3(string voteName)
    {
        firebase.SendVote_More3(voteName, 3);
    }


    // 5�ʰ� Result ȭ���� �����ְ� ������ IEnumerator �Լ�
    public IEnumerator viewResult(string voteName)
    {
        firebase.CountVote(voteName);
        yield return new WaitForSeconds(5); 
        ResultUI.SetActive(false);
    }
    public void viewResultStart(string voteName)
    {
        StartCoroutine(viewResult(voteName));
    }

    public void ResultChange(long agreeCount, long DisagreeCount)
    {
        float Percent_A = (((float)agreeCount) / ((float)(agreeCount + DisagreeCount)));
        float Percent_D = (((float)DisagreeCount) / ((float)(agreeCount + DisagreeCount)));

        PieChart.fillAmount = 0f;

        Result_Txt.text = (Percent_A*100).ToString("F1") + " %�� ������� 1����������" + "\n" +
            (Percent_D*100).ToString("F1") + "%�� ������� 2����������" + "\n" +
            "�����߽��ϴ�.";

        ResultUI.SetActive(true);

        StartCoroutine(ChartEffect(Percent_A));
    }

    

    public IEnumerator ChartEffect(float percent)
    {
        float time = 0f;
        // �ִϸ��̼� ��� �ð�
        float effectTime = 1f;
        pieCharts[1].fillAmount = 0;
        pieCharts[0].fillAmount = 0;
        pieCharts[2].fillAmount = 1;

        while (PieChart.fillAmount < percent)
        {
            time += Time.deltaTime / effectTime;
            PieChart.fillAmount = Mathf.Lerp(0, 1, time);
            yield return null;
        }
    }




    public void viewResultStart_3(string voteName)
    {
        StartCoroutine(viewResult_More3(voteName));
    }
    public IEnumerator viewResult_More3(string voteName)
    {
        firebase.CountVote_More3(voteName);
        yield return new WaitForSeconds(5);
        ResultUI.SetActive(false);
    }

    public void ResultChange_More3(DataSnapshot snapshot)
    {
        int index = 1;
        List<long> counts = new List<long>();
        List<float> percents = new List<float>();

        long sum = 0;
        Result_Txt.text = "";

        for (int i = 0; i < 3; i++)
        {
            counts.Add(snapshot.Child("Select" + index.ToString()).ChildrenCount);
            sum += counts[i];
            index += 1;
        }

        index = 1;

        foreach (var data in counts)
        {
            percents.Add((float)data / (float)sum);
            Result_Txt.text += index + "�� �������� " + ((float)data / (float)sum * 100).ToString("F1") + "%\n";
            index += 1;
        }

        ResultUI.SetActive(true);

        StartCoroutine(ChartEffect_3(index - 1, percents));
    }

    // ��Ʈ ����ư�� ���� ���� ������ ���� ��� �����ϰ�
    // 4���� �Ǹ鼭 Pie Chart Image�� 4���� �÷���� ��.
    public IEnumerator ChartEffect_3(int count, List<float> percents)
    {
        float time = 0f;
        // �ִϸ��̼� ��� �ð�
        float effectTime = 1f;
        float fullAmount = 0f;

        for (int i = 0; i < 4; i++)
        {
            pieCharts[i].fillAmount = 0f;
        }

        for (int i = 0; i < count; i++)
        {
            fullAmount += percents[i];
            while (pieCharts[3 - i].fillAmount < fullAmount)
            {
                time += Time.deltaTime / effectTime;
                pieCharts[3 - i].fillAmount = Mathf.Lerp(0, 1, time);
                yield return null;
            }
        }

    }

    public void scenario_vote_count(string voteName)
    {
        firebase.CountVote_makeWay_Scenario(voteName);
    }
    public void scenario_vote_count_3(string voteName)
    {
        firebase.CountVote_makeWay_Scenario_3(voteName);
    }


    private void OnEnable()
    {
        Lua.RegisterFunction("Agree", this, SymbolExtensions.GetMethodInfo(() => Agree((string)"")));
        Lua.RegisterFunction("Disagree", this, SymbolExtensions.GetMethodInfo(() => Disagree((string)"")));
        Lua.RegisterFunction("Select1", this, SymbolExtensions.GetMethodInfo(() => Select1((string)"")));
        Lua.RegisterFunction("Select2", this, SymbolExtensions.GetMethodInfo(() => Select2((string)"")));
        Lua.RegisterFunction("Select3", this, SymbolExtensions.GetMethodInfo(() => Select3((string)"")));
        Lua.RegisterFunction("viewResultStart", this, SymbolExtensions.GetMethodInfo(() => viewResultStart((string)"")));
        Lua.RegisterFunction("viewResultStart_3", this, SymbolExtensions.GetMethodInfo(() => viewResultStart_3((string)"")));
        Lua.RegisterFunction("scenario_vote_count", this, SymbolExtensions.GetMethodInfo(() => scenario_vote_count((string)"")));
        Lua.RegisterFunction("scenario_vote_count_3", this, SymbolExtensions.GetMethodInfo(() => scenario_vote_count_3((string)"")));
    }
    private void OnDisable()
    {
        Lua.UnregisterFunction("Agree");
        Lua.UnregisterFunction("Disagree");
        Lua.UnregisterFunction("Select1");
        Lua.UnregisterFunction("Select2");
        Lua.UnregisterFunction("Select3");
        Lua.UnregisterFunction("viewResultStart");
        Lua.UnregisterFunction("viewResultStart_3");
        Lua.UnregisterFunction("scenario_vote_count");
        Lua.UnregisterFunction("scenario_vote_count_3");
    }

}
