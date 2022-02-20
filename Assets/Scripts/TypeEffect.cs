using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeEffect : MonoBehaviour
{
    string targetMsg; // 표시할 대화 문자열을 따로 저장
    public int charPerSec; // 글자 재생 속도
    int index;
    Text msgText;
    public GameObject EndCursor;
    AudioSource audioSource;
    float interval; 
    public bool isAnim;
    // Start is called before the first frame update
    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMSg(string msg)
    {
        if(isAnim) {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        } else {
        targetMsg = msg;
        EffectStart();
        }
    }
    void EffectStart()
    {
        isAnim = true;
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);
        // Effecting();
        interval = 1.0f/ charPerSec;
        Invoke("Effecting", interval); // 1/charPerSec 1글자가 나오는 딜레이
    }
    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();
        index++; 
        Invoke("Effecting", interval); // 1/charPerSec 1글자가 나오는 딜레이

    }
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
        index = 0;
    }
}
