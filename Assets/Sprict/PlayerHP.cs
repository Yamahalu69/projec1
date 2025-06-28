// PlayerHP.cs

using UnityEngine;
using UnityEngine.UI; // UIテキストなどでHPを表示する場合に備えて追加

public class PlayerHP : MonoBehaviour
{
    // このスクリプトの唯一のインスタンスを保持する静的変数（シングルトン）
    public static PlayerHP Instance { get; private set; }

    // 【ご要望通り】HPを格納するprivate変数
    private int Player_HP;

    // Inspectorで設定できる最大HP
    [SerializeField]
    private int maxHp = 100;

    // 他のスクリプトから現在のHPを安全に読み取るためのプロパティ
    public int CurrentHp
    {
        get { return Player_HP; }
    }

    void Awake()
    {
        // シングルトンパターンの実装
        // もしインスタンスがまだなければ、このインスタンスを登録
        if (Instance == null)
        {
            Instance = this;
            // シーンをまたいでもHP管理オブジェクトが破壊されないようにしたい場合は下のコメントを外す
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // すでにインスタンスが存在する場合は、このオブジェクトを破棄する
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ゲーム開始時にHPを最大値に設定
        Player_HP = maxHp;
        Debug.Log("プレイヤーのHPが初期化されました。現在のHP: " + Player_HP);
    }

    // ダメージを受けるための公開メソッド
    public void TakeDamage(int damageAmount)
    {
        // HPからダメージ量を引く
        Player_HP -= damageAmount;

        Debug.Log(damageAmount + "のダメージを受けた！ 残りHP: " + Player_HP);

        // HPが0以下になった場合の処理
        if (Player_HP <= 0)
        {
            Player_HP = 0; // HPがマイナスにならないように0で固定
            Die();
        }
    }

    // HPが0になったときの処理
    private void Die()
    {
        Debug.Log("プレイヤーは力尽きた...。");
        // ここにゲームオーバー処理などを記述します
        // 例: Time.timeScale = 0; // ゲームを停止する
    }
}