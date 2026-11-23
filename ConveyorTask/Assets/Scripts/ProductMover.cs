using UnityEngine;
//using UnityEngine.UIElements;

public class ProductMover : MonoBehaviour
{
    
    [SerializeField] float speed = 5f;
    public Conveyor CurrentConveyor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       transform.position = CurrentConveyor.StartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentConveyor.EndPoint.position, speed*Time.deltaTime);
        
        if (Vector3.Distance(transform.position, CurrentConveyor.EndPoint.position) < 0.01) {
            CurrentConveyor = CurrentConveyor.Next;
            if (CurrentConveyor == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
