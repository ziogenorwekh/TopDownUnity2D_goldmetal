using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; // 1. C#에서 자바의 해쉬맵과 같은 역할
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // 2. 인스턴스
        portraitData = new Dictionary<int, Sprite>();
        GenerateData(); // 이 메서드를 호출해서 스트링 데이터를 만들어요
    }

    private void GenerateData() // 데이터를 생성하는데, 각 id별로 대화내용을 스트링 배열로 넣는다.
    {
        talkData.Add(1000, new string[] { "안녕?:1", "이 곳에 처음 왔구나?:2", "갑자기 화나네:0" }); // 루도
        talkData.Add(2000, new string[] {"여긴 내가 사는 집이야:7", "이 마을은 정말 한적하고 아름다워:5", "내 뒤의 작은 개울에서는 물고기도 있어!:5"
        ,"너도 함께 이 마을에서 살아볼래?:7"}); // 루나
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 팻말이다." });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] {"어서 와.:1","이 마을에 놀라운 전설이 있다는데:3",
        "오른쪽 마을에 루나가 알려줄거야:1"});
        // 첫번째 Data가 끝나고 2000 id 데이터를 못 찾을 경우 1000 id는 이 말을 계속 반복
        // talkData.Add(11 + 1000, new string[] {"아직 못찾았어?:0", "오른쪽 마을에 있어 어서 찾아봐.:1"}); 


        talkData.Add(11 + 2000, new string[] {"안녕?:5", "이 마을에 전설을 들으러 온거야?:6",
        "그림 일 좀 하나 해주면 좋을텐데...:7","내 집 근처에 떨어진 동전 좀 주워줬으면 해!:6"});
        talkData.Add(13+2000, new String[] {"이 근처에 있을텐데..?:7", "빨리 좀 찾아줬으면 좋겠어!:6"});

        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });
        
        talkData.Add(20 + 1000, new string[] {"루나의 동전?:1","돈을 흘리고 다니면 못 쓰지!:0",
        "나중에 루나에게 한마디 해야겠어:3"});
        
        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다줘:7" });
        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워!:6" });

        portraitData.Add(1000 + 0, portraitArr[0]); // 화
        portraitData.Add(1000 + 1, portraitArr[1]); // 평범
        portraitData.Add(1000 + 2, portraitArr[2]); // 스마일
        portraitData.Add(1000 + 3, portraitArr[3]); // 대화
        portraitData.Add(2000 + 4, portraitArr[4]); // 화
        portraitData.Add(2000 + 5, portraitArr[5]); // 평범
        portraitData.Add(2000 + 6, portraitArr[6]); // 스마일
        portraitData.Add(2000 + 7, portraitArr[7]); // 대화
    }

    public String GetTalk(int id, int talkIndex)
    { // 아이디 값과 스트링 배열의 값을 받으면
        if (!talkData.ContainsKey(id))
        {

            if (!talkData.ContainsKey(id - id % 10))
            {
                // 퀘스트 맨 처음 대사마저 없을 때 , 기본 대사를 가지고 온다.
                return GetTalk(id - id%100,talkIndex);
            }
            else
            {
                // 퀘스트 해당 진행 순서 대사가 없을 때, 퀘스트 맨 처음으로 대사를 가지고 온다.
                return GetTalk(id - id%10,talkIndex);
            }
        }
        if (talkIndex == talkData[id].Length)
        { // 스트랭 배열의 값에 도달하면
            return null; // null 리턴
        }
        else
        { // 그것이 아닌 경우 딕셔너리를 계속 반환한다. 딕셔너리를 계속 반환하면서 이 다음 GameManager 참조
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
