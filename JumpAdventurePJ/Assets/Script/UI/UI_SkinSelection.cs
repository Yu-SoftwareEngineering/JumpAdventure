using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkinSelection : MonoBehaviour
{

    [SerializeField] private int currentIndex;
    [SerializeField] private int maxIndex;
    [SerializeField] private Animator skinDisplay;
    [System.Serializable]
    public struct Skin
    {
        public string skinName;
        public int sKinPrice;
        public bool unlocked;
    }
    [SerializeField] private Skin[] skinList;
    [SerializeField] private Button button_Buy;
    [SerializeField] private Button button_Select;
    [SerializeField] private TextMeshProUGUI text_Price;
    [SerializeField] private TextMeshProUGUI text_Bank;

    private void Start()
    {
        LoadSkinSaveInfo();
        UpdateSkinDisplay();
    }

    private void Update()
    {
        text_Bank.text = "Bank : " + PlayerPrefs.GetInt("FruitsInBank").ToString("0");
    }

    // 저장된 스킨 구매 정보 불러오기
    private void LoadSkinSaveInfo()
    {
        for (int i = 0; i <= maxIndex; i++)
        {
            // 저장된 보유중 스킨 정보
            bool haveSkin = PlayerPrefs.GetInt(skinList[i].skinName + "Unlocked", 0) == 1;

            if (haveSkin == true)
            {
                skinList[i].unlocked = true;
            }
        }

        // 기본 스킨 활성화
        skinList[0].unlocked = true;
    }


    // 레이어 가중치 설정 함수
    private void UpdateSkinDisplay()
    {
        for (int i = 0; i <= maxIndex; i++)
        {
            skinDisplay.SetLayerWeight(i, 0);
        }

        skinDisplay.SetLayerWeight(currentIndex, 1);
        SwitchBuyUI();
    }

    private void SwitchBuyUI()
    {
        // 현재 보여지는 스킨 보유중
        if (skinList[currentIndex].unlocked == true)
        {
            text_Price.transform.parent.gameObject.SetActive(false);
            button_Buy.gameObject.SetActive(false);
            button_Select.gameObject.SetActive(true);
        }
        else
        {
            text_Price.transform.parent.gameObject.SetActive(true);
            text_Price.text = "Price : " + skinList[currentIndex].sKinPrice.ToString();
            button_Buy.gameObject.SetActive(true);
            button_Select.gameObject.SetActive(false);
        }
    }

    // 스킨 구매 
    public void BuySkin()
    {
        int money = PlayerPrefs.GetInt("FruitsInBank");
        int price = skinList[currentIndex].sKinPrice;

        // 보유중인 과일수가 가격만큼 있을시
        if (money >= price)
        {
            PlayerPrefs.SetInt("FruitsInBank", money - price);
            PlayerPrefs.SetInt(skinList[currentIndex].skinName + "Unlocked", 1);
            skinList[currentIndex].unlocked = true;
            SwitchBuyUI();
            AudioManager.instance.PlaySFX(11);
        }
    }


    // 오른쪽 ">" 버튼
    public void NextSkin()
    {
        currentIndex++;

        if (currentIndex > maxIndex)
            currentIndex = 0;

        UpdateSkinDisplay();
        AudioManager.instance.PlaySFX(11);
    }

    // 왼쪽 "<" 버튼 
    public void PreviousSkin()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = maxIndex;

        UpdateSkinDisplay();
        AudioManager.instance.PlaySFX(11);
    }

    // Select 버튼 클릭시, 현재 스킨 인덱스인 currentIndex를 SkinManager로 전달
    public void SelectSkin()
    {
        SkinManager.instance.SetSkinId(currentIndex);
    }


    public void ResetSkinIndex()
    {
        currentIndex = 0;
        UpdateSkinDisplay();
        SwitchBuyUI();
    }


}
