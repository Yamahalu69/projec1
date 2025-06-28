// DamageReceiver.cs

using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    // オブジェクトが他のコライダーと接触したときに呼び出されるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        // 接触した相手のタグが "Sword" または "Cutter" かどうかをチェック
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Cutter"))
        {
            Debug.Log(gameObject.name + " が " + collision.gameObject.name + " に接触しました。");

            // PlayerHPスクリプトのTakeDamageメソッドを呼び出して10ダメージを与える
            // PlayerHP.Instance が null でないことを確認するとより安全
            if (PlayerHP.Instance != null)
            {
                PlayerHP.Instance.TakeDamage(10);
            }
            else
            {
                Debug.LogError("PlayerHPのインスタンスが見つかりません！シーンにPlayerHPスクリプトを持つオブジェクトを配置してください。");
            }
        }
    }
}