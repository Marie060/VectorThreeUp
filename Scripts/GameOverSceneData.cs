using UnityEngine;
using TMPro;

public class GameOverSceneData : MonoBehaviour {
    public TextMeshProUGUI textFuelTanks;
    public TextMeshProUGUI textFuelLeft;
    public TextMeshProUGUI textDistanceTraveled;
    
    // Start is called before the first frame update
    void Start() {
        textFuelTanks.text = ScoreData.FuelTanks.ToString();
        textFuelLeft.text = ScoreData.Fuel.ToString("0.##");
        textDistanceTraveled.text = ScoreData.TraveledHeight.ToString("0.##");
    }
}