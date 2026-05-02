using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ClickInteractor : MonoBehaviour
{
    public InputAction pointerPosition;
    public InputAction leftClick;
    public LayerMask buildingLayer;
    public Camera cam;
    Tower _tower;

    private void OnEnable()
    {
        pointerPosition.Enable();
        leftClick.Enable();
    }

    private void OnDisable()
    {
        pointerPosition.Disable();
        leftClick.Disable();
    }

    private void Update()
    {
        if (leftClick.WasPerformedThisFrame())
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 screenPos = pointerPosition.ReadValue<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
        Collider2D hit = Physics2D.OverlapPoint(worldPos, buildingLayer);

        if (_tower != null)
            _tower.OpenUpgradePanel(false);

        _tower = null;

        if (hit != null && hit.TryGetComponent(out Tower tower))
        {
            _tower = tower;
            _tower.OpenUpgradePanel(true);
        }
    }
}