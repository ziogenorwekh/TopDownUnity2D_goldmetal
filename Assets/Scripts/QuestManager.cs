using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int questId;
    public int questActionIndex;

    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;
    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GeneratData();
    }

    private void GeneratData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기.", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("루나의 동전 찾아주기.", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 }));
    }
    public int GetQuestTalkIndex(int id)
    { //npc id를 받고 퀘스트번호를 반환하는 함수 생성
        return questId + questActionIndex;
    }
    public string CheckQuest(int id) // 대화 진행을 위해 퀘스트 대화 순서를 올리는 함수   
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // Control quest object
        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }

        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
    public void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                {
                    questObject[0].SetActive(true); // 퀘스트를 받으면 액티브
                }
                break;
            case 20:
                if (questActionIndex == 0)
                {
                    questObject[0].SetActive(true); // 퀘스트를 받으면 액티브
                }
                else if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false); // 주서먹으면 비액티브
                }
                break;
        }
    }
}
