using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb; //zmienna zawieraj¹ca rigidbody
    [SerializeField] float speed = 10f; //zmienna z prêdkoœci¹ gracza, ustalana w edytorze - SerializeField
    [SerializeField] float camSensitivity = 1f; //zmienna z czu³oœci¹ kamery
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //znajdowanie komponentu/w³aœciwoœci Rigidbody w obiekcie do którego jest podpiêty skrypt
    }

    // Update is called once per frame
    void Update()
    {
        OnMove(); //wywo³anie metody OnMove
    }

    void OnMove() //deklaracja metody OnMove, która nic nie zwraca (void)
    {
        float z = Input.GetAxis("Vertical"); //pobiera info z klawiatury oœ x (0, 1 lub -1)
        float x = Input.GetAxis("Horizontal"); //pobiera info z klawiatury oœ y (0, 1 lub -1)

        rb.linearVelocity = new Vector3(x * speed, 0,  z * speed); //nadaje prêdkoœæ liniow¹ graczowi (jego rigidbody)
    }
}
