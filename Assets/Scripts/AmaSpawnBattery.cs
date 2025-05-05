using UnityEngine;
using UnityEngine.UI;

public class AmaSpawnBattery : MonoBehaviour
{
    public GameObject batteryPrefab;
    public Button button1;
    public Button button2;

    public void SpawnBattery(Vector3 position)
    {
        GameObject battery = Instantiate(batteryPrefab, position, Quaternion.identity);
        AmaBattery ama = battery.GetComponent<AmaBattery>();
        if (ama != null)
        {
            ama.SetupButtons(button1, button2);
        }
        else
        {
            Debug.LogError("[BatterySpawner] Spawned object has no AmaBattery component!");
        }
    }
}
