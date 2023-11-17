using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PawnScoreProcessor : MonoBehaviour {
    private float _spawnPosition;
    private float _currentPosition;
    
    private void Start() {
        ScoreData.ResetData();

        _spawnPosition = transform.position.y;
    }

    private void Update() {
        _currentPosition = transform.position.y;
        float distance = _spawnPosition - _currentPosition;
        
        // Set distance, distance is an absolute value
        ScoreData.TraveledHeight = Mathf.Abs(distance);
        
        CheckFuelCount();
        CheckHitpoint();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("ScoreFuel")) {
            ScoreData.FuelTanks += 1;
            ScoreData.Fuel += 25.0f;
            
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("ScoreForbid")) {
            ScoreData.Hitpoint -= 10.0f;
            
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Triggers game over when player is at the highest point or gone out of bound
        if (other.gameObject.CompareTag("GameArea") || other.gameObject.CompareTag("OutOfBounds")) {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void CheckHitpoint() {
        if (ScoreData.Hitpoint <= 0.0f) {
            // Do something when it's supposed to be dead
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void CheckFuelCount() {
        if (ScoreData.Fuel <= 0.0f) {
            // Do something when it's supposed to be done when out of fuel
            SceneManager.LoadScene("GameOverScene");
        }
    }
}

// Class with static data that is accessible without instantiating
public class ScoreData {
    public static float Hitpoint;
    public static float Fuel;
    public static int FuelTanks;
    public static float TraveledHeight;

    public static void ResetData() {
        Hitpoint = 100.0f;
        Fuel = 125.0f;
        FuelTanks = 0;

        TraveledHeight = 0.0f;
    }
}