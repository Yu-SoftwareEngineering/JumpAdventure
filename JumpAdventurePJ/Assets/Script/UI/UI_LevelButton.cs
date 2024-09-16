using TMPro;
using UnityEngine;

public class UI_LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    
    private int levelIndex;

    public void SetupButton (int _levelIndex)
    {
	    levelIndex = _levelIndex;
	    levelNumberText.text = "Level " + levelIndex;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
