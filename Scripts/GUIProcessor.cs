using UnityEngine;
using TMPro;

public class GUIProcessor : MonoBehaviour {
    public TextMeshProUGUI textHitpoint;
    public TextMeshProUGUI textTravelHeight;
    public TextMeshProUGUI textFuel;

    // Update is called once per frame
    private void Update() {
        textHitpoint.text = "Hitpoint\n" + ScoreData.Hitpoint.ToString("0.##");
        textTravelHeight.text = "Traveled distance\n" + ScoreData.TraveledHeight.ToString("0.##") + " m";
        textFuel.text = "Fuel\n" + ScoreData.Fuel.ToString("0.##");
    }
}