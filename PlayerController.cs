using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hareketHizi = 5f; // Hareket h�z�
    public float bakmaHizi = 10f; // D�n�� h�z�
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Yumu�ak hareket i�in Rigidbody'nin do�ru �ekilde ayarland���ndan emin olun
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        // Hareket giri�ini i�le
        float yatayHareket = Input.GetAxis("Horizontal");
        float dikeyHareket = Input.GetAxis("Vertical");
        Vector3 hareket = new Vector3(yatayHareket, 0f, dikeyHareket).normalized;

        // Hareket vekt�r�n� hesapla ve karakteri hareket ettir
        Vector3 hareketVector = transform.TransformDirection(hareket) * hareketHizi * Time.deltaTime;
        rb.MovePosition(rb.position + hareketVector);

        // Karakterin gitmekte oldu�u y�ne d�nmesini sa�la
        if (hareket != Vector3.zero)
        {
            Quaternion yeniRotasyon = Quaternion.LookRotation(hareket);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, yeniRotasyon, bakmaHizi * Time.deltaTime));
        }

        // Y�r�yor mu kontrol�
        bool isWalking = yatayHareket != 0f || dikeyHareket != 0f;
        animator.SetBool("isWalking", isWalking);
    }
}
