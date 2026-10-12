using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    CameraManager cameraManager;

    void Start()
    {
        cameraManager = CameraManager.Instance;
    
    }
    void Update()
    {
        UpdateMouseInput();
        UpdateMoveInput();
        UpdateShootInput();
    }
    void UpdateMouseInput()
    {
        UserInput.mouseWorldPosition = cameraManager.MainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
    void UpdateMoveInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            UserInput.moveInput = Vector2.zero;
            return;
        }

        Vector2 input = Vector2.zero;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) input.y += 1;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) input.y -= 1;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) input.x += 1;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) input.x -= 1;

        UserInput.moveInput = Vector2.ClampMagnitude(input, 1f);
    }
    void UpdateShootInput()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            UserInput.shootInput = false;
            return;
        }

        UserInput.shootInput = mouse.leftButton.isPressed;
    }
}
