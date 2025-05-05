using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AmaBattery : MonoBehaviour
{
    private Button button1;
    private Button button2;

    private bool isPlayerInside = false;
    private AhGuangHealth playerHealth;
    private bool used = false;

    private float button1Time = -1f;
    private float button2Time = -1f;
    private float bufferTime = 1f;

    public void SetupButtons(Button b1, Button b2)
    {
        button1 = b1;
        button2 = b2;

        AddButtonEvents(button1, true);
        AddButtonEvents(button2, false);

        Debug.Log("[AmaBattery] Buttons assigned through SetupButtons.");
    }

    void AddButtonEvents(Button button, bool isFirst)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry down = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        down.callback.AddListener((e) => SetPressTime(isFirst));
        trigger.triggers.Add(down);
    }

    void SetPressTime(bool isFirst)
    {
        float now = Time.time;

        if (isFirst)
        {
            button1Time = now;
            Debug.Log("[AmaBattery] Button 1 pressed at: " + button1Time);
        }
        else
        {
            button2Time = now;
            Debug.Log("[AmaBattery] Button 2 pressed at: " + button2Time);
        }

        TryHeal();
    }

    void TryHeal()
    {
        Debug.Log($"[AmaBattery] Checking heal conditions: inside={isPlayerInside}, used={used}, playerHealth={playerHealth != null}");

        if (!isPlayerInside || used || playerHealth == null) return;

        float timeDiff = Mathf.Abs(button1Time - button2Time);
        Debug.Log($"[AmaBattery] Time difference between buttons: {timeDiff}");

        if (timeDiff <= bufferTime)
        {
            float healAmount = playerHealth.maxHealth * 0.2f;
            float before = playerHealth.Health;

            playerHealth.Health += healAmount;
            playerHealth.Health = Mathf.Clamp(playerHealth.Health, 0, playerHealth.maxHealth);
            playerHealth.UpdateHealthBar();

            Debug.Log($"[AmaBattery] Healed player! Health before: {before}, after: {playerHealth.Health}");

            used = true;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("[AmaBattery] Buttons not pressed close enough in time.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AhGuangHealth health = other.GetComponent<AhGuangHealth>();
        if (health != null)
        {
            isPlayerInside = true;
            playerHealth = health;
            Debug.Log("[AmaBattery] Player entered trigger.");
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
