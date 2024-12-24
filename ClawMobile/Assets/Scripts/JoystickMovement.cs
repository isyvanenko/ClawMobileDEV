using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    public RectTransform joystickKnob;  // The movable knob
    public RectTransform joystickBase; // The static base
    public float joystickRange = 50f;  // Maximum horizontal distance the knob can move
    public float horizontalInput;      // Horizontal input value (-1 to 1)

    private Vector2 startPosition;     // Starting position of the joystick knob

    void Start()
    {
        startPosition = joystickKnob.anchoredPosition; // Save the initial position
    }

    public void OnDrag()
    {
        // Get mouse/touch position in the UI space
        Vector2 pointerPosition = Input.mousePosition;

        // Convert to local UI position relative to the joystick base
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBase, pointerPosition, null, out Vector2 localPointerPosition);

        // Calculate horizontal offset and clamp within joystick range
        float offsetX = Mathf.Clamp(localPointerPosition.x - startPosition.x, -joystickRange, joystickRange);

        // Update joystick knob position and calculate horizontal input
        joystickKnob.anchoredPosition = new Vector2(offsetX, startPosition.y);
        horizontalInput = offsetX / joystickRange; // Normalize input (-1 to 1)
    }

    public void OnRelease()
    {
        // Reset the joystick knob to its base position
        joystickKnob.anchoredPosition = startPosition;
        horizontalInput = 0f; // Reset input
    }
}
