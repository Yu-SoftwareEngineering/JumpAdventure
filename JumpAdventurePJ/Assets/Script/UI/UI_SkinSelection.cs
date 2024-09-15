using UnityEngine;

public class UI_SkinSelection : MonoBehaviour
{

    [SerializeField] private int currentIndex;
    [SerializeField] private int maxIndex;
    [SerializeField] private Animator skinDisplay;

    // 레이어 가중치 설정 함수
    private void UpdateSkinDisplay()
    {
        for (int i = 0; i <= maxIndex; i++)
        {
            skinDisplay.SetLayerWeight(i, 0);
        }

        skinDisplay.SetLayerWeight(currentIndex, 1);
    }

    // 오른쪽 ">" 버튼
    public void NextSkin()
    {
        currentIndex++;

        if (currentIndex > maxIndex)
            currentIndex = 0;

        UpdateSkinDisplay();
    }

    // 왼쪽 "<" 버튼 
    public void PreviousSkin()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = maxIndex;

        UpdateSkinDisplay();
    }

    // Select 버튼 클릭시, 현재 스킨 인덱스인 currentIndex를 SkinManager로 전달
    public void SelectSkin()
    {
        SkinManager.instance.SetSkinId(currentIndex);
    }

}
