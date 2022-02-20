using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string questName;
    public int[] npcId; // 퀘스트와 연관 되어있는 npc아이디

    public QuestData(string questName,int[] npcId) {
        this.questName = questName;
        this.npcId = npcId;
    }
}
