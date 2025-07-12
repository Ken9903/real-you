using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScenarioManager : MonoBehaviour
{
    public DataController dataController;

    public int scenario_Main_Num = 0;  //�ó����� �ѹ��� ���, RealTImeƮ������ �ð��� ���� //�б�� ���� X �ð��� ū���
    public int Chat_Num = 0; //������ �ʿ� ���� �ó����� ���۰� ���ÿ� �ش� ��ȣ�� �ʱ�ȸ -> ���ӽ� �������ϸ� Wait���� ���ϱ�.
    public int first_turning_point = 0;
    public int second_turning_point = 0;
    public int third_turning_point = 0;
    public int four_turning_point = 0;
    public int five_turning_point = 0;
    public int six_turning_point = 0;
    public int seven_turning_point = 0;
    public int scenario_count = 5; //�� �ó����� ����
    public bool[] watch_scenario;

    public int notWatch = 0;

    public bool profile_btn = false;
    public bool memo_btn = false;
    public Text TitleUi;

    // Loading ����
    public GameObject LoadingUI;
    public CanvasGroup canvasGroup;
    public string loadSceneName;


    public int get_turning_point(string voteName)
    {
        if (voteName == "Vote1")
        {
            return first_turning_point;
        }
        else if (voteName == "Vote2")
        {
            return second_turning_point;
        }
        else if (voteName == "Vote3")
        {
            return third_turning_point;
        }
        else if (voteName == "Vote4")
        {
            return four_turning_point;
        }
        else if (voteName == "Vote5")
        {
            return five_turning_point;
        }
        else if (voteName == "Vote6")
        {
            return six_turning_point;
        }
        else if (voteName == "Vote7")
        {
            return seven_turning_point;
        }
        else
        {
            Debug.Log("get_turning_point Error");
            return 0;
        }
    }

    public void sceneChange(string scenename)
    {
        Debug.Log("��ü����" + scenename);
        if(scenename == "IdleScene")
        {
            TitleUi.text = "��� ��� ��";
        }
        LoadingUI.SetActive(true);
        StartCoroutine(LoadSceneProcess(scenename));
        //SceneManager.LoadScene(scenename); �񵿱� �ε����� �Ȱ��� ������ Ȱ��ȭ
    }
    private IEnumerator LoadSceneProcess(string scenename)
    {
        while (true)
        {
            if (SceneManager.GetActiveScene().name == "InitScene")
            {
                //Init�� �̸� ���̵� ��� �̷�
            }
            else
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                loadSceneName = scenename;
                break;
            }
            yield return new WaitForSeconds(1);
        }
        // Fade�� �ε� UI ����
        yield return StartCoroutine("Fade", true);

        Debug.Log("�� �񵿱� �ε� ����");
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;

        Debug.Log("�񵿱� �ε� while ���� ��");
        while (!op.isDone)
        {
            yield return null;
            if (op.progress >= 0.9f)
            {
                // ���� �ҷ����� �� ����ϴ� �ð� ����
                yield return new WaitForSeconds(5f);

                // �� �ε� �����ϰ� bool �� ����
                op.allowSceneActivation = true;
                Debug.Log("�� �񵿱� �ε� ����");
                yield break;
            }
        }
    }

    // �� �ε� �غ� ���� �Ϸ�� �� ������ �˷��ִ� �ݹ� �Լ�
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // UI�� Fade�� �ڿ������� ��Ÿ���� �ϴ� �ڷ�ƾ, bool���� true�� ��Ÿ����, false�� �������
    private IEnumerator Fade(bool isFadeIn)
    {
        Debug.Log("Fade ����");
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 1f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }
        Debug.Log("Fade ����");
        if (!isFadeIn)
        {
            LoadingUI.SetActive(false);
        }
    }
    public void set_3Choice_Pos()
    {
        RectTransform rect = GameObject.Find("Bottom Panel").GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, 0);

    }

    public void set_profile_btn_active_data()
    {
        this.profile_btn = true;
        dataController.SaveGameData();
    }
    public void set_memo_btn_active_data()
    {
        this.memo_btn = true;
        dataController.SaveGameData();
    }



    private void OnEnable()
    {
        Lua.RegisterFunction("get_turning_point", this, SymbolExtensions.GetMethodInfo(() => get_turning_point((string)"")));
        Lua.RegisterFunction("sceneChange", this, SymbolExtensions.GetMethodInfo(() => sceneChange((string)"")));
        Lua.RegisterFunction("set_3Choice_Pos", this, SymbolExtensions.GetMethodInfo(() => set_3Choice_Pos()));
        Lua.RegisterFunction("set_profile_btn_active_data", this, SymbolExtensions.GetMethodInfo(() => set_profile_btn_active_data()));
        Lua.RegisterFunction("set_memo_btn_active_data", this, SymbolExtensions.GetMethodInfo(() => set_memo_btn_active_data()));
    }
    private void OnDisable()
    {
       Lua.UnregisterFunction("get_turning_point");
       Lua.UnregisterFunction("sceneChange");
        Lua.UnregisterFunction("set_3Choice_Pos");
        Lua.UnregisterFunction("set_profile_btn_active_data");
        Lua.UnregisterFunction("set_memo_btn_active_data");
    }

}
