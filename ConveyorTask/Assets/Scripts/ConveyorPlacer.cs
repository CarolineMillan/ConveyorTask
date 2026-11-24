using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ConveyorPlacer : MonoBehaviour
{
    [SerializeField] Conveyor[] conveyorPrefab;
    Conveyor selectedConveyor;
    [SerializeField] ProductMover[] productPrefab;
    int productIndex = 0;
    [SerializeField] LayerMask floorMask;
    readonly List<Conveyor> firstPlaced = new List<Conveyor>();
    Conveyor lastPlaced;
    float currentAngle = 0;
    [SerializeField] float snapDistance = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedConveyor = conveyorPrefab[0];
        Time.timeScale = 1;
    }

    public void SetRunning(bool running)
    {
        Time.timeScale = running ? 1 : 0;
    }

    // Select is called when a Conveyor is selected in the menu
    public void Select(int index)
    {
        selectedConveyor = conveyorPrefab[index];
    }

    // Update is called once per frame
    void Update()
    {
        // when left mouse button is clicked, use the position to create an instance of ConveyorPrefab
        if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleClick();
        }

        // places a product on the conveyor
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            HandleSpawn();
        }

        // rotate conveyor 90 degrees
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            HandleRotate();
        }
    }

    void HandleSpawn()
    {
        foreach (Conveyor conveyor in firstPlaced)
        {
            ProductMover product = Instantiate(productPrefab[productIndex], conveyor.StartPoint.position, Quaternion.identity);
            product.CurrentConveyor = conveyor;
        }

        productIndex = (productIndex + 1) % (productPrefab.Length);

    }

    void HandleClick()
    {
        Vector2 pos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(pos);

        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, floorMask);

        if (!hit) { return; }
        if (lastPlaced != null && Vector3.Distance(lastPlaced.EndPoint.position, hitInfo.point) < snapDistance)
        {

            // rotate by the currentAngle
            if (currentAngle == 180) { currentAngle = 0; }
            Quaternion rot = lastPlaced.transform.rotation * Quaternion.Euler(0, currentAngle, 0);
            Conveyor obj = Instantiate(selectedConveyor, hitInfo.point, rot);

            // gets the distance to move the conveyer to snap onto the previous conveyor
            Vector3 diff = lastPlaced.EndPoint.position - obj.StartPoint.position;
            obj.transform.position += diff;
            lastPlaced.Next = obj;
            lastPlaced = obj;
            currentAngle = 0;
        }
        else
        {
            Conveyor obj = Instantiate(selectedConveyor, hitInfo.point, Quaternion.Euler(0, currentAngle, 0));
            firstPlaced.Add(obj);
            lastPlaced = obj;
        }
    }

    void HandleRotate()
    {
        currentAngle = (currentAngle + 90) % 360;
    }
}
