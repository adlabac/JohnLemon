using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;  // Parametar kojim se definiše brzina kojom lik (John Lemon) mijenja pravac kretanja

    Animator anim;  // Referenca na Animator komponentu Johnovog objekta
    Rigidbody rb;  // Referenca na Rigidbody komponentu Johnovog objekta
    Vector3 movement;  // Vektor smjera u kom lik treba da se kreće
    Quaternion rotation = Quaternion.identity;  // Trenutni ugao rotacije lika, koji se polako približava vektoru "movement"

    void Start()  // Inicijalizacija promjenljivih na početku
    {
        anim = GetComponent<Animator>();  // Preuzimanje Animator komponente
        rb = GetComponent<Rigidbody>();   // Preuzimanje RigidBody komponente
    }

    void FixedUpdate()  // Obrada jednog "otkucaja" petlje zadužene za obradu fizike
    {
        float horizontal = Input.GetAxis("Horizontal");  // Očitavanje vrijednosti kontrole po horizontalnoj osi
        float vertical = Input.GetAxis("Vertical");  // Očitavanje vrijednosti kontrole po vertikalnoj osi

        movement.Set(horizontal, 0f, vertical);  // Kreiranje vektora pravca, na osnovu očitanih kontrola
        movement.Normalize();  // Normalizacija vektora, tako da ima intenzitet 1, tako da vrijednosti kontrola ne utiču na brzinu kretanja lika

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);  // Da li se lik kreće u pravcu horizontalne ose?
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);  // Da li se lik kreće u pravcu vertikalne ose?
        bool isWalking = hasHorizontalInput || hasVerticalInput;  // Da li se lik kreće u pravcu bilo koje ose?
        anim.SetBool("isWalking", isWalking);  // Podesi parametar animacije, tako da odgovara tome da li se lik kreće ili stoji

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);  // Odredi željeni pravac u kom treba okrenuti lika, u zavisnosti od proteklog vremena, ali ne brže od zadatog parametra
        rotation = Quaternion.LookRotation(desiredForward);  // Odredi ugao rotacije na osnovu izračunatog pravca
    }

    void OnAnimatorMove()  // Poziva se kada se odredi "root motion" lika, kako bi se kretao brzinom u skladu sa njegovom animacijom
    {
        rb.MovePosition(rb.position + movement * anim.deltaPosition.magnitude);  // Pomjeri lika u željenom pravcu, za dužinu definisanu njegovim root motionom
        rb.MoveRotation(rotation);  // Postavi željeni ugao za koji je lik okrenut
    }
}
