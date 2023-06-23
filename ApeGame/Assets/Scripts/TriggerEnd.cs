using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TriggerEnd : MonoBehaviour
{
    public GameObject fracturedPrefab;
    public GameObject playerPrefab;
    public GameObject GameMan;
    public GameObject camera;
    private PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private float currentIntensity = 0f;
    private Coroutine intensityCoroutine;

    private float desiredTime = 1f;
    private float timer = 0f;

    private Rigidbody p;
    private bool moving = false;

    public void OnTriggerEnter(Collider a)
    {
        string collidedObjectTag = a.gameObject.tag;
        if(!a.CompareTag("Cart") || moving)
            return;

        GameObject cartObject = GameObject.FindWithTag("Cart");
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 cartPos = cartObject.transform.position;
        Vector3 playerPos = player.transform.position;

        Rigidbody otherRb = a.GetComponentInParent<Rigidbody>();

        if(cartObject != null) {
            Vector3 vel = otherRb.velocity;
            vel += new Vector3(0f, 10f, 2f);
            Destroy(player);
            Destroy(cartObject);
            Destroy(gameController);

            Vector3 newCartPos = new Vector3(cartPos.x, cartPos.y - 2.26f, cartPos.z);
            GameObject tempCart = Instantiate(fracturedPrefab, newCartPos, Quaternion.identity);
            //tempCart = Instantiate(fracturedPrefab, newCartPos, Quaternion.identity);

            GameObject newPlayer = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            Rigidbody rb = GameObject.FindWithTag("EditorOnly").GetComponent<Rigidbody>();
            vel.y = Mathf.Abs(vel.y) / 2f;
            vel.z = Mathf.Abs(vel.z);
            rb.velocity = vel * 6f;


            for(int i = 0; i < tempCart.transform.childCount; ++i) {
                GameObject child = tempCart.transform.GetChild(i).gameObject;
                Rigidbody childRb = child.GetComponent<Rigidbody>();
                childRb.velocity = vel * Random.Range(-10.5f, 10.5f);
            }
            Crash();
            p = rb;
            moving = true;
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
            currentIntensity = 1f;
            intensityCoroutine = StartCoroutine(ChangeIntensityOverTime(0f, .75f));
        }

    }

    public void FixedUpdate() {
        if(moving) {
            if(p.velocity.magnitude < 1) {
                timer += Time.deltaTime;
                if(timer >= desiredTime) {
                    TriggerStart.timer = false;
                    print("Telling GameManager that we're dead");
                    moving = false;
                    GameManager script = GameMan.GetComponent<GameManager>();
                    script.dead = true;
                }
            } else {
                timer = 0f;
            }
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
}
