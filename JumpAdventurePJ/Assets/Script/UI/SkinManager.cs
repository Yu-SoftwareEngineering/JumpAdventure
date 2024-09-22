using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;
    public int choosenSkinId;
    
    private void Awake()
    {
        // Scene 이동시 파괴되지 않음
        DontDestroyOnLoad(this.gameObject);

        // 오브젝트가 2개이상이 되는것 방지
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // UI_SkinSelection.cs에서 currentIndex를 받아올때 사용
    public void SetSkinId(int id) => choosenSkinId = id;

}
