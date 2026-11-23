using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorPlacer : MonoBehaviour
{
    [SerializeField] GameObject ConveyorPrefab;
    [SerializeField] LayerMask FloorMask;
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
                GameObject obj = Instantiate(ConveyorPrefab, hitInfo.point, Quaternion.identity);
            }
            
        }
    }
}
