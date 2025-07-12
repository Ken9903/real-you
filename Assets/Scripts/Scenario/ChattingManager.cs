using System; //Serializable �����ϱ� ���� ���.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class ChattingManager : MonoBehaviour //***�ó����� �ѹ��� �����̾ƴ� ������ ���� ����� ���� �پ缺 �ְ� ������ �ʿ�����
{
    //���� Ŭ����
    public ScenarioManager scenarioManager;
    public InputField player_chat_input;

    [Serializable]
    public class ChatDataList
    {
        public string[] chatData; // ���� ��ȭ ���� 
    }
    public ChatDataList[] chatList; //�ó����� �к� 
    public string[] nameList;
    public bool[] sex; //nameList�� �ε����� ���� 0:���� 1:����
    public GameObject[] currentChatList = new GameObject[7] { null, null, null, null, null, null, null }; //***5�� max_chat_num�̶� ����ȭ �ʿ�***
    public Transform[] chatPoint; //���� �迭�� ���� ���� ����


    //����
    public int wait_next_chat_max = 350; //���� ä���� �ö������� �ɸ��� �ð� -> ���������� ȭ���� ǥ�� ���� *0.01���� �ʿ�
    public int wait_next_chat_min = 150;
    public int max_chat_num = 12;
    private int current_chat_num = 0;
    public int max_chat_kind = 30; //���� �ó����� �ϳ��� �� �� �ִ� ��� ä�� ���� ����
    private bool player_chatting = false;


    // ���ҽ�
    public GameObject chatUi; //Ui���ø�
    public Sprite manImage;
    public Sprite womanImage;
    public GameObject donation_panel; //�����̼� ��ü Ui
    public GameObject donate_name_money;
    public GameObject donate_content;


    public void Awake()
    {
        float devicewidth = Screen.width;
        float deviceheight = Screen.height;
        //1.65�̻� ->  8�� 1.82 �̻� -> 7�� 1.99 6��  2.16 -> 5��  0.17  
        double resolution = devicewidth / deviceheight;
        while (true)
        {
            if (resolution - 0.17 >= 2.33)
            {
                Debug.Log(resolution - 0.17);
                max_chat_num--;
                resolution -= 0.17;
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator playChatting()
    {
        while (true)
        {
            if (!player_chatting)
            {
                if (current_chat_num == max_chat_num) // ���� ä���� �ִ�ġ �� ��
                {
                    Destroy(currentChatList[max_chat_num - 1]); //���� �����ִ� ä�� ���� 
                    currentChatList[max_chat_num - 1] = null;
                    current_chat_num--;
                }
                int chatData_Random_Index = UnityEngine.Random.Range(0, max_chat_kind); //ǥ���� ������ ����
                string target_Data = chatList[scenarioManager.Chat_Num].chatData[chatData_Random_Index]; //ǥ���� ä��
                int Name_Sex_Random_Index = UnityEngine.Random.Range(0, 40); // �̸��� ���� ���� ����
                GameObject currentChatUi = Instantiate(chatUi, chatPoint[0]);

                currentChatUi.transform.GetChild(1).GetComponent<Text>().text = target_Data; //�ؽ�Ʈ ����
                currentChatUi.transform.GetChild(2).GetComponent<Text>().text = nameList[Name_Sex_Random_Index]; //�̸� ����
                if (!sex[Name_Sex_Random_Index]) //������ ���� �̹��� ����
                {
                    currentChatUi.transform.GetChild(3).GetComponent<Image>().sprite = manImage;
                }
                else
                {
                    currentChatUi.transform.GetChild(3).GetComponent<Image>().sprite = womanImage;
                }
                for (int i = max_chat_num - 1; i >= 0; i--) //�� ���������� �Ʒ��� ����
                {
                    if (currentChatList[i] != null)
                    {
                        currentChatList[i].transform.SetParent(GameObject.Find(String.Concat("ChatPoint", (i + 2).ToString())).transform);
                        currentChatList[i].GetComponent<RectTransform>().localPosition = Vector3.zero; //��ġ ������ �ʿ�
                        currentChatList[i + 1] = currentChatList[i];
                    }
                }
                currentChatList[0] = currentChatUi;
                current_chat_num++;

                float wait = UnityEngine.Random.Range(wait_next_chat_min, wait_next_chat_max) * 0.01f;
                yield return new WaitForSeconds(wait);
            }
        }
    }




    public void playerChat_onClick()
    {
        player_chatting = true; // ���ÿ� UI�����ϸ� ���� �߻������ϴ� ���� ���� ����
        string chat = player_chat_input.text;
        Debug.Log(chat);

        if (current_chat_num == max_chat_num) // ���� ä���� �ִ�ġ �� ��
        {
            Destroy(currentChatList[max_chat_num - 1]); //���� �����ִ� ä�� ���� 
            currentChatList[max_chat_num - 1] = null;
            current_chat_num--;
        }
        GameObject currentChatUi = Instantiate(chatUi, chatPoint[0]);

        currentChatUi.transform.GetChild(1).GetComponent<Text>().text = chat; //�ؽ�Ʈ ����
        currentChatUi.transform.GetChild(1).GetComponent<Text>().color = Color.black;
        currentChatUi.transform.GetChild(2).GetComponent<Text>().text = "Guest356"; //�̸� -> ���� �޾ƿ���
        currentChatUi.transform.GetChild(3).GetComponent<Image>().sprite = manImage; // ���� �޾ƿ���

        for (int i = max_chat_num - 1; i >= 0; i--) //�� ���������� �Ʒ��� ����
        {
            if (currentChatList[i] != null)
            {
                currentChatList[i].transform.SetParent(GameObject.Find(String.Concat("ChatPoint", (i + 2).ToString())).transform);
                currentChatList[i].GetComponent<RectTransform>().localPosition = Vector3.zero; //��ġ ������ �ʿ�
                currentChatList[i + 1] = currentChatList[i];
            }
        }
        currentChatList[0] = currentChatUi;
        current_chat_num++;

        player_chat_input.text = "";
        player_chatting = false;
    }

    public IEnumerator donate(string name, System.Single money, string content, System.Single delay)
    {
        donation_panel.SetActive(true);
        donate_name_money.GetComponent<Text>().text = name + "����" + money + "���� �Ŀ��ϼ̽��ϴ�.";
        donate_content.GetComponent<Text>().text = content;
        yield return new WaitForSeconds(delay);
        donation_panel.SetActive(false);
    }

    public void playDonate(string name, System.Single money, string content, System.Single delay)
    {
        StartCoroutine(donate(name, money, content, delay));
    }

    public void nextChat()
    {
        scenarioManager.Chat_Num++;
    }
    public void initChat()
    {
        scenarioManager.Chat_Num = 0;
    }
    public void scenarioChat_Setting(System.Single num)
    {
        scenarioManager.Chat_Num = (int)num;
    }

    public void chnage_chat_speed(System.Single min, System.Single max)
    {
        wait_next_chat_min = (int)min;
        wait_next_chat_max = (int)max;
    }
    public void init_chat_speed()
    {
        wait_next_chat_min = 150;
        wait_next_chat_max = 350;
    }

    public void set_chat_time_event()//�ó����� 0�� �ð��˷��ִ� �̺�Ʈ�� �Լ�
    {
        int temp = 0; //*** �ش� ��ȣ �߰� ���
        chatList[temp].chatData[0] = DateTime.Now.ToString("HH") +"�ð� �̾�";
        chatList[temp].chatData[1] = DateTime.Now.ToString("HH") + "����";
        chatList[temp].chatData[2] = "����" + DateTime.Now.ToString("HH") + "��";
        chatList[temp].chatData[3] = "����" + DateTime.Now.ToString("HH") + "��";

    }




    void Start()
    {
        StartCoroutine(playChatting());
    }

    private void OnEnable()
    {
        Lua.RegisterFunction("playDonate", this, SymbolExtensions.GetMethodInfo(() => playDonate((string)"", (int)0, (string)"", (float)0)));
        Lua.RegisterFunction("scenarioChat_Setting", this, SymbolExtensions.GetMethodInfo(() => scenarioChat_Setting((int)0)));
        Lua.RegisterFunction("nextChat", this, SymbolExtensions.GetMethodInfo(() => nextChat()));
        Lua.RegisterFunction("initChat", this, SymbolExtensions.GetMethodInfo(() => initChat()));
        Lua.RegisterFunction("chnage_chat_speed", this, SymbolExtensions.GetMethodInfo(() => chnage_chat_speed((int)0, (int)0)));
        Lua.RegisterFunction("init_chat_speed", this, SymbolExtensions.GetMethodInfo(() => init_chat_speed()));
        Lua.RegisterFunction("set_chat_time_event", this, SymbolExtensions.GetMethodInfo(() => set_chat_time_event()));
    }
    private void OnDisable()
    {
        Lua.UnregisterFunction("playDonate");
        Lua.UnregisterFunction("scenarioChat_Setting");
        Lua.UnregisterFunction("nextChat");
        Lua.UnregisterFunction("initChat");
        Lua.UnregisterFunction("chnage_chat_speed");
        Lua.UnregisterFunction("init_chat_speed");
        Lua.UnregisterFunction("set_chat_time_event");
    }
}
