using UnityEngine;

// Self-explanatory
public class HideOnStart : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        gameObject.SetActive(false);
    }
}