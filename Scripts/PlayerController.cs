using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 630;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public bool hasDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
           playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           isOnGround = false;
           playerAnim.SetTrigger("Jump_trig");
           dirtParticle.Stop();
           playerAudio.PlayOneShot(jumpSound, 1.0f);
           hasDoubleJump = true;
        } else if(Input.GetKeyDown(KeyCode.Space) && hasDoubleJump && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            hasDoubleJump = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.speed = 2.5f;
        }
        else
        {
            playerAnim.speed = 1;
        }
    }
    private  void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
           isOnGround = true;
           dirtParticle.Play();
        } else if(collision.gameObject.CompareTag("Obstacle"))
        {
           gameOver = true;
           Debug.Log("Game Over!");
           playerAnim.SetBool("Death_b", true);
           playerAnim.SetInteger("DeathType_int", 1);
           explosionParticle.Play();
           dirtParticle.Stop();
           playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
