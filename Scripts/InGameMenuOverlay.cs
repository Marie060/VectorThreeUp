using UnityEngine;

public class InGameMenuOverlay : MonoBehaviour {
    public GameObject menuOverlay;

    private bool _isMenuVisible = false;

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMenuOverlay();
        }
    }

    public void ToggleMenuOverlay() {
        _isMenuVisible = !_isMenuVisible;
        
        menuOverlay.SetActive(_isMenuVisible);
        Time.timeScale = _isMenuVisible ? 0.0f : 1.0f;
    }
}