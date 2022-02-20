using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h; // 위아래
    float v; // 좌우    
    public float speed;
    Rigidbody2D rigid;
    bool isHorizonMove;
    Vector3 dirVec;
    Animator anim;
    GameObject scanObject;
    public GameManager gameManager;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move Value
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        // Check Button Up$Down , gameManager.isAction ? false : 이건 채팅 패널이 활성화 되어있을때 움직이지 못하도록 하는 것
        bool hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");
        // Check Hrizontal Move 
        if (hDown)
        { // 참이면
            isHorizonMove = true; // 수평 이동 true
        }
        else if (vDown)
        { // 참이면
            isHorizonMove = false; // 수평 이동 false
        } else if(hUp || vUp) {
            isHorizonMove = h != 0;
        }

        // Animation 타입이 같지 않으면 명시적으로 형변환
        // 기존에 파라미터 값에 같은 값이 들어있으면
        if (anim.GetInteger("hAxisRaw") != h)
        { // 둘의 값이 달라야지
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h); // anim에 인테자 값을 넘겨준다.
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        { // 마찬가지로 둘의 값이 다르면
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else // 값이 바뀌지 않았을 때에는 값을 false로
        {
            anim.SetBool("isChange", false);
        }

        // 방향 1. 우리가 바라보는 방향에서
        if(vDown && v == 1) {
            dirVec = Vector3.up;
        } else if (vDown && v == -1) {
            dirVec = Vector3.down;
        } else if (hDown && h == -1) {
            dirVec = Vector3.left;
        } else if (hDown && h == 1) {
            dirVec = Vector3.right;
        }
        // 4. 디버그로 무엇이 찍혔는지 본다
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
        // 무브
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v); // 대각선 이동 제한 수평 참이면 수직만 false면 수평만
        rigid.velocity = moveVec * speed;
        // 레이 2. 레이를 쏴서
        Debug.DrawRay(rigid.position,dirVec*0.7f,new Color(1,0,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position,dirVec,0.7f,LayerMask.GetMask("Object"));
        // 3. 콜라이더에 맞은 값이 널이 아닐 경우에
        if(rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        } else {
            scanObject = null;
        }
    }
}
