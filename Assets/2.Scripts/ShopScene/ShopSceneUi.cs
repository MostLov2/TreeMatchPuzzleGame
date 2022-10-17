using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSceneUi : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField]Transform middleCanvas;

    [Header("Button")]
    [SerializeField]Image buyButton;
    [SerializeField]Image upgradeButton;
    [SerializeField]Image itemButton;
    [SerializeField]Image gotchaButton;
    
    [Header("ButtonImage")]
    [SerializeField]Sprite buyOnButtonImage;
    [SerializeField]Sprite upgradeOnButtonImage;
    [SerializeField]Sprite itemOnButtonImage;
    [SerializeField]Sprite gotchaOnButtonImage;
    [SerializeField]Sprite buyOffButtonImage;
    [SerializeField]Sprite upgradeOffButtonImage;
    [SerializeField]Sprite itemOffButtonImage;
    [SerializeField]Sprite gotchaOffButtonImage;

    [Header("Panel")]
    [SerializeField] Transform ShopBuyPanel;
    [SerializeField] Transform ShopUpgradePanel;
    [SerializeField] Transform BuyItemPanel;
    [SerializeField] Transform BuyGotchaPanel;
    [SerializeField] Transform PersentagePanel;
    private void Awake()
    {
        middleCanvas = GameObject.FindGameObjectWithTag("MiddleCanvas").GetComponent<Transform>();

        buyButton = middleCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        upgradeButton = middleCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        itemButton = middleCanvas.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        gotchaButton = middleCanvas.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();

        ShopBuyPanel = middleCanvas.transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        ShopUpgradePanel = middleCanvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Transform>();
        BuyItemPanel = middleCanvas.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Transform>();
        BuyGotchaPanel = middleCanvas.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<Transform>();
        PersentagePanel = middleCanvas.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(2).GetComponent<Transform>();

        buyOnButtonImage = Resources.Load<Sprite>("ShopScene/button shop buy click");
        upgradeOnButtonImage = Resources.Load<Sprite>("ShopScene/button shop upgrade click");
        itemOnButtonImage = Resources.Load<Sprite>("ShopScene/button shop item click");
        gotchaOnButtonImage = Resources.Load<Sprite>("ShopScene/button shop gotcha click");
        buyOffButtonImage = Resources.Load<Sprite>("ShopScene/button shop buy");
        upgradeOffButtonImage = Resources.Load<Sprite>("ShopScene/button shop upgrade");
        itemOffButtonImage = Resources.Load<Sprite>("ShopScene/button shop item");
        gotchaOffButtonImage = Resources.Load<Sprite>("ShopScene/button shop gotcha");
    }
    public void ShopBuyButton()
    {
        buyButton.sprite = buyOnButtonImage;
        upgradeButton.sprite = upgradeOffButtonImage;
        ShopBuyPanel.gameObject.SetActive(true);
        ShopUpgradePanel.gameObject.SetActive(false);
    }
    public void ShopUpgradeButton()
    {
        buyButton.sprite = buyOffButtonImage;
        upgradeButton.sprite = upgradeOnButtonImage;
        ShopBuyPanel.gameObject.SetActive(false);
        ShopUpgradePanel.gameObject.SetActive(true);
    }
    public void ShopItemButton()
    {
        itemButton.sprite = itemOnButtonImage;
        gotchaButton.sprite = gotchaOffButtonImage;
        BuyItemPanel.gameObject.SetActive(true);
        BuyGotchaPanel.gameObject.SetActive(false);
    }
    public void ShopGotchaButton()
    {
        itemButton.sprite = itemOffButtonImage;
        gotchaButton.sprite = gotchaOnButtonImage;
        BuyItemPanel.gameObject.SetActive(false);
        BuyGotchaPanel.gameObject.SetActive(true);
    }
    public void ShopToLobbyButton()
    {
        SceneManager.LoadScene("LobbySceneRestart");
    }
    public void PersentagePanelOn()
    {
        PersentagePanel.gameObject.SetActive(true);
    }
    public void PersentagePanelOff()
    {
        PersentagePanel.gameObject.SetActive(false);
    }
}
