using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody body;
    public bool leftFoot, rightFoot;
    public bool control;
    private bool gravityJump;
    public float minlimitX,maxlimitX;
    private ParticleSystem[] jumpParticlePlay;

    private float gravityPower, mass,jumpSensibility;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        jumpParticlePlay = transform.Find("Jump").GetComponentsInChildren<ParticleSystem>();
        
        if (PlayerPrefs.HasKey("GravityPower"))
        {
            gravityPower = PlayerPrefs.GetFloat("GravityPower");
        }
        else gravityPower = 10f;
        
        if (PlayerPrefs.HasKey("PlayerMass"))
        {
            mass = PlayerPrefs.GetFloat("PlayerMass");
        }
        else mass = 9.81f;
        
        Physics.gravity = new Vector3(0, -mass, 0);

        
        if (PlayerPrefs.HasKey("JumpSensibility"))
        {
            jumpSensibility = PlayerPrefs.GetFloat("JumpSensibility");
        }
        else jumpSensibility = 1f;

        if (PlayerPrefs.HasKey("NoTrigger"))
        {
            if (PlayerPrefs.GetInt("NoTrigger") == 1)
            {
                gravityJump = true;
            }
            else gravityJump = false;
        }

        if (SceneManager.GetActiveScene().name == "BridgeGame" |SceneManager.GetActiveScene().name == "BridgeGame")
        {
            if (PlayerPrefs.HasKey("BridgeExtraPower"))
            {
                gravityPower += PlayerPrefs.GetInt("BridgeExtraPower");
            }
        }
        
    }

    public virtual void Effets()
    {
        // if (body.velocity.y > 0.1f)
        // {
        //     foreach (var particle in jumpParticlePlay)
        //     {
        //         particle.Play();
        //     }
        // }
        // else
        // {
        //     foreach (var particle in jumpParticlePlay)
        //     {
        //         particle.Stop();
        //     }
        // }
    }
    
    private void OnEnable()
    {
        GameBase.StartGame.AddListener(()=> control = true);
        GameBase.FinishGame.AddListener(()=> gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        GameBase.StartGame.RemoveListener(()=> control = true);
        GameBase.FinishGame.RemoveListener(()=> gameObject.SetActive(false));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameBase.Dilaver.SoundSystem.PlaySound(Sounds.jump);
            body.velocity = new Vector3(body.velocity.x, 1 * 8);
        }

        //Effets();
        
        if (!control) return;
        
        var y = KinectManager.Instance
            .GetJointPosition(0, 0).y;
         Debug.Log(y);

        if(!gravityJump)
        {
            if (y > jumpSensibility & (leftFoot | rightFoot) & body.velocity.y < 1)
            {
                GameBase.Dilaver.SoundSystem.PlaySound(Sounds.jump);
                body.velocity = new Vector3(body.velocity.x, y * gravityPower);
            }
        }
        else
        {
            if (y > jumpSensibility)
            {
                GameBase.Dilaver.SoundSystem.PlaySound(Sounds.jump);
                body.velocity = new Vector3(body.velocity.x, y * gravityPower);
            }
        }

        // if (body.velocity.y > 0 & ( y- jumpSensibility) > 0)
        // {
        //     var speed = body.velocity.y;
        //     body.velocity -= new Vector3(0,speed + body.velocity.y,0);
        // }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minlimitX, maxlimitX), transform.position.y, 0);
    }
}
