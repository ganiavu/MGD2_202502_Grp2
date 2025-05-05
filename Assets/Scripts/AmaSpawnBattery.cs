using UnityEngine;
using UnityEngine.UI;

public class BatterySpawner : MonoBehaviour
{
    public GameObject batteryPrefab;
    public Button button1;
    public Button button2;

    public void SpawnBattery(Vector3 position)
    {
        GameObject batteryParent = Instantiate(batteryPrefab, position, Quaternion.identity);
        Debug.Log("[BatterySpawner] Spawned parent battery object.");

        // 🔍 Find AmaBattery script in children
        AmaBattery batteryScript = batteryParent.GetComponentInChildren<AmaBattery>();

        if (batteryScript != null)
        {
            Debug.Log("[BatterySpawner] Found AmaBattery child. Assigning buttons...");

        }
        else
        {
            Debug.LogError("[BatterySpawner] AmaBattery.cs not found in children!");
        }
    }
}
