using UnityEngine;
using UnityEngine.InputSystem;

public enum ConstructionState
{
    Inactive,
    Moving
}

public class BuildingConstructor : MonoBehaviour
{
    [Header("References")]
    public Camera cam;

    [Header("Input")]
    public InputAction pointerPosition;
    public InputAction leftClick;
    public InputAction cancelInput;

    [Header("Settings")]
    public LayerMask terrainLayer;
    public LayerMask buildingLayer;
    public Color validColor;
    public Color invalidColor;

    [Header("State")]
    ConstructionState _state;
    Color _initColor;
    Building _building;

    private void OnEnable()
    {
        pointerPosition.Enable();
        leftClick.Enable();
        cancelInput.Enable();
    }

    private void OnDisable()
    {
        pointerPosition.Disable();
        leftClick.Disable();
        cancelInput.Disable();
    }

    public void EnterConstructionMode(GameObject prefab)
    {
        ClearExistingBuilding();
        _state = ConstructionState.Moving;
        var spawnPos = MouseToWorldPoint();
        var clone = Instantiate(prefab, spawnPos, Quaternion.identity);
        _building = clone.GetComponent<Building>();
        _initColor = _building.GetColor();
    }

    private void Update()
    {
        if (_state != ConstructionState.Inactive)
        {
            ProcessConstructionMode();
        }
    }

    void ProcessConstructionMode()
    {
        if (cancelInput.WasPerformedThisFrame())
        {
            CancelConstruction();
            return;
        }

        MoveBuilding();

        if (!IsFullyOnTerrain() || !CanConstructAtPosition())
        {
            _building.SetColor(invalidColor);
            return;
        }

        _building.SetColor(validColor);

        if (leftClick.WasPerformedThisFrame())
        {
            ConstructBuilding();
        }
    }

    void CancelConstruction()
    {
        ClearExistingBuilding();
        ExitConstructionMode();
    }

    void ClearExistingBuilding()
    {
        if (_building != null)
        {
            Destroy(_building.gameObject);
        }
    }

    void ExitConstructionMode()
    {
        _building = null;
        _state = ConstructionState.Inactive;
    }

    Vector3 MouseToWorldPoint()
    {
        var screenPos = pointerPosition.ReadValue<Vector2>();
        var worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -cam.transform.position.z));
        worldPos.z = 0f;
        return worldPos;
    }

    void MoveBuilding()
    {
        _building.transform.position = MouseToWorldPoint();
    }

    // All 4 corners of the building must be inside a terrain collider
    bool IsFullyOnTerrain()
    {
        Vector2[] corners = _building.GetColliderCorners();

        foreach (var corner in corners)
        {
            var hit = Physics2D.OverlapPoint(corner, terrainLayer);
            if (hit == null)
                return false;
        }

        return true;
    }

    // Only 1 overlap allowed (the building itself)
    bool CanConstructAtPosition()
    {
        Collider2D[] overlaps = new Collider2D[2];
        var objectCenter = _building.CenterPoint();
        var extents = _building.ColliderWorldSize() / 2f;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(buildingLayer);
        filter.useTriggers = false;

        var collisionCount = Physics2D.OverlapBox(objectCenter, extents, 0f, filter, overlaps);
        return collisionCount == 1;
    }

    void ConstructBuilding()
    {
        _building.spriteRenderer.color = _initColor;
        _building.InvokeBuildingPlace();
        ExitConstructionMode();
    }
}