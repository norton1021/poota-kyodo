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

    [System.Serializable]
    public class StageSettings
    {
        public string sceneName;
        public float timeLimits;
        public string stageCameraMode;
        public Vector3 stageCameraOffset;
    }

    [SerializeField, Header("ステージごとの制限時間とカメラの設定")]
    StageSettings[] stageSettings = new StageSettings[11];
    
    // UIのオブジェクト
    GameObject canvas;
    // ステージ情報UIのオブジェクト
    GameObject gameInformation;
    // ステージ情報UIのオブジェクト
    GameObject stageInformation;
    // カメラのオブジェクト
    GameObject mainCamera;
    // サウンドマネージャーのオブジェクト
    GameObject soundManager;

    // クリア時の獲得ポイント
    int point = 0;
    // スコア
    int score = 0;
    // 次にライフを増やすスコアの閾値
    int nextLifeScoreThreshold = 0;
    // 増加したライフの数
    int increasedLives = 0;
    // ライフが増加したことを伝える
    bool lifeIncreased = false;

    // ステージ番号で管理するための変数（0はタイトル画面）
    int stageVariable = 0;
    // タイマー
    float time = 0;
    // シーン数のカウント
    int scenes = 0;

    // （デバッグ用）ステージ番号入力場所
    TMP_InputField inputField;
    
    void Start()
    {
        this.mode = "menu";
        this.status = "playing";
        this.canvas = GameObject.Find("CanvasPrefab");
        this.gameInformation = GameObject.Find("GameInformation");
        this.stageInformation = GameObject.Find("StageInformation");
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.soundManager = GameObject.Find("SoundManagerPrefab");
        this.point = 0;
        this.score = 0;
        this.nextLifeScoreThreshold = 30000;
        this.increasedLives = 0;
        this.lifeIncreased = false;
        this.stageVariable = 0;
        this.time = 0;
        this.mainCamera.GetComponent<CameraController>().cameraMode = stageSettings[stageVariable].stageCameraMode;
        this.mainCamera.GetComponent<CameraController>().prePlayerPos = new Vector3(0, 0, -10);
        this.scenes = SceneManager.sceneCountInBuildSettings;

        // （デバッグ用）inputFieldを有効化
        this.inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>();
        this.inputField.onEndEdit.AddListener(OnEnterInputField);
    }

    void Update()
    {
        // メニュー時
       // if (this.mode == "menu")
       // {
            //this.gameInformation.GetComponent<TextMeshProUGUI>().text =
              //  "The Ball";
            //this.stageInformation.GetComponent<TextMeshProUGUI>().text =
              //  "Enter numbers from 1 to " + (this.scenes - 1);

            // （デバッグ用）inputFieldを選択状態にする
            if (this.inputField != null)
            {
                this.inputField.Select();
            }
      //  }
        // ゲームプレイ時
        else if (this.mode == "game")
        {
            SceneTransition();

            Timer();

            StageInformation();

            GameInformation();
        }
    }

    // （デバッグ用）ステージ番号を入力する
    void OnEnterInputField(string inputMsg)
    {
        this.inputField.DeactivateInputField();
        if (int.TryParse(inputMsg, out this.stageVariable))
        {
            if (this.stageVariable >= 1 &&
                this.stageVariable <= this.scenes - 1)
            {

                // 「決定」を鳴らす
                this.soundManager.GetComponent<SoundManager>().PlayTheSound("決定");

                // ゲームの状態をゲームプレイ時に設定
                this.mode = "game";

                ResetStatus();
            }
            else
            {
                // 「エラー」を鳴らす
                this.soundManager.GetComponent<SoundManager>().PlayTheSound("エラー");

                Debug.Log("1から" + (this.scenes - 1) + "までの数字を入力してね");
                this.inputField.ActivateInputField();
            }
        }
        else
        {
            // 「エラー」を鳴らす
            this.soundManager.GetComponent<SoundManager>().PlayTheSound("エラー");

            Debug.Log("入力が無効です");
            this.inputField.ActivateInputField();
        }
    }

    // ゲームの状態ごとのシーンの遷移方法
    void SceneTransition()
    {
        // （デバッグ用）リセット用コマンド
        if (this.mode == "game" &&
            (Keyboard.current != null &&
            Keyboard.current.qKey.wasPressedThisFrame))
        {
            this.stageVariable = 0;
            this.soundManager.GetComponent<SoundManager>().PlayTheBGM("stop");
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(this.canvas, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(this.mainCamera, SceneManager.GetActiveScene());
            SceneManager.LoadScene("TitleScene");
        }

        // ミスまたは時間切れのとき
        if (this.status == "miss" ||
            this.status == "timesup")
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                ResetStatus();
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

                // ライフの情報をリセット
                this.increasedLives = 0;
                this.lifeIncreased = false;

                // 最後のステージに到達したらタイトルシーンへ
                if (this.stageVariable > this.scenes - 1)
                {
                    this.stageVariable = 0;
                    this.soundManager.GetComponent<SoundManager>().PlayTheBGM("stop");
                    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
                    SceneManager.MoveGameObjectToScene(this.canvas, SceneManager.GetActiveScene());
                    SceneManager.MoveGameObjectToScene(this.mainCamera, SceneManager.GetActiveScene());
                    SceneManager.LoadScene("TitleScene");
                }
                else
                {
                    ResetStatus();
                }
            }
        }
        // ゲームオーバーのとき
        else if (this.status == "gameover")
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                // タイトルシーンへ
                this.stageVariable = 0;
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("stop");
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
                SceneManager.MoveGameObjectToScene(this.canvas, SceneManager.GetActiveScene());
                SceneManager.MoveGameObjectToScene(this.mainCamera, SceneManager.GetActiveScene());
                SceneManager.LoadScene("TitleScene");
            }
        }
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
        if (this.status == "clear")
        {
            if (this.lifeIncreased)
            {
                this.gameInformation.GetComponent<TextMeshProUGUI>().text =
                    "Score: " + this.score + "  +" + this.point + "pts!\n" +
                    "Life Count: " + this.playerLives + "  +" + this.increasedLives;
            }
            else
            {
                this.gameInformation.GetComponent<TextMeshProUGUI>().text =
                    "Score: " + this.score + "  +" + this.point + "pts!\n" +
                    "Life Count: " + this.playerLives;
            }
        }
        else
        {
            this.gameInformation.GetComponent<TextMeshProUGUI>().text =
                "Score: " + this.score + "\n" +
                "Life Count: " + this.playerLives;
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

        // 「ミス」を鳴らす
        this.soundManager.GetComponent<SoundManager>().PlayTheSound("ミス");

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

        // クリア時の残り時間を獲得ポイントに変換
        this.point = (int)(this.time * 1000);
        // ポイントをスコアに合算
        this.score += this.point;

        // 30000ptsごとにライフを増やす
        while (this.score >= this.nextLifeScoreThreshold)
        {
            this.increasedLives++;
            this.nextLifeScoreThreshold += 30000;
        }

        if (this.increasedLives >= 1)
        {
            this.playerLives += this.increasedLives;
            this.lifeIncreased = true;
        }

        // 制限時間に対する残り時間の割合でファンファーレを変える
        if (this.time / stageSettings[this.stageVariable].timeLimits < 0.2f)
        {
            this.soundManager.GetComponent<SoundManager>().PlayTheSound("クリア1");
        }
        else if (this.time / stageSettings[this.stageVariable].timeLimits < 0.4f)
        {
            this.soundManager.GetComponent<SoundManager>().PlayTheSound("クリア2");
        }
        else
        {
            this.soundManager.GetComponent<SoundManager>().PlayTheSound("クリア3");
        }
    }

    // ゲームオーバーの処理
    void GameOver()
    {
        this.status = "gameover";

        // 結果を表示
    }

    // シーン遷移時の各項目のリセット
    void ResetStatus()
    {
        // 時間やカメラが未設定ならタイトルシーンへ
        if (stageSettings[stageVariable].timeLimits <= 0 ||
            stageSettings[stageVariable].stageCameraMode == null)
        {
            // タイトルシーンへ
            this.stageVariable = 0;
            this.soundManager.GetComponent<SoundManager>().PlayTheBGM("stop");
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(this.canvas, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(this.mainCamera, SceneManager.GetActiveScene());
            SceneManager.LoadScene("TitleScene");
            return;
        }

        // ステージに応じてBGMを再生
        switch ((stageVariable - 1) / 4)
        {
            case 0:
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("StageBGM1");
                break;
            case 1:
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("StageBGM2");
                break;
            case 2:
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("StageBGM3");
                break;
            case 3:
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("StageBGM4");
                break;
            case 4:
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("StageBGM5");
                break;
            default:
                this.soundManager.GetComponent<SoundManager>().PlayTheBGM("stop");
                break;
        }

        // いくつかのオブジェクトを削除せずにシーン遷移
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this.mainCamera);
        DontDestroyOnLoad(this.canvas);
        DontDestroyOnLoad(this.soundManager);
        SceneManager.LoadScene(this.stageVariable);

        // カメラモードを更新
        this.mainCamera.GetComponent<CameraController>().cameraMode = stageSettings[stageVariable].stageCameraMode;
        this.mainCamera.GetComponent<CameraController>().cameraOffset = stageSettings[stageVariable].stageCameraOffset;

        // ステージプレイ時の状態をステージプレイ中にリセット
        this.status = "playing";

        // 制限時間を更新
        this.time = stageSettings[stageVariable].timeLimits;
    }
}