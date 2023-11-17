using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform targetFollow;
    public float interpolation = 4.0f;

    // Update is called once per frame
    private void Update() {
        if (targetFollow) {
            var pos = targetFollow.position;
            Vector3 target = new Vector3(
                pos.x,
                pos.y,
                transform.position.z
            );

            transform.position = Vector3.Lerp(
                transform.position,
                target,
                interpolation * Time.deltaTime
            );
        }
    }
}