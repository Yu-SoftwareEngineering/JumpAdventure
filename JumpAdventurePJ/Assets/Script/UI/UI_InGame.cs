using UnityEngine;
using TMPro;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    public UI_FadeEffect fadeEffect;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(instance);
        }
        
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1f);
    }
    
    public void UpdateFruitUI (int _collectedFruits , int _totalFruits)
    {
	    fruitText.text = _collectedFruits + "/" + _totalFruits;
    }

    public void UpdateTimerUI (float timer)
    {
	    timerText.text = timer.ToString("00") + " s";
    }
}
