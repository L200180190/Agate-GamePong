using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;

    // Tombol untuk gerak ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas dan bawah Game Scene (batas bawah dengan minus (-))
    public float yBoundary = 9.0f;

    // Rigidbody 2D raket
    private Rigidbody2D rigidBody2D;

    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    // Skor Player
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Dapatkan kecepatan raket
        Vector2 velocity = rigidBody2D.velocity;

        // Jika pemain menekan tombol ke atas, kecepatan positif ke komponen y (atas)
        if (Input.GetKey(upButton)){
            velocity.y = speed;
        }

        // Jika pemain menekan tombol ke bawah, kecepatan negatif ke komponen y (bawah)
        else if (Input.GetKey(downButton)){
            velocity.y = -speed;
        }

        // Jika pemain tidak menekan apa-apa, kecepatan nol
        else{
            velocity.y = 0.0f;
        }

        // Masukkan kembali kecepatan ke rigidBody2D
        rigidBody2D.velocity = velocity;


        // Dapatkan posisi raket
        Vector3 position = transform.position;

        // Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas
        if (position.y > yBoundary){
            position.y = yBoundary;
        }

        // Jika posisi raket melewati batas bawah (-yBoundary) kembalikan ke batas atas
        else if (position.y < -yBoundary){
            position.y = -yBoundary;
        }

        // Masukkan kembali posisi ke transform
        transform.position = position;
    }

    // Menaikkan skor sebanyak 1 poin
    public void IncrementScore(){
        score++;
    }

    // Mengembalikan skor menjadi 0
    public void ResetScore(){
        score = 0;
    }

    // Mendapatkan nilai skor
    public int Score{
        get { return score; }
    }

    // Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    // Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
