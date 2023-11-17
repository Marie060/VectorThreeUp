using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawn : MonoBehaviour {
    [Header("Spawn Configuration")] public GameObject platformPrefab;
    public Transform spawnParent;
    public int spawnCount = 512;
    public float overlapSafeRadius = 8.0f;

    [Header("Item Spawn Configuration")] public GameObject fuelPrefab;
    public GameObject forbidPrefab;

    private BoxCollider2D bound;

    private Vector2 mininalBound;
    private Vector2 maximalBound;

    // Start is called before the first frame update
    private void Awake() {
        SetupBounds();
        SpawnPlatforms();
    }

    private void SetupBounds() {
        bound = GetComponent<BoxCollider2D>();

        var bounds = bound.bounds;
        mininalBound = new Vector2(bounds.min.x, bounds.min.y);
        maximalBound = new Vector2(bounds.max.x, bounds.max.y);

        // We don't need the BoxCollider2D anymore, so delete it
        Destroy(bound);
        // Except we need it to check if player is outside play area or not
        // bound.isTrigger = true;
    }

    private void SpawnPlatforms() {
        for (int i = 0; i <= spawnCount; i++) {
            Vector2 pos = new Vector2(
                Random.Range(mininalBound.x, maximalBound.x),
                Random.Range(mininalBound.y, maximalBound.y)
            );

            // Spawn the platform itself
            var p = Instantiate(platformPrefab, spawnParent);
            p.transform.position = pos;

            // Check if this spawned platform happens to overlap with existing platform
            if (!CheckOverlaps(p.transform, overlapSafeRadius, "Platform")) {
                // Then spawn items on the platform
                SpawnPlatformItem(p.transform.GetChild(0));
            }
        }
    }

    // Returns true if it does overlap with something, returns false otherwise
    private bool CheckOverlaps(Transform point, float radius, string compareTag) {
        // Overlap check with (if any) existing platforms within radius
        Collider2D[] c = Physics2D.OverlapCircleAll(point.position, radius);
        if (c.Length > 0) {
            // Loop through if all overlapped colliders
            foreach (var t in c) {
                // Destroy this spawned platform if any of the collider happens to be another platform
                if (String.Compare(t.gameObject.tag, compareTag, StringComparison.Ordinal) != 0) {
                    Destroy(point.gameObject);
                    return true;
                }
                else {
                    return false;
                }
            }
            return false;
        }
        else {
            return false;
        }
    }

    private void SpawnPlatformItem(Transform parent) {
        int selector = Random.Range(0, 2);
        GameObject template = selector == 0 ? fuelPrefab : forbidPrefab;

        var i = Instantiate(template, parent);
        i.transform.SetParent(parent);
    }
}