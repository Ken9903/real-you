using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class EnddingSupporter : MonoBehaviour
{
    private ScenarioManager scenarioManager;
    private FireBase_Ver1 fireBase_Ver1;
    // Start is called before the first frame update
    void Start()
    {
        scenarioManager = GameObject.Find("ScenarioManager").GetComponent<ScenarioManager>();
        RealTime_Event_Trigger realTime_Event_Trigger = GameObject.Find("ScenarioManager").GetComponent<RealTime_Event_Trigger>();
        fireBase_Ver1 = GameObject.Find("FireBase Controller").GetComponent<FireBase_Ver1>();

        //��������
        fireBase_Ver1.CountVote_makeWay_Scenario("Vote4");
        fireBase_Ver1.CountVote_makeWay_Scenario("Vote6");
        fireBase_Ver1.CountVote_makeWay_Scenario("Vote7");
        
        if(scenarioManager.second_turning_point == 0)
        {
            Debug.Log("������ �ʹ�����Ʈ �ʱ�ȭ ����, �� ��ǥ ����");
            fireBase_Ver1.CountVote_makeWay_Scenario_3("Vote2");
        }


        scenarioManager.scenario_Main_Num = 10;
        Debug.Log("��ž �ڷ�ƾ 10 ����");

        StartCoroutine(ending_wait());
        
    }
    IEnumerator ending_wait()
    {
        while(true)
        {
            if(fireBase_Ver1.tasking)
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(1); //task�� �ȳ����� �� ���
                if(fireBase_Ver1.tasking)
                {
                    continue;
                }
                //Fade ȿ�� �߰�
                ending();
                break;
            }
        }
    }
    
    public void ending()
    {
        if(scenarioManager.first_turning_point == 0) //�Ű� ����
        {
            if(scenarioManager.second_turning_point == 1) // ����
            {
                if(scenarioManager.third_turning_point == 0) //����
                {
                    if (scenarioManager.five_turning_point == 0)
                    {
                        if (scenarioManager.six_turning_point == 0) //�Ű� ����
                        {
                            //����0100 -> 2222
                            DialogueManager.StartConversation("2212");
                            Debug.Log("2222");
                        }
                        else if (scenarioManager.six_turning_point == 1) //�Ű� ��
                        {
                            //����0101 -> 2221
                            DialogueManager.StartConversation("2221");
                            Debug.Log("2221");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else if (scenarioManager.five_turning_point == 1)
                    {
                        if (scenarioManager.seven_turning_point == 0) //�Ű� ����
                        {
                            //����0100 -> 2222
                            DialogueManager.StartConversation("2212");
                            Debug.Log("2222");
                        }
                        else if (scenarioManager.seven_turning_point == 1) //�Ű� ��
                        {
                            //����0101 -> 2221
                            DialogueManager.StartConversation("2221");
                            Debug.Log("2221");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }

                }
                else if(scenarioManager.third_turning_point == 1) // â��
                {
                    if(scenarioManager.four_turning_point == 0) //�Ű� ����
                    {
                        //���� 0110 -> 2212
                        DialogueManager.StartConversation("2212");
                        Debug.Log("2212");
                    }
                    else if(scenarioManager.four_turning_point == 1) //�Ű���
                    {
                        //���� 0111 -> 2211
                        DialogueManager.StartConversation("2211");
                        Debug.Log("2211");
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else if(scenarioManager.second_turning_point == 2) //���Ϸ�
            {
                if (scenarioManager.third_turning_point == 0) //����
                {
                    if(scenarioManager.five_turning_point == 0)
                    {
                        if(scenarioManager.six_turning_point == 0) //�Ű� ����
                        {
                            //����0200 -> 2322
                            DialogueManager.StartConversation("2212");
                            Debug.Log("2322");
                        }
                         else if (scenarioManager.six_turning_point == 1) //�Ű� ��
                        {
                            //����0201 -> 2321
                            DialogueManager.StartConversation("2221");
                            Debug.Log("2321");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else if(scenarioManager.five_turning_point == 1)
                    {
                        if (scenarioManager.seven_turning_point == 0) //�Ű� ����
                        {
                            //����0200 -> 2322
                            DialogueManager.StartConversation("2212");
                            Debug.Log("2322");
                        }
                        else if (scenarioManager.seven_turning_point == 1) //�Ű� ��
                        {
                            //����0201 -> 2321
                            DialogueManager.StartConversation("2221");
                            Debug.Log("2321");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                    

                }
                else if (scenarioManager.third_turning_point == 1) // â��
                {
                    if (scenarioManager.four_turning_point == 0) //�Ű� ����
                    {
                        //����0210 -> 2312
                        DialogueManager.StartConversation("2212");
                        Debug.Log("2312");
                    }
                    else if (scenarioManager.four_turning_point == 1) //�Ű���
                    {
                        //����0211 -> 2311
                        DialogueManager.StartConversation("2211");
                        Debug.Log("2311");
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else if(scenarioManager.second_turning_point == 3) // ��
            {
                if (scenarioManager.third_turning_point == 0) //����
                {
                    if (scenarioManager.five_turning_point == 0)
                    {
                        if (scenarioManager.six_turning_point == 0) //�Ű� ����
                        {
                            //����0300 -> 2122
                            DialogueManager.StartConversation("2112");
                            Debug.Log("2122");
                        }
                        else if (scenarioManager.six_turning_point == 1) //�Ű� ��
                        {
                            //����0301 -> 2121
                            DialogueManager.StartConversation("2121");
                            Debug.Log("2121");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else if (scenarioManager.five_turning_point == 1)
                    {
                        if (scenarioManager.seven_turning_point == 0) //�Ű� ����
                        {
                            //����0300 -> 2122
                            DialogueManager.StartConversation("2112");
                            Debug.Log("2122");
                        }
                        else if (scenarioManager.seven_turning_point == 1) //�Ű� ��
                        {
                            //����0301 -> 2121
                            DialogueManager.StartConversation("2121");
                            Debug.Log("2121");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }

                }
                else if (scenarioManager.third_turning_point == 1) // â��
                {
                    if (scenarioManager.four_turning_point == 0) //�Ű� ����
                    {
                        //����0310 -> 2112
                        DialogueManager.StartConversation("2112");
                        Debug.Log("2112");
                    }
                    else if (scenarioManager.four_turning_point == 1) //�Ű���
                    {
                        //����0311 -> 2111
                        DialogueManager.StartConversation("2111");
                        Debug.Log("2111");
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else
            {
                Debug.Log("ERROR");
            }
        }
        else if(scenarioManager.first_turning_point == 1) //�Ű���
        {
            if (scenarioManager.second_turning_point == 1) // ����
            {
                if (scenarioManager.third_turning_point == 0) //����
                {
                    if (scenarioManager.five_turning_point == 0)
                    {
                        if (scenarioManager.six_turning_point == 0) //�Ű� ����
                        {
                            //����1100 -> 1222
                            DialogueManager.StartConversation("1212");
                            Debug.Log("1222");
                        }
                        else if (scenarioManager.six_turning_point == 1) //�Ű� ��
                        {
                            //����1101 -> 1221
                            DialogueManager.StartConversation("1221");
                            Debug.Log("1221");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else if (scenarioManager.five_turning_point == 1)
                    {
                        if (scenarioManager.seven_turning_point == 0) //�Ű� ����
                        {
                            //����1100 -> 1222
                            DialogueManager.StartConversation("1212");
                            Debug.Log("1222");
                        }
                        else if (scenarioManager.seven_turning_point == 1) //�Ű� ��
                        {
                            //����1101 -> 1221
                            DialogueManager.StartConversation("1221");
                            Debug.Log("1221");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }

                }
                else if (scenarioManager.third_turning_point == 1) // â��
                {
                    if (scenarioManager.four_turning_point == 0) //�Ű� ����
                    {
                        //����1110 -> 1212
                        DialogueManager.StartConversation("1212");
                        Debug.Log("1212");
                    }
                    else if (scenarioManager.four_turning_point == 1) //�Ű���
                    {
                        //����1111 -> 1211
                        DialogueManager.StartConversation("1211");
                        Debug.Log("1211");
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else if (scenarioManager.second_turning_point == 2) //���Ϸ�
            {
                if (scenarioManager.third_turning_point == 0) //����
                {
                    if (scenarioManager.five_turning_point == 0)
                    {
                        if (scenarioManager.six_turning_point == 0) //�Ű� ����
                        {
                            //����1200 -> 1322
                            DialogueManager.StartConversation("1312");
                            Debug.Log("1322");
                        }
                        else if (scenarioManager.six_turning_point == 1) //�Ű� ��
                        {
                            //����1201 -> 1321
                            DialogueManager.StartConversation("1221");
                            Debug.Log("1321");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else if (scenarioManager.five_turning_point == 1)
                    {
                        if (scenarioManager.seven_turning_point == 0) //�Ű� ����
                        {
                            //����1200 -> 1322
                            DialogueManager.StartConversation("1312");
                            Debug.Log("1322");
                        }
                        else if (scenarioManager.seven_turning_point == 1) //�Ű� ��
                        {
                            //����1201 -> 1321
                            DialogueManager.StartConversation("1221");
                            Debug.Log("1321");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }


                }
                else if (scenarioManager.third_turning_point == 1) // â��
                {
                    if (scenarioManager.four_turning_point == 0) //�Ű� ����
                    {
                        //����1210 -> 1312
                        DialogueManager.StartConversation("1312");
                        Debug.Log("1312");
                    }
                    else if (scenarioManager.four_turning_point == 1) //�Ű���
                    {
                        //����1211 -> 1311
                        DialogueManager.StartConversation("1211");
                        Debug.Log("1311");
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else if (scenarioManager.second_turning_point == 3) // ��
            {
                if (scenarioManager.third_turning_point == 0) //����
                {
                    if (scenarioManager.five_turning_point == 0)
                    {
                        if (scenarioManager.six_turning_point == 0) //�Ű� ����
                        {
                            //����1300 -> 1122
                            DialogueManager.StartConversation("1122");
                            Debug.Log("1122");
                        }
                        else if (scenarioManager.six_turning_point == 1) //�Ű� ��
                        {
                            //����1301 -> 1121
                            DialogueManager.StartConversation("1121");
                            Debug.Log("1121");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else if (scenarioManager.five_turning_point == 1)
                    {
                        if (scenarioManager.seven_turning_point == 0) //�Ű� ����
                        {
                            //����1300 -> 1122
                            DialogueManager.StartConversation("1122");
                            Debug.Log("1122");
                        }
                        else if (scenarioManager.seven_turning_point == 1) //�Ű� ��
                        {
                            //����1301 -> 1121
                            DialogueManager.StartConversation("1121");
                            Debug.Log("1121");
                        }
                        else
                        {
                            Debug.Log("ERROR");
                        }
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }


                }
                else if (scenarioManager.third_turning_point == 1) // â��
                {
                    if (scenarioManager.four_turning_point == 0) //�Ű� ����
                    {
                        //����1310 -> 1112
                        DialogueManager.StartConversation("1112");
                        Debug.Log("1112");
                    }
                    else if (scenarioManager.four_turning_point == 1) //�Ű���
                    {
                        //����1311 -> 1111
                        DialogueManager.StartConversation("1111");
                        Debug.Log("1111");
                    }
                    else
                    {
                        Debug.Log("ERROR");
                    }
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else
            {
                Debug.Log("ERROR");
            }
        }
        else
        {
            Debug.Log("ERROR");
        }

    }

   
}
