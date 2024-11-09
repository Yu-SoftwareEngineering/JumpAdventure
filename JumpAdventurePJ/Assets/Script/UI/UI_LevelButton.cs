using TMPro;
using UnityEngine;

public class UI_LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI fruitsText;

    private int levelIndex;

    public void SetupButton(int _levelIndex)
    {
        levelIndex = _levelIndex;
        levelNumberText.text = "Level " + levelIndex;
        bestTimeText.text = SetupTimerText();
        fruitsText.text = SetupFruitsText();

    }

    // TimerText 설정

    private string SetupTimerText()
    {
        float timerValue = PlayerPrefs.GetFloat("Level_" + levelIndex + "BestTime", 999);
        Debug.Log(timerValue); // 정상적으로 출력됨
        return "Best Time : " + timerValue.ToString("00") + " s";
    }


    // FruitsText 설정

    private string SetupFruitsText()
    {
        int totalFruits = PlayerPrefs.GetInt("Level_" + levelIndex + "TotalFruits", 0);
        int fruitsCollected = PlayerPrefs.GetInt("Level_" + levelIndex + "FruitsCollected");
        return "Fruits : " + fruitsCollected + "/" + totalFruits;
    }


    void Start()
    {

    }

    void Update()
    {

    }
}
