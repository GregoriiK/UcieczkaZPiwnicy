using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb; //zmienna zawieraj¹ca rigidbody
    [SerializeField] float speed = 10f; //zmienna z prêdkoœci¹ gracza, ustalana w edytorze - SerializeField
    [SerializeField] float camSensitivity = 1f; //zmienna z czu³oœci¹ kamery
    Camera cam; //zmienna typu Camera
    [SerializeField]float jumpForce = 500f; //ustawienie si³y skoku
    bool isGrounded = false;        //sprawdzenie czy gracz stan¹³ na ziemi
    float sprintMultiplier = 1f;
    float sneakMultiplier = 0.4f;
    bool isSneaking = false;
    float cameraSneakOffset = 0.1f;
    CapsuleCollider playerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //znajdowanie komponentu/w³aœciwoœci Rigidbody w obiekcie do którego jest podpiêty skrypt
        cam = Camera.main; //przypisanie g³ównej kamery na scenie (ta w graczu) do zmiennej cam
        playerCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove(); //wywo³anie metody OnMove
        LookAround(); //Wywo³anie metody LookAround
        Jump();
        Sneak();
        Sprint();
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift)) sprintMultiplier = 2; //sprawdzanie czy jest wciœniêty lewy shift, jeœli tak to ustawia zmienn¹ sprintMultiplier na 2
        else sprintMultiplier = 1; //jeœli warunek nie jest spe³niony to ustawia zmienn¹ sprint multiplier na 1

        if (isSneaking) sprintMultiplier = 1;
    }

    void Sneak()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isSneaking = !isSneaking;
            if (isSneaking)
            {
                sneakMultiplier = 0.4f;
                cam.transform.localPosition = new Vector3(0,cam.transform.localPosition.y - cameraSneakOffset,0);
                playerCollider.height = 1;
            }
            else
            {
                sneakMultiplier = 1;
                cam.transform.localPosition = new Vector3(0, cam.transform.localPosition.y + cameraSneakOffset, 0);
                playerCollider.height = 2;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true; // sprawdzenie czy gracz zderzy³ siê z obiektem o tagu "Ground", ustawienie zmiennej isGrounded na true
    }

    void Jump()
    {
        if ( Input.GetKeyDown(KeyCode.Space) && isGrounded) // sprawdza czy spacja zosta³a naciœniêta i czy zmienna bool isGrounded jest prawd¹
        {
            rb.AddForce(Vector3.up * jumpForce); //jeœli prawda to "podrzuca" gracza w górê z moc¹ (jumpForce)
            isGrounded = false;                  // i ustawia zmienn¹ isGrounded na false - uniemo¿liwia to podskakiwanie w powietrzu
        }
    }

    void OnMove() //deklaracja metody OnMove, która nic nie zwraca (void)
    {
        float z = Input.GetAxis("Vertical"); //pobiera info z klawiatury oœ x (0, 1 lub -1)
        float x = Input.GetAxis("Horizontal"); //pobiera info z klawiatury oœ y (0, 1 lub -1)

        rb.linearVelocity = transform.TransformDirection(x * (speed*sprintMultiplier*sneakMultiplier), rb.linearVelocity.y, z * (speed*sprintMultiplier*sneakMultiplier)); //nadajemy ruch graczowi (jego rb), z u¿yciem
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
        if (cam.transform.eulerAngles.x > 65 && cam.transform.eulerAngles.x < 80 && mouseY < 0) mouseY = 0;//sprawdzenie czy patrzê za nisko i czy próbujê dalej patrzeæ w dó³
        else if(cam.transform.eulerAngles.x <295 && cam.transform.eulerAngles.x > 280 && mouseY > 0) mouseY = 0;//sprawdzenie czy patrzê za nisko i czy próbujê dalej patrzeæ w dó³



        cam.transform.Rotate(-mouseY * camSensitivity,0,0); //u¿ywamy metody Rotate do obrotu kamery wokó³ osi X
       
    }
}
