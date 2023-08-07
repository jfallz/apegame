using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class CannonFire : MonoBehaviour
{
    public AudioSource soundCannon;
    public GameObject PlayerObject;
    GameObject newPlayer;
    public bool Aiming = true;
    public float minRotation = 0;
    public float maxRotation = 90;
    public float curAngle = 0f;
    public float force = 800f;
    private GameObject camera;
    private PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private float currentIntensity = 0f;
    private Coroutine intensityCoroutine;

    public void Start() {
        camera = GameObject.FindWithTag("MainCamera");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Aiming) {
            if(Input.GetKey(KeyCode.A)) {
                curAngle += 1f;
            } else if(Input.GetKey(KeyCode.D)) {
                curAngle -= 1f;
            }
            //print(curAngle);

            Vector3 newRotation = new Vector3(curAngle, -180, 0);
            transform.eulerAngles = newRotation;

            if(Input.GetKey(KeyCode.Space))
            {
                print("clicked");
          
                Fire();
            }
        }
    }

    public void Crash() {
        // induce stress on camera for shaking effect
        StressReceiver trauma = camera.GetComponent<StressReceiver>();
        trauma.InduceStress(5f);

        postProcessVolume = camera.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        if (chromaticAberration != null)
        {
            if(intensityCoroutine != null) {
                StopCoroutine(intensityCoroutine);
            }
            chromaticAberration.intensity.value = 1f;
            currentIntensity = .7f;
            intensityCoroutine = StartCoroutine(ChangeIntensityOverTime(.2f, .2f));
        }

    }

    private IEnumerator ChangeIntensityOverTime(float targetIntensity, float duration) {
        float elapsedTime = 0f;
        float startIntensity = currentIntensity;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolated intensity based on time
            float t = Mathf.Clamp01(elapsedTime / duration);
            currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, t);

            // Set the chromatic aberration intensity
            chromaticAberration.intensity.value = currentIntensity;

            yield return null;
        }

        // Ensure the final intensity is set correctly
        currentIntensity = targetIntensity;
        chromaticAberration.intensity.value = currentIntensity;

        // Reset the coroutine reference
        intensityCoroutine = null;
    }

    void Fire() {
        soundCannon.Play();
        float angle = (curAngle * -1f) * Mathf.Deg2Rad;
        float xComponent = Mathf.Cos(angle) * force;
        float zComponent = Mathf.Sin(angle) * force;
        GameObject player = GameObject.FindWithTag("Player");
        newPlayer = Instantiate(PlayerObject, player.transform.position, player.transform.rotation);
        Destroy(player);
        
        Rigidbody rb = newPlayer.GetComponent<Rigidbody>();
         // Add the ConfigurableJoint component to the target Rigidbody
        //ConfigurableJoint joint; // Reference to the ConfigurableJoint component

       // joint = rb.gameObject.AddComponent<ConfigurableJoint>();

        // Configure the joint properties
        //joint.angularXMotion = ConfigurableJointMotion.Limited; // Allow rotation along the X-axis within limits

        rb.isKinematic = false;
        Vector3 forceApply = new Vector3(0f, xComponent, zComponent);
        rb.AddForce(forceApply * 1f, ForceMode.Impulse);
                // Get the current inertia tensor
        //Vector3 inertiaTensor = rb.inertiaTensor;

        // Set the X component to zero
        //inertiaTensor.x = 0f;

        // Assign the modified inertia tensor back to the Rigidbody
        //rb.inertiaTensor = inertiaTensor;
        Aiming = false;
        Crash();
        //Destroy(joint);
    }
}
