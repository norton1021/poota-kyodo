using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // ステージ番号
    int stage = 1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Clear()
    {
        // ステージ番号を1増加
        this.stage += 1;

        // このオブジェクトを削除せずにシーン遷移
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(this.stage);
    }
}
