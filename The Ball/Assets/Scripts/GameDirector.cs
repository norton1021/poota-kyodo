using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // ゲームの状態
    string mode = "menu";
    // ステージプレイ時の状態
    string status = "playing";

    [SerializeField, Header("プレイヤーの残機")]
    int playerLives = 5;

    [SerializeField, Header("ステージごとの制限時間（秒）")]
    float[] timeLimits = new float[11];

    [SerializeField, Header("ステージごとのカメラモード")]
    string[] stageCameraMode = new string[11];
    
    // UIのオブジェクト
    GameObject canvas;
    // ステージ情報UIのオブジェクト
    GameObject gameInformation;
    // ステージ情報UIのオブジェクト
    GameObject stageInformation;
    // カメラのオブジェクト
    GameObject mainCamera;

    // スコア
    int score = 0;
    // ステージ番号で管理するための変数（0はタイトル画面）
    int stageVariable = 0;
    // タイマー
    float time = 0;
    // シーン数のカウント
    int scenes = 0;

    // （デバッグ用）ステージ番号入力場所
    TMP_InputField IF;
    
    void Start()
    {
        this.mode = "menu";
        this.status = "playing";
        this.canvas = GameObject.Find("CanvasPrefab");
        this.gameInformation = GameObject.Find("GameInformation");
        this.stageInformation = GameObject.Find("StageInformation");
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.score = 0;
        this.stageVariable = 0;
        this.time = 0;
        this.mainCamera.GetComponent<CameraController>().cameraMode = this.stageCameraMode[stageVariable];
        this.mainCamera.GetComponent<CameraController>().prePlayerPos = new Vector3(0, 0, -10);
        this.scenes = SceneManager.sceneCount;
        this.IF = GameObject.Find("InputStageNumber").GetComponent<TMP_InputField>();

    }

    void Update()
    {
        // メニュー時
        if (this.mode == "menu")
        {
            //this.stageInformation.GetComponent<TextMeshProUGUI>().text =
            //    "Enter numbers from 1 to " + (this.scenes - 1);

            // （デバッグ用）ステージ番号を入力する
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                // ステージ番号を1に
                this.stageVariable = 1;
                
                // ゲームの状態をゲームプレイ時に設定
                this.mode = "game";

                // 制限時間をステージ番号のものに設定
                this.time = this.timeLimits[stageVariable];

                // カメラモードをステージ番号のものに設定
                this.mainCamera.GetComponent<CameraController>().cameraMode = this.stageCameraMode[stageVariable];

                // ステージプレイ時の状態をステージプレイ中に設定
                this.status = "playing";

                // いくつかのオブジェクトを削除せずにステージ番号にシーン遷移
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(this.mainCamera);
                DontDestroyOnLoad(this.canvas);
                SceneManager.LoadScene(stageVariable);
                // Debug.Log("a");
                // this.IF.onEndEdit.AddListener(OnEnterInputField);
            }
        }
        // ゲームプレイ時
        else if (this.mode == "game")
        {
            GameInformation();

            Timer();

            StageInformation();

            SceneTransition();
        }
    }

    // （デバッグ用）ステージ番号を入力する
    void OnEnterInputField(string inputMsg)
    {
        if (int.TryParse(inputMsg, out this.stageVariable))
        {
            if (this.stageVariable >= 1 &&
            this.stageVariable <= this.scenes)
            {
                // ゲームの状態をゲームプレイ時に設定
                this.mode = "game";

                // 制限時間をステージ番号のものに設定
                this.time = this.timeLimits[stageVariable];

                // カメラモードをステージ番号のものに設定
                this.mainCamera.GetComponent<CameraController>().cameraMode = this.stageCameraMode[stageVariable];

                // ステージプレイ時の状態をステージプレイ中に設定
                this.status = "playing";

                // いくつかのオブジェクトを削除せずにステージ番号にシーン遷移
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(this.mainCamera);
                DontDestroyOnLoad(this.canvas);
                SceneManager.LoadScene(stageVariable);
            }
            else
            {
                Debug.Log("1から" + this.scenes + "までの数字を入力してね");
            }
        }
        else
        {
            Debug.Log("入力が無効です；；");
        }
    }

    // ゲーム情報の更新
    void GameInformation()
    {
        // もしUIが存在しなければ処理しない
        if (this.canvas == null ||
            this.gameInformation == null)
        {
            return;
        }

        // スコアとプレイヤーの残機を表示
        this.gameInformation.GetComponent<TextMeshProUGUI>().text =
            "Score: " + this.score + "\n" +
            "Life Count: " + this.playerLives;
    }

    // 時間を減らす
    void Timer()
    {
        // ステージプレイ中のみ動くようにする
        if (this.status == "playing")
        {
            this.time -= Time.deltaTime;

            // 時間切れ（0秒）でミス
            if (this.time <= 0)
            {
                Miss();
            }
        }
    }

    // ステージ情報の更新
    void StageInformation()
    {
        // もしUIが存在しなければ処理しない
        if (this.canvas == null ||
            this.stageInformation == null)
        {
            return;
        }

        // ミスしたとき
        if (this.status == "miss")
        {
            this.stageInformation.GetComponent<TextMeshProUGUI>().text =
                "Stage " + this.stageVariable + "\n" +
                "Time: " + this.time.ToString("F1") + "\n" +
                "Miss!\n" +
                "Press the R Key";
        }
        // 時間切れのとき
        else if (this.status == "timesup")
        {
            this.stageInformation.GetComponent<TextMeshProUGUI>().text =
                "Stage " + this.stageVariable + "\n" +
                "Time: " + this.time.ToString("F1") + "\n" +
                "Time's up!\n" +
                "Press the R Key";
        }
        // クリアしたとき
        else if (this.status == "clear")
        {
            this.stageInformation.GetComponent<TextMeshProUGUI>().text =
                "Stage " + this.stageVariable + "\n" +
                "Time: " + this.time.ToString("F1") + "\n" +
                "Cleared!\n" +
                "Press the R Key";
        }
        // ゲームオーバーのとき
        else if (this.status == "gameover")
        {
            this.stageInformation.GetComponent<TextMeshProUGUI>().text =
                "Stage " + this.stageVariable + "\n" +
                "Time: " + this.time.ToString("F1") + "\n" +
                "Game over...\n" +
                "Press the R Key";
        }
        // ステージプレイ時
        else
        {
            this.stageInformation.GetComponent<TextMeshProUGUI>().text =
                "Stage " + this.stageVariable + "\n" +
                "Time: " + this.time.ToString("F1");
        }
    }
    
    // ミスしたときの処理
    public void Miss()
    {
        // ステージプレイ中以外で呼び出されないように返す
        if (this.status != "playing")
        {
            return;
        }

        // プレイヤーを削除
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        // プレイヤーの残機を1減少
        this.playerLives -= 1;

        // もし残機が0ならばゲームオーバー
        if (this.playerLives <= 0)
        {
            GameOver();
        }

        // ゲームオーバーではないとき
        if (this.status != "gameover")
        {
            // ミスしたか時間切れか
            if (this.time > 0)
            {
                this.status = "miss";
            }
            else
            {
                this.status = "timesup";
            }
        }
    }

    // ステージクリアの処理
    public void Clear()
    {
        // ステージプレイ中以外で呼び出されないように返す
        if (this.status != "playing")
        {
            return;
        }

        // プレイヤーを削除
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        this.status = "clear";

        // クリア時の残り時間をスコアに変換
        this.score += (int)(this.time * 1000);
    }

    // ゲームオーバーの処理
    void GameOver()
    {
        this.status = "gameover";

        // 結果を表示
    }

    void SceneTransition()
    {
        // ミスまたは時間切れのとき
        if (this.status == "miss" ||
            this.status == "timesup")
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                // 制限時間をリセット
                this.time = this.timeLimits[stageVariable];

                // カメラモードをリセット
                this.mainCamera.GetComponent<CameraController>().cameraMode = this.stageCameraMode[stageVariable];

                // ステージプレイ時の状態をステージプレイ中にリセット
                this.status = "playing";

                // いくつかのオブジェクトを削除せずにシーン遷移
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(this.mainCamera);
                DontDestroyOnLoad(this.canvas);
                SceneManager.LoadScene(this.stageVariable);
            }
        }
        // クリアしたとき
        else if (this.status == "clear")
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                // ステージ番号を1増加
                this.stageVariable += 1;

                // 制限時間を次のステージのものに更新
                this.time = this.timeLimits[stageVariable];

                // カメラモードを次のステージのものに更新
                this.mainCamera.GetComponent<CameraController>().cameraMode = this.stageCameraMode[stageVariable];

                // ステージプレイ時の状態をステージプレイ中にリセット
                this.status = "playing";

                // いくつかのオブジェクトを削除せずにシーン遷移
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(this.mainCamera);
                DontDestroyOnLoad(this.canvas);
                SceneManager.LoadScene(this.stageVariable);
            }
        }
        // ゲームオーバーのとき
        else if (this.status == "gameover")
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                // タイトル画面へ
                Destroy(gameObject);
                Destroy(this.mainCamera);
                Destroy(this.canvas);
                SceneManager.LoadScene(0);
            }
        }
    }
}
