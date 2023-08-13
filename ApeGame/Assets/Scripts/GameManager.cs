using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public CannonFire scriptRef;
    public int distanceTraveled, distanceBest;
    // ----
    public float maxDistance = 0f;
    public float maxSpeed = 0f;
    public float maxAltitude = 0f;
    // ----
    private int startingDistance;
    [SerializeField] public float currency = 0f;
    public GameObject prefabToInstantiate;
    public GameObject playerMenu;
    public bool restart = false; // if true, will restart the player and then reset bool to false
    public float TimeT = 0f;
    private bool inFlight = false; 
    private bool launched = false;
    public GameObject speedometer;
    public float min = 317f;
    public float max = 45f;
    private bool dead = false;
    // ----
    public GameObject[] confettiPrefabs;
    private List<GameObject> confettiArray = new List<GameObject>();
    public void Start() {
        distanceTraveled = 0;
        distanceBest = 0;
    }

    public void FixedUpdate() {
        scriptRef = GameObject.Find("Rotation").GetComponent<CannonFire>();
        inFlight = !scriptRef.Aiming;
        if(inFlight) {
            Rigidbody rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
            if (!launched) {
                startingDistance = (int)GameObject.FindWithTag("Player").transform.position.z;
                launched = true;

            }
            if(Physics.Raycast(GameObject.FindWithTag("Player").transform.position, Vector3.down, 7f)) {
                if(rb.velocity.magnitude < 1 && !dead) 
                    Death();
            }
            statTrack();
        }
    }

    public void statTrack() {
        GameObject p = GameObject.FindWithTag("Player");
        float speed = p.GetComponent<Rigidbody>().velocity.magnitude;
        distanceTraveled = (int)(p.transform.position.z) - startingDistance;
        TextMeshProUGUI d = GameObject.Find("DISTANCE").GetComponent<TextMeshProUGUI>();
        d.text = distanceTraveled > 9999 ? FormatNumber(distanceTraveled) + "m" : distanceTraveled + "m";
        if(speed > maxSpeed) {
            maxSpeed = speed;
        }

        if(distanceTraveled > maxDistance)
            maxDistance = distanceTraveled;
    }
    public void Death() {
        if(distanceTraveled > distanceBest) {
            distanceBest = distanceTraveled;
            Confetti();
        }
        currency += distanceTraveled * .25f;
        TextMeshProUGUI c = GameObject.Find("CURRENCY").GetComponent<TextMeshProUGUI>();
        c.text = ((int)currency).ToString();
        playerMenu.SetActive(true);
        dead = true;
        GameObject.Find("deathMenu").GetComponent<Animator>().Play("PopIn");
    }

    public void ShopMenu() {
    }

    public void Restart() {
        print("restarting");
        playerMenu.SetActive(false);
        GameObject.Find("DISTANCE").GetComponent<TextMeshProUGUI>().text = "0m";
        GameObject targetObject = GameObject.FindWithTag("Cart");
        Vector3 targetPosition = targetObject.transform.position;
        Quaternion targetRotation = targetObject.transform.rotation;
        Destroy(targetObject);
        Destroy(GameObject.FindWithTag("Player"));
        GameObject newObject = Instantiate(prefabToInstantiate, targetPosition, targetRotation);
        launched = false;
        dead = false;
        for(int i = confettiArray.Count - 1; i >= 0; --i) {
            Destroy(confettiArray[i]);
            confettiArray.RemoveAt(i);
        }
        distanceTraveled = 0;
    }

    public void Confetti() {
        
        audioSource.PlayOneShot(clip);
        
        GameObject blastConfetti = Instantiate(confettiPrefabs[0], GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
        blastConfetti.GetComponent<Transform>().localScale = new Vector3(50f, 50f, 50f);
        confettiArray.Add(blastConfetti);
    }

    private string FormatNumber(int number)
    {
        if (number >= 1_000_000_000)
        {
            return $"{(double)number / 1_000_000_000:N1}B";
        }
        else if (number >= 1_000_000)
        {
            return $"{(double)number / 1_000_000:N1}M";
        }
        else if (number >= 1_000)
        {
            return $"{(double)number / 1_000:N1}k";
        }
        else
        {
            return number.ToString("N0");
        }
    }
}

