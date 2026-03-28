using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb; //zmienna zawieraj¹ca rigidbody
    [SerializeField] float speed = 10f; //zmienna z prêdkoœci¹ gracza, ustalana w edytorze - SerializeField
    [SerializeField] float camSensitivity = 1f; //zmienna z czu³oœci¹ kamery
    Camera cam; //zmienna typu Camera
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //znajdowanie komponentu/w³aœciwoœci Rigidbody w obiekcie do którego jest podpiêty skrypt
        cam = Camera.main; //przypisanie g³ównej kamery na scenie (ta w graczu) do zmiennej cam
    }

    // Update is called once per frame
    void Update()
    {
        OnMove(); //wywo³anie metody OnMove
        LookAround(); //Wywo³anie metody LookAround
    }

    void OnMove() //deklaracja metody OnMove, która nic nie zwraca (void)
    {
        float z = Input.GetAxis("Vertical"); //pobiera info z klawiatury oœ x (0, 1 lub -1)
        float x = Input.GetAxis("Horizontal"); //pobiera info z klawiatury oœ y (0, 1 lub -1)

        rb.linearVelocity = transform.TransformDirection(x * speed, rb.linearVelocity.y, z * speed); //nadajemy ruch graczowi (jego rb), z u¿yciem
        //metody TransformDirection, która pozwala na ruch z uwzglêdnieniem obrotu gracza
    }

    void LookAround() //definicja metody LookAround
    {
        float mouseX = Input.GetAxis("Mouse X"); // stworzenie zmiennej mouseX i przypisanie wartoœci sczytanej z ruchu myszki w osi X
        float mouseY = Input.GetAxis("Mouse Y"); // stworzenie zmiennej mouseY i przypisanie wartoœci sczytanej z ruchu myszki w osi Y
        //Debug.Log(mouseY); //Wypisanie w konsoli wartoœci zmiennej mouseX
        gameObject.transform.Rotate(0,mouseX * camSensitivity,0); //gameObject odnosi siê do objektu do którego jest pod³¹czony skrypt (Player)
                                                                  //transform odnosi siê do pozycji, rotacji i skali tego obiektu
                                                                  //Rotate jest metod¹ z Unity, która s³u¿y do obracania obiektu o podan¹ wartoœæ.
        if (cam.transform.eulerAngles.x > 65 && cam.transform.eulerAngles.x < 295 ) Debug.Log("Warunek spe³niony"); // do dokoñczenia
        cam.transform.Rotate(-mouseY * camSensitivity,0,0); //u¿ywamy metody Rotate do obrotu kamery wokó³ osi X
       
    }
}
