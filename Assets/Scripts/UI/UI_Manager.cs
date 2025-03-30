using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    public Slider holdSlider;
    private bool canUseHoldWheel = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            Instance = (UI_Manager)this;
        }
    }

    public void EnableHoldWheel(bool value)
    {
        canUseHoldWheel = value;
    }

    public void SetupHoldSlider(float maxValue)
    {
        if (canUseHoldWheel)
        {
            holdSlider.maxValue = maxValue;
        }
    }

    public void UpdateHoldSlider(float value)
    {
        if (canUseHoldWheel)
        {
            holdSlider.value = value;
        }
    }
}
