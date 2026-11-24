using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ConveyorPlacer : MonoBehaviour
{
    [SerializeField] Conveyor[] ConveyorPrefab;
    [SerializeField] Conveyor SelectedConveyor;
    [SerializeField] ProductMover[] ProductPrefab;
    [SerializeField] int ProductIndex;
    [SerializeField] LayerMask FloorMask;
    [SerializeField] List<Conveyor> FirstPlaced;
    [SerializeField] Conveyor LastPlaced;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectedConveyor = ConveyorPrefab[0];
        ProductIndex = 0;
        Time.timeScale = 1;
    }

    public void SetRunning(bool running)
    {
        Time.timeScale = running ? 1 : 0;
    }
    
    // Select is called when a Conveyor is selected in the menu
    public void Select(int index)
    {
        SelectedConveyor = ConveyorPrefab[index];
    }

    // Update is called once per frame
    void Update()
    {
        // when left mouse button is clicked, use the position to create an instance of ConveyorPrefab
        
        if (Mouse.current.leftButton.wasPressedThisFrame  && !EventSystem.current.IsPointerOverGameObject()) {
            
            Vector2 pos = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(pos);

            bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, FloorMask);
            
            if (hit)
            {
                if (LastPlaced != null && Vector3.Distance(LastPlaced.EndPoint.position, hitInfo.point) < 2f) {
                    Debug.Log("Snap!");
                    Conveyor obj = Instantiate(SelectedConveyor, hitInfo.point, LastPlaced.transform.rotation);
                    Vector3 diff = LastPlaced.EndPoint.position - obj.StartPoint.position;
                    obj.transform.position += diff;
                    if (LastPlaced != null) {
                        LastPlaced.Next = obj;
                    } //else
                    //{
                        // TODO: unreachable? delete?
                    //    FirstPlaced.Add(obj);
                    //}
                    LastPlaced = obj;
                }
                else {
                    Conveyor obj = Instantiate(SelectedConveyor, hitInfo.point, Quaternion.identity);
                    //if (LastPlaced != null) {
                    //    LastPlaced.Next = obj;
                    //} else
                    //if (LastPlaced == null)
                    //{
                        FirstPlaced.Add(obj);
                    //}
                    LastPlaced = obj;
                }
            }
            
        }

        // places a product on the conveyor
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (FirstPlaced != null)
            {
                // spawn a product
                foreach (Conveyor conveyor in FirstPlaced) {
                    ProductMover product = Instantiate(ProductPrefab[ProductIndex], conveyor.StartPoint.position, Quaternion.identity);
                    product.CurrentConveyor = conveyor;
                }
                
                ProductIndex = (ProductIndex + 1) % (ProductPrefab.Length);
                
            }
        }
    }
}
