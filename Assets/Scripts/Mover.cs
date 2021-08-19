using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float rocketSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField]  ParticleSystem mainEngineParticles;
    [SerializeField]  ParticleSystem mainEngineParticles2;
    [SerializeField]  ParticleSystem mainEngineParticles3;
    [SerializeField]  ParticleSystem mainEngineParticles4;
    [SerializeField]  ParticleSystem mainEngineParticles5;

    
    [SerializeField]  ParticleSystem leftThrusterParticles;
    [SerializeField]  ParticleSystem rightThrusterParticles;
        
    Rigidbody rb;
    AudioSource _audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotating();
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotating();
        }
        else
        {
            StopRotating();
        }
    }

    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }


    void ApplyRotation(float rotationFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationFrame);
        rb.freezeRotation = false;
    }
    
    void StopThrusting()
    {
        MainEngineParticleStoper();
        _audioSource.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * rocketSpeed);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            MainEngineParticleStarter();
        }
    }
    void RightRotating()
    {
        ApplyRotation(-rotateSpeed);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void LeftRotating()
    {
        ApplyRotation(rotateSpeed);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }

        leftThrusterParticles.Play();
    }
    void MainEngineParticleStarter()
    {
        mainEngineParticles.Play();
        mainEngineParticles2.Play();
        mainEngineParticles3.Play();
        mainEngineParticles4.Play();
        mainEngineParticles5.Play();
    }
    void MainEngineParticleStoper()
    {
        mainEngineParticles.Stop();
        mainEngineParticles2.Stop();
        mainEngineParticles3.Stop();
        mainEngineParticles4.Stop();
        mainEngineParticles5.Stop();
    }
}
