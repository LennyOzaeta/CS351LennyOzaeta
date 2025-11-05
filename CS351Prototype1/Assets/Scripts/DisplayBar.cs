using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Must include to use Slider (imports UI library)

// Follow Along Lesson 8

public class DisplayBar : MonoBehaviour
{
    public Slider slider; // Reference to Slider for health bar
    public Gradient gradient; // Gradient for health bar
    public Image fill; // Image for fill of health bar

    // Function to set current value of Slider (which could apply to any type of bar, not just a health bar)
    public void SetValue(float value)
    {
        slider.value = value; // Set value of Slider
        fill.color = gradient.Evaluate(slider.normalizedValue); // Set color of Slider's fill
    }

    // Function to set max value of Slider
    public void SetMaxValue(float value)
    {
        slider.maxValue = value; // Set max value of Slider
        slider.value = value; // Set current value of Slider to max value
        fill.color = gradient.Evaluate(1f); // Set color of Slider's fill (maximum)
    }
}
