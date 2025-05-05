using UnityEngine;
using UnityEngine.UI;

public class AmaBattery : MonoBehaviour
{
    private Button ButtonL;
    private Button ButtonR;

    private bool isPlayerInside = false;
    private AhGuangHealth playerHealth;
    private bool used = false;

    private float button1Time = -1f;
    private float button2Time = -1f;
    private float bufferTime = 0.5f;

    void Start()
    {
        Debug.Log("[AmaBattery] Start() called.");

        // 🔍 Auto-find buttons by name
        GameObject b1Obj = GameObject.Find("ButtonL");
        GameObject b2Obj = GameObject.Find("ButtonR");

        if (b1Obj != null && b2Obj != null)
        {
            ButtonL = b1Obj.GetComponent<Button>();
            ButtonR = b2Obj.GetComponent<Button>();

            if (ButtonL != null && ButtonR != null)
            {
                ButtonL.onClick.AddListener(() => OnButtonClicked(true));
                ButtonR.onClick.AddListener(() => OnButtonClicked(false));

                Debug.Log("[AmaBattery] Buttons found and listeners added.");
            }
            else
            {
                Debug.LogError("[AmaBattery] Found GameObjects but Button component missing.");
            }
        }
        else
        {
            Debug.LogError("[AmaBattery] Button GameObjects not found by name.");
        }
    }

    void OnButtonClicked(bool isFirst)
    {
        float now = Time.time;

        if (isFirst)
        {
            button1Time = now;
            Debug.Log("[AmaBattery] Button 1 clicked at " + now);
        }
        else
        {
            button2Time = now;
            Debug.Log("[AmaBattery] Button 2 clicked at " + now);
        }

        TryHeal();
    }

    void TryHeal()
    {
        if (!isPlayerInside || used || playerHealth == null)
        {
            Debug.Log("[AmaBattery] Conditions not met for healing.");
            return;
        }

        float timeDiff = Mathf.Abs(button1Time - button2Time);
        Debug.Log("[AmaBattery] Time difference: " + timeDiff);

        if (timeDiff <= bufferTime)
        {
            float amount = playerHealth.maxHealth * 0.2f;
            float before = playerHealth.Health;
            playerHealth.Health += amount;
            playerHealth.Health = Mathf.Clamp(playerHealth.Health, 0, playerHealth.maxHealth);
            playerHealth.UpdateHealthBar();

            Debug.Log($"[AmaBattery] Healed from {before} to {playerHealth.Health}");

            used = true;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("[AmaBattery] Button presses not close enough.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[AmaBattery] Trigger entered by: " + other.name);

        AhGuangHealth health = other.GetComponent<AhGuangHealth>();
        if (health != null)
        {
            isPlayerInside = true;
            playerHealth = health;
            Debug.Log("[AmaBattery] Player detected inside trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AhGuangHealth>() != null)
        {
            isPlayerInside = false;
            playerHealth = null;
            Debug.Log("[AmaBattery] Player exited trigger.");
        }
    }
}
