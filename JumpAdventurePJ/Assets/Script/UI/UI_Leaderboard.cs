using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Leaderboard : MonoBehaviour
{
    [SerializeField] private UI_LevelButton buttonPrefab;
    [SerializeField] private Transform buttonsParent;

    [SerializeField] private bool[] levelsUnlocked;

    private void Start()
    {
        LoadLevelsInfo();
        CreateButton();
    }

    private void CreateButton()
    {
	    int levelsAmount = SceneManager.sceneCountInBuildSettings - 2;

	    for (int i = 1; i <= levelsAmount; i++)
	    {
            if (levelsUnlocked [ i ] == false)
            {
                return;
            }
		    UI_LevelButton newButton = Instantiate(buttonPrefab, buttonsParent);		
		    newButton.SetupButton(i);
	    }
    }   

    private void LoadLevelsInfo()
    {
	    int levelsAmount = SceneManager.sceneCountInBuildSettings - 2;

	    levelsUnlocked = new bool [levelsAmount + 1]; 

	    for (int i = 1; i <= levelsAmount; i++)
	    {
		    // PlayerPrefs에 저장한 정보 불러오기 
		    bool levelUnlocked = PlayerPrefs.GetInt("Level_" + i + "Unlocked", 0) == 1;

		    if (levelUnlocked)
            {
                levelsUnlocked [ i ] = true;
            }
			    
	    }
    }

    void Update()
    {
        
    }
}
