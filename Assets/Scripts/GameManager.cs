using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanal; // 대화상자 게임 오브젝트
    // public Text talkText; // 대화상자 게임 오브젝트 안에있는 text
    public TypeEffect talk;
    public GameObject scanObject; // 충돌한 오브젝트를 체크할려고
    public Text questText;
    public Image portraitImg;
    public Sprite prevPortrait;
    public Animator portraitAnim; // 스프라이트와 다르면 이 애니메이션 실행
    public GameObject meneSet;
    public bool isAction;
    public int talkIndex = 0;
    public GameObject player;

    public Text Name;
    void Awake()
    {
        // talkPanal.SetActive(false); // 처음 대화상자는 false 값으로 보이지 않는다.
        Debug.Log(questManager.CheckQuest());
        Name.text="";
    }
    private void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (meneSet.activeSelf)
            {
                meneSet.SetActive(false);
            }
            else
            {
                meneSet.SetActive(true);
            }
        }
    }
    public void Action(GameObject scanObj) // GameObject에서 scanObj는 레이캐스트에서 충돌한 collider 값을 반환한다.
    {
        // if (isAction) // Exit Action
        // {
        //     isAction = false;
        // }
        // Enter Action

        isAction = true; // Action값이 true이므로, 패널 깨울 준비
        scanObject = scanObj; // scanObj는 콜라이더 값을 받고
        ObjData objData = scanObject.GetComponent<ObjData>(); // objData에서 id값과 bool값인 NPC 를 받아온다.
        Talk(objData.id, objData.isNpc); // npc 판별과, id값을 넘겨줘요
        nameSearch(objData.id, objData.isNpc); // 충돌한 오브젝트의 이름 부여
        talkPanal.SetBool("isShow", isAction); // 충돌한 객체가 있는 경우이므로, 패널 깨운다.
    }

    void nameSearch(int id, bool isNpc)
    {
        if (isNpc)
        {
            switch (id)
            {
                case 1000:
                Name.text = "루도";
                    break;
                case 2000:
                Name.text = "루나";
                    break;
            }
        } else {
            switch (id)
            {
                case 100:
                Name.text = "나무상자";
                break;
                case 200:
                Name.text = "팻말";
                break;
                case 5000:
                Name.text = "동전";
                break;
            }
        }
    }
    void Talk(int id, bool isNpc)
    { // id값과 isNpc값을 받은 Talk 메서드는
        int questTalkIndex = 0;
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMSg("");
            return;
        }
        else
        {
            // set Talk Data
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
            // string talkData = talkManager.GetTalk(id,talkIndex); // TalkManager에 있는 GetTalk를 꺼내와서 id와 talkIndex를 받아온다.

        }


        // End Talk
        if (talkData == null)
        { // talkData가 null이된다면
            isAction = false; // 패널을 지우고
            talkIndex = 0; // 인덱스값을 초기화한다. 이를 초기화하지 않을 경우에 대화가 이어지지 않는다.
            questText.text = questManager.CheckQuest(id);
            // Debug.Log(questManager.CheckQuest(id));
            return; // void 함수에서 return;은 강제 종료 역할
        }

        // Continue Talk
        if (isNpc)
        { // NPC인가?
            // talkText.text = talkData.Split(':')[0]; // 맞으면 NPC 텍스트 값 가져옴
            talk.SetMSg(talkData.Split(':')[0]);
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            // Name.text = (scanObject.name == "NPC A" ? "루도" : "루나"); // 너무 위험하지 않나
            portraitImg.color = new Color(1, 1, 1, 1); // 초상화 이미지 보이고

            // Animation Portrait
            if (prevPortrait != portraitImg.sprite) // 이전 초상화와 현재 초상화가 다르면
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            // talkText.text = talkData; // 아니면 텍스트 값 가져옴
            talk.SetMSg(talkData);
            // Name.text = scanObject.name;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true; // Action은 true고
        talkIndex++; // index값을 증가시켜서 다음 대화가 나오도록 한다.
    }
    public void GameSave()
    {
        // player.x
        PlayerPrefs.SetFloat("playerX", player.transform.position.x);
        // player.y
        PlayerPrefs.SetFloat("playerY", player.transform.position.y);
        // Quest Id
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        // Quest Action Index
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        meneSet.SetActive(false);
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("playerX"))
        { // 한번이라도 save 한 적 없으면 Load 하지마
            return;
        }
        float x = PlayerPrefs.GetFloat("playerX");
        float y = PlayerPrefs.GetFloat("playerY");
        int quId = PlayerPrefs.GetInt("QuestId");
        int quActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = quId;
        questManager.questActionIndex = quActionIndex;
        questManager.ControlObject();
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
