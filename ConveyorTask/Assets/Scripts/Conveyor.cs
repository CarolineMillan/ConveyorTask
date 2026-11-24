using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    
    public Transform StartPoint => start;
    public Transform EndPoint => end;
    public Conveyor Next;
}
