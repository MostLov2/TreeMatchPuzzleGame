using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CrashSystem : MonoBehaviour
{
    //----------------------TouchInfo--------------------------
    float                       MaxDistance = 15;//레이케스트 사거리
    Vector3                     mousePos;//마우스 위치 저장
    float                       moveSpeed=15;
    int                         layerMask = 1 << 8;
    //----------------------PlayerWeapon--------------------------
    Transform                   dragonflyStick;//잠자리채
    Transform                   spray;//살충제
    //----------------------Camera--------------------------
    Transform                   cam;
    Transform                   camMini;
    GameObject                  tutorialCam;
    //----------------------Collider--------------------------
    CircleCollider2D            col;
    //----------------------Button--------------------------
    Transform                   sprayButton;
    Transform                   dragonflyButton;
    //----------------------Bool--------------------------
    bool                        youCanTouch;
    bool                        isDragonflyBT;//잠자리채 버튼을 클릭시 켜졌는지 확인
    bool                        isSprayBT;//살충제 버튼을 클릭시 켜졌는지 확인
    //----------------------AudioClip--------------------------
    [SerializeField] AudioClip[] clip;
    private void Awake()
    {
        cam =               Camera.main.transform;
        //tutorialCam =       GameObject.FindGameObjectWithTag("TutorialCam");
        camMini =           GameObject.FindGameObjectWithTag("MiniGameCam").GetComponent<Transform>();

        sprayButton =       GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>();//c
        dragonflyButton =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).transform.GetChild(1).GetComponent<Transform>();//c

        col =               GetComponent<CircleCollider2D>();

        dragonflyStick =    transform.GetChild(0);
        spray =             transform.GetChild(1);

        youCanTouch =       true;
        isDragonflyBT =     true;
        isSprayBT =         false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!MiniGameManager.isMiniGameStart)
        {
            if (collision.CompareTag("Right"))//우측이동
            {
                Vector3 moveMent = Vector3.right * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            if (collision.CompareTag("Left"))//좌측 이동
            {
                Vector3 moveMent = Vector3.left * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            if (collision.CompareTag("Down"))//아래 이동
            {
                Vector3 moveMent = Vector3.down * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            if (collision.CompareTag("Up"))//위쪽이동
            {
                Vector3 moveMent = Vector3.up * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            float x = Mathf.Clamp(cam.position.x, -11, 11);//좌우값 고정
            float y = Mathf.Clamp(cam.position.y, -16, 8);//위아래 고정
            cam.position= new Vector3(x,y, -1);
        }
    }
    private void Update()
    {
        PlayerControl();
    }
    /// <summary>
    /// 터치 시 작용하는 함수
    /// </summary>
    void PlayerControl()
    {
        mousePos = Input.mousePosition;//터치한곳에 값을 저장
        if (!MiniGameManager.isMiniGameStart)
        {
            ItemMove();
        }
        if(MiniGameManager.isMiniGameStart)
        {
            mousePos = camMini.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            if (Input.GetMouseButton(0))//마우스 좌클릭 및 터치
            {
                col.enabled = true;
            }
        }
        #region 주석
        /*if (Tutorial.isTutorial)
        {
            if (!EventSystem.current.IsPointerOverGameObject())//버튼 터치시 다른 이벤트가 안일어나는 조건문
            {
                mousePos = tutorialCam.GetComponent<Camera>().ScreenToWorldPoint(mousePos);//저장한 값을 화면에 같은 위치로 변경
                if (Input.GetMouseButton(0))//마우스 좌클릭 및 터치
                {
                    col.enabled = true;
                    if (isDragonflyBT)//잠자리채 버튼 터치시
                    {
                        spray.gameObject.SetActive(false);
                        dragonflyStick.gameObject.SetActive(true);
                    }
                }
                if (Input.GetMouseButtonDown(0) && youCanTouch)
                {
                    col.enabled = true;
                    if (isSprayBT)//스프레이 버튼 터치시 
                    {
                        int randomNum = Random.Range(0, 2);
                        SoundManager.instance.PlaySFX(clip, randomNum, 1, 1);
                        dragonflyStick.gameObject.SetActive(false);
                        spray.gameObject.SetActive(true);
                        StartCoroutine(SprayTouch());
                    }
                }
            }
        }*/
        #endregion
        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, MaxDistance,layerMask);//레이케스트 발사 설정
        if(hit.collider != null)
        {
            if (!hit.collider.CompareTag("UI"))
            {
                transform.position = hit.point;//오브젝트의 위치를 터치한곳으로 변경
            }
            
            if (Input.GetMouseButtonUp(0)|| hit.collider.CompareTag("UI"))//마우스 및 터치를 하지않았을때 뜨지 않게
            {
                dragonflyStick.gameObject.SetActive(false);
                //spray.gameObject.SetActive(false);
                col.enabled = false;
            }
        }
    }
    /// <summary>
    /// 무기 움직이게 하고 작용하게 하는 함수
    /// </summary>
    void ItemMove()//마우스 이동포인트로 아이템 이동하는 함수
    {
        if (!EventSystem.current.IsPointerOverGameObject())//버튼 터치시 다른 이벤트가 안일어나는 조건문
        {
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);//저장한 값을 화면에 같은 위치로 변경
            if (Input.GetMouseButton(0))//마우스 좌클릭 및 터치
            {
                col.enabled = true;
                if (isDragonflyBT)//잠자리채 버튼 터치시
                {
                    spray.gameObject.SetActive(false);
                    dragonflyStick.gameObject.SetActive(true);
                }
            }
            if (Input.GetMouseButtonDown(0) && youCanTouch)
            {
                col.enabled = true;
                if (isSprayBT)//스프레이 버튼 터치시 
                {
                    int randomNum = Random.Range(0, 2);
                    SoundManager.instance.PlaySFX(clip, randomNum, 1, 1);
                    dragonflyStick.gameObject.SetActive(false);
                    StartCoroutine(SprayTouch());
                }
            }
        }

    }
    /// <summary>
    /// 스프레이 터치시 On Off
    /// </summary>
    /// <returns></returns>
    IEnumerator SprayTouch()
    {
        spray.gameObject.SetActive(true);
        youCanTouch = false;
        yield return new WaitForSeconds(1);
        youCanTouch = true;
        spray.gameObject.SetActive(false);
    }
    /// <summary>
    /// 스프레이 공격시 나오는 함수
    /// </summary>
    public void SprayButton()//스프레이 버튼
    {
        dragonflyButton.gameObject.SetActive(true);
        sprayButton.gameObject.SetActive(false);
        spray.gameObject.SetActive(false);
        dragonflyStick.gameObject.SetActive(false) ;
        isDragonflyBT = false;
        isSprayBT = true;
    }
    /// <summary>
    /// 잠자리체 사용시 나오는 함수
    /// </summary>
    public void DragonFlyStick()//잠자리채 버튼
    {
        dragonflyButton.gameObject.SetActive(false);
        sprayButton.gameObject.SetActive(true);
        spray.gameObject.SetActive(false);
        dragonflyStick.gameObject.SetActive(false);
        isDragonflyBT = true;
        isSprayBT = false;
    }

}
