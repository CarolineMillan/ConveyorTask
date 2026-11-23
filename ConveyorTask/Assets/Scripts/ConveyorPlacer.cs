using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorPlacer : MonoBehaviour
{
    [SerializeField] Conveyor ConveyorPrefab;
    [SerializeField] LayerMask FloorMask;
    [SerializeField] Conveyor LastPlaced;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                if (LastPlaced != null && Vector3.Distance(LastPlaced.EndPoint.position, hitInfo.point) < 1f) {
                    Debug.Log("Snap!");
                    Conveyor obj = Instantiate(ConveyorPrefab, hitInfo.point, LastPlaced.transform.rotation);//Quaternion.identity);
                    // to snap, look at obj.StartPoint and LastPlaced.EndPoint
                    Vector3 diff = LastPlaced.EndPoint.position - obj.StartPoint.position;
                    obj.transform.position += diff;
                    LastPlaced = obj;
                }
                else {
                    Conveyor obj = Instantiate(ConveyorPrefab, hitInfo.point, Quaternion.identity);
                    LastPlaced = obj;
                }
            }
            
        }
    }
}
