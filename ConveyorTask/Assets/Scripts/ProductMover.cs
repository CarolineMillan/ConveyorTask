using UnityEngine;
//using UnityEngine.UIElements;

public class ProductMover : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 180f;
    public Conveyor CurrentConveyor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       transform.position = CurrentConveyor.StartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentConveyor.EndPoint.position, moveSpeed*Time.deltaTime);

        Vector3 direction = CurrentConveyor.EndPoint.position - CurrentConveyor.StartPoint.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed*Time.deltaTime);
        
        if (Vector3.Distance(transform.position, CurrentConveyor.EndPoint.position) < 0.01) {
            CurrentConveyor = CurrentConveyor.Next;
            if (CurrentConveyor == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
