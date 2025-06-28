using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoppyPlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [Tooltip("プレイヤーの移動速度")]
    [SerializeField] private float moveSpeed = 5.0f;

    [Header("ジャンプ設定")]
    [Tooltip("プレイヤーのジャンプ力")]
    [SerializeField] private float jumpForce = 7.0f;
    [Tooltip("地面と判定するタグ")]
    [SerializeField] private string groundTag = "Ground";
    private bool isGrounded;

    [Header("Bacum（吸引機）設定")]
    [Tooltip("出現させるBacumオブジェクトのプレハブ")]
    [SerializeField] private GameObject bacumPrefab;
    [Tooltip("プレイヤーからのBacumの相対的な出現位置")]
    [SerializeField] private Vector3 bacumOffset = new Vector3(0, 0.5f, 1.5f);
    private GameObject currentBacumInstance;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ★変更点: マウス左クリックの状態に応じて処理を分岐
        // Input.GetMouseButton(0) は左クリックが押されている間 true を返す
        if (Input.GetMouseButton(0))
        {
            // --- クリックされている時の処理 ---
            // プレイヤーの水平移動を停止させる
            // これにより、クリックした瞬間にピタッと止まり、操作感が向上します。
            // Y軸の速度(rb.velocity.y)は維持することで、ジャンプ中の落下などは妨げません。
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
        else
        {
            // --- クリックされていない時の処理 ---
            // WASDキーによる移動処理を許可
            HandleMovement();

            // Spaceキーによるジャンプ処理を許可
            HandleJump();
        }

        // Bacumの出現/消滅処理は、クリック状態に関わらず毎フレームチェックする
        HandleBacum();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    /// <summary>
    /// Bacumの出現・消滅処理
    /// </summary>
    private void HandleBacum()
    {
        // マウス左クリックが押された瞬間の処理
        if (Input.GetMouseButtonDown(0))
        {
            if (bacumPrefab != null && currentBacumInstance == null)
            {
                Vector3 spawnPosition = transform.TransformPoint(bacumOffset);
                currentBacumInstance = Instantiate(bacumPrefab, spawnPosition, bacumPrefab.transform.rotation);
            }
        }
        // マウス左クリックが離された瞬間の処理
        else if (Input.GetMouseButtonUp(0))
        {
            if (currentBacumInstance != null)
            {
                Destroy(currentBacumInstance);
                currentBacumInstance = null;
            }
        }
    }

    /// <summary>
    /// 他のColliderに接触した瞬間に呼ばれる
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// 他のColliderから離れた瞬間に呼ばれる
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
}