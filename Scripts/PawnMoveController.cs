using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PawnMoveController : MonoBehaviour {
    [Header("Component References")] public Camera mainCamera;
    public GameObject cursorObject;
    public GameObject fireSprite;

    [Header("Properties")] public float impulsePower = 32.0f;

    public float angularInterpolation = 64.0f;
    public float maxVelocity = 16.0f;

    private Rigidbody2D _physicsBody;

    private void Awake() {
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    private void Start() {
        fireSprite.SetActive(false);
        mainCamera = Camera.main;
        _physicsBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        // Visual feedback of where the cursor is;
        if (cursorObject) cursorObject.transform.position = GetCursorPosition();

        // Fire effect lmao
        fireSprite.SetActive(Input.GetMouseButton(0) && ScoreData.Fuel > 0.0f);

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            RotateTowards(transform.position, GetCursorPosition(), angularInterpolation);
        }
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(0) && ScoreData.Fuel > 0.0f) {
            ImpulseTowards(transform.position, GetCursorPosition(), impulsePower);
            
            // Consume fuel
            ScoreData.Fuel -= Time.deltaTime * 5.0f;
        }

        // Limit rigidbody's velocity, you shall not go sonic
        _physicsBody.velocity = Vector2.ClampMagnitude(_physicsBody.velocity, maxVelocity);
    }

    private Vector3 GetCursorPosition() {
        // Convert screen-space mouse position data into world-space position data
        return mainCamera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            mainCamera.nearClipPlane // Z is almost 0, only modify X and Y, this is a 2D game
        ));
    }

    // Movement by rigidbody's velocity modivication
    private void ImpulseTowards(Vector3 origin, Vector3 target, float power) {
        Vector3 direction = (target - origin).normalized;
        _physicsBody.velocity = -direction * power;
    }

    // Rotation quaternion
    private void RotateTowards(Vector3 origin, Vector3 target, float interpolation) {
        _physicsBody.angularVelocity = 0;
        
        Quaternion rot = Quaternion.LookRotation((origin - target).normalized, Vector3.back);
        _physicsBody.SetRotation(Quaternion.Lerp(
            transform.rotation,
            rot,
            interpolation * Time.deltaTime
        ));
    }

    // TODO: check is grounded by Rigidbody2D or BoxCollider2D contacts
    private bool IsGrounded() {
        
        return false;
    }
}