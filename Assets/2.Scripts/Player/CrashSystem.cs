using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CrashSystem : MonoBehaviour
{
    //----------------------TouchInfo--------------------------
    float                       MaxDistance = 15;//�����ɽ�Ʈ ��Ÿ�
    Vector3                     mousePos;//���콺 ��ġ ����
    float                       moveSpeed=15;
    int                         layerMask = 1 << 8;
    //----------------------PlayerWeapon--------------------------
    Transform                   dragonflyStick;//���ڸ�ä
    Transform                   spray;//������
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
    bool                        isDragonflyBT;//���ڸ�ä ��ư�� Ŭ���� �������� Ȯ��
    bool                        isSprayBT;//������ ��ư�� Ŭ���� �������� Ȯ��
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
            if (collision.CompareTag("Right"))//�����̵�
            {
                Vector3 moveMent = Vector3.right * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            if (collision.CompareTag("Left"))//���� �̵�
            {
                Vector3 moveMent = Vector3.left * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            if (collision.CompareTag("Down"))//�Ʒ� �̵�
            {
                Vector3 moveMent = Vector3.down * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            if (collision.CompareTag("Up"))//�����̵�
            {
                Vector3 moveMent = Vector3.up * moveSpeed * Time.deltaTime;
                cam.position += moveMent;
            }
            float x = Mathf.Clamp(cam.position.x, -11, 11);//�¿찪 ����
            float y = Mathf.Clamp(cam.position.y, -16, 8);//���Ʒ� ����
            cam.position= new Vector3(x,y, -1);
        }
    }
    private void Update()
    {
        PlayerControl();
    }
    /// <summary>
    /// ��ġ �� �ۿ��ϴ� �Լ�
    /// </summary>
    void PlayerControl()
    {
        mousePos = Input.mousePosition;//��ġ�Ѱ��� ���� ����
        if (!MiniGameManager.isMiniGameStart)
        {
            ItemMove();
        }
        if(MiniGameManager.isMiniGameStart)
        {
            mousePos = camMini.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            if (Input.GetMouseButton(0))//���콺 ��Ŭ�� �� ��ġ
            {
                col.enabled = true;
            }
        }
        #region �ּ�
        /*if (Tutorial.isTutorial)
        {
            if (!EventSystem.current.IsPointerOverGameObject())//��ư ��ġ�� �ٸ� �̺�Ʈ�� ���Ͼ�� ���ǹ�
            {
                mousePos = tutorialCam.GetComponent<Camera>().ScreenToWorldPoint(mousePos);//������ ���� ȭ�鿡 ���� ��ġ�� ����
                if (Input.GetMouseButton(0))//���콺 ��Ŭ�� �� ��ġ
                {
                    col.enabled = true;
                    if (isDragonflyBT)//���ڸ�ä ��ư ��ġ��
                    {
                        spray.gameObject.SetActive(false);
                        dragonflyStick.gameObject.SetActive(true);
                    }
                }
                if (Input.GetMouseButtonDown(0) && youCanTouch)
                {
                    col.enabled = true;
                    if (isSprayBT)//�������� ��ư ��ġ�� 
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
        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, MaxDistance,layerMask);//�����ɽ�Ʈ �߻� ����
        if(hit.collider != null)
        {
            if (!hit.collider.CompareTag("UI"))
            {
                transform.position = hit.point;//������Ʈ�� ��ġ�� ��ġ�Ѱ����� ����
            }
            
            if (Input.GetMouseButtonUp(0)|| hit.collider.CompareTag("UI"))//���콺 �� ��ġ�� �����ʾ����� ���� �ʰ�
            {
                dragonflyStick.gameObject.SetActive(false);
                //spray.gameObject.SetActive(false);
                col.enabled = false;
            }
        }
    }
    /// <summary>
    /// ���� �����̰� �ϰ� �ۿ��ϰ� �ϴ� �Լ�
    /// </summary>
    void ItemMove()//���콺 �̵�����Ʈ�� ������ �̵��ϴ� �Լ�
    {
        if (!EventSystem.current.IsPointerOverGameObject())//��ư ��ġ�� �ٸ� �̺�Ʈ�� ���Ͼ�� ���ǹ�
        {
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);//������ ���� ȭ�鿡 ���� ��ġ�� ����
            if (Input.GetMouseButton(0))//���콺 ��Ŭ�� �� ��ġ
            {
                col.enabled = true;
                if (isDragonflyBT)//���ڸ�ä ��ư ��ġ��
                {
                    spray.gameObject.SetActive(false);
                    dragonflyStick.gameObject.SetActive(true);
                }
            }
            if (Input.GetMouseButtonDown(0) && youCanTouch)
            {
                col.enabled = true;
                if (isSprayBT)//�������� ��ư ��ġ�� 
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
    /// �������� ��ġ�� On Off
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
    /// �������� ���ݽ� ������ �Լ�
    /// </summary>
    public void SprayButton()//�������� ��ư
    {
        dragonflyButton.gameObject.SetActive(true);
        sprayButton.gameObject.SetActive(false);
        spray.gameObject.SetActive(false);
        dragonflyStick.gameObject.SetActive(false) ;
        isDragonflyBT = false;
        isSprayBT = true;
    }
    /// <summary>
    /// ���ڸ�ü ���� ������ �Լ�
    /// </summary>
    public void DragonFlyStick()//���ڸ�ä ��ư
    {
        dragonflyButton.gameObject.SetActive(false);
        sprayButton.gameObject.SetActive(true);
        spray.gameObject.SetActive(false);
        dragonflyStick.gameObject.SetActive(false);
        isDragonflyBT = true;
        isSprayBT = false;
    }

}
