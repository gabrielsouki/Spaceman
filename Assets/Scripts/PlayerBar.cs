using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType { healthBar, manaBar }

public class PlayerBar : MonoBehaviour
{
    public Slider m_slider;
    public BarType type;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialValues();
    }

    // Update is called once per frame
    void Update()
    {
        SetValues();
    }

    void SetInitialValues()
    {
        switch (type)
        {
            case BarType.healthBar:
                SetHealthValues();
                break;
            case BarType.manaBar:
                SetManaValues();
                break;
        }
    }

    void SetHealthValues()
    {
        m_slider.minValue = PlayerController.MIN_HEALTH;
        m_slider.maxValue = PlayerController.MAX_HEALTH;
        m_slider.value = PlayerController.INITIAL_HEALTH;
    }

    void SetManaValues()
    {
        m_slider.minValue = PlayerController.MIN_MANA;
        m_slider.maxValue = PlayerController.MAX_MANA;
        m_slider.value = PlayerController.INITIAL_MANA;
    }

    void SetValues()
    {
        switch (type)
        {
            case BarType.healthBar:
                m_slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            case BarType.manaBar:
                m_slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }
}
