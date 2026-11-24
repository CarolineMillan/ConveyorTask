using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorPlacer : MonoBehaviour
{
    [SerializeField] Conveyor[] ConveyorPrefab;
    [SerializeField] Conveyor SelectedConveyor;
    [SerializeField] ProductMover Product;
    [SerializeField] LayerMask FloorMask;
    [SerializeField] Conveyor FirstPlaced;
    [SerializeField] Conveyor LastPlaced;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectedConveyor = ConveyorPrefab[0];
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
        
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            
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
                    } else
                    {
                        // TODO: unreachable? delete?
                        FirstPlaced = obj;
                    }
                    LastPlaced = obj;
                }
                else {
                    Conveyor obj = Instantiate(SelectedConveyor, hitInfo.point, Quaternion.identity);
                    //if (LastPlaced != null) {
                    //    LastPlaced.Next = obj;
                    //} else
                    if (LastPlaced == null)
                    {
                        FirstPlaced = obj;
                    }
                    LastPlaced = obj;
                }
            }
            
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (FirstPlaced != null)
            {
                // spawn a product
                ProductMover product = Instantiate(Product, FirstPlaced.StartPoint.position, Quaternion.identity);
                product.CurrentConveyor = FirstPlaced;
            }
        }
    }
}
