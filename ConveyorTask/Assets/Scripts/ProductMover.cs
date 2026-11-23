using UnityEngine;
using UnityEngine.UIElements;

public class ProductMover : MonoBehaviour
{
    
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       transform.position = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.position, speed*Time.deltaTime);
    }
}
