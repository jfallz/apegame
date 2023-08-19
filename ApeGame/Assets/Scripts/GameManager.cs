using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip2;

    public CannonFire scriptRef;
    public int distanceTraveled, distanceBest;

    // ----
    // All time max statistics:
    public float maxDistance = 0f;
    public float maxSpeed = 0f;
    public float maxAltitude = 0f;
    // ----

    // ---- 
    // Current run statistics:
    public float currMaxDistance = 0f;
    public float currMaxSpeed = 0f;
    public float currMaxAltitude = 0f;
    // ----
    private int startingDistance;
    [SerializeField] public float currency = 0f;
    public GameObject prefabToInstantiate;
    public GameObject playerMenu;
    public bool restart = false; // if true, will restart the player and then reset bool to false
    public float TimeT = 0f;
    private bool inFlight = false; 
    private bool launched = false;
    public GameObject speedometer, newBest;
    public float min = 317f;
    public float max = 45f;
    private bool dead = false;
    // ----
    public GameObject[] confettiPrefabs;
    // ----
    public AudioClip[] ButtonSounds;
    // ----
    private List<GameObject> confettiArray = new List<GameObject>();
    public void Start() {
        distanceTraveled = 0;
        maxDistance = 0;
        maxSpeed = 0;
        maxAltitude = 0;
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
        int speed = (int) (p.GetComponent<Rigidbody>().velocity.magnitude / 3f);
        distanceTraveled = (int)(((p.transform.position.z) - startingDistance) / 3f);
        TextMeshProUGUI d = GameObject.Find("DISTANCE").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI s = GameObject.Find("SPEED").GetComponent<TextMeshProUGUI>();
        s.text = speed + "m/s";
        d.text = distanceTraveled > 9999 ? FormatNumber(distanceTraveled) + "m" : distanceTraveled + "m";
        int altitude = (int) ((p.transform.position.y - 121.7634f  ) / 3f);

        if(speed > currMaxSpeed)
            currMaxSpeed = (float)speed;

        if(distanceTraveled > currMaxDistance)
            currMaxDistance = (float)distanceTraveled;

        if(altitude > currMaxAltitude)
            currMaxAltitude = (float)altitude;
    }
    public void Death() {
        currency += distanceTraveled * .25f;
        TextMeshProUGUI c = GameObject.Find("CURRENCY").GetComponent<TextMeshProUGUI>();
        c.text = ((int)currency).ToString();
        playerMenu.SetActive(true);
        Scores();
        audioSource.PlayOneShot(clip2);
        // if new personal best
        if(currMaxDistance > maxDistance) {
            maxDistance = currMaxDistance;
            Confetti();
            playerMenu.GetComponent<Animator>().Play("PopIn");
            newBest.SetActive(true);
        } else if(currMaxAltitude > maxAltitude) {
            maxAltitude = currMaxAltitude;
        } else if(currMaxSpeed > maxSpeed) {
            maxSpeed = currMaxSpeed;
        }
        currMaxSpeed = 0f;
        currMaxAltitude = 0f;
        currMaxSpeed = 0f;
        dead = true;
    }

    public void ShopMenu() {
    }

    public void Restart() {
        print("restarting");
        playerMenu.SetActive(false);
        newBest.SetActive(false);
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

    public void Scores() {
        // max distance, height, speed, and bonus will go here
        TextMeshProUGUI distance = GameObject.Find("DISTANCEend").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI speed = GameObject.Find("SPEEDend").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI altitude = GameObject.Find("ALTITUDEend").GetComponent<TextMeshProUGUI>();

        distance.text = "Distance: " + currMaxDistance + "m";
        speed.text = "Speed: " + currMaxSpeed + "m/s";
        altitude.text = "Altitude: " + currMaxAltitude + "m";
    }

    public void Confetti() {
        
        audioSource.PlayOneShot(clip);
        
        GameObject blastConfetti = Instantiate(confettiPrefabs[0], GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
        blastConfetti.GetComponent<Transform>().localScale = new Vector3(50f, 50f, 50f);
        confettiArray.Add(blastConfetti);
    }

    public void ButtonPress1() {
        audioSource.PlayOneShot(ButtonSounds[0]);
    }

    public void ButtonPress2() {
        audioSource.PlayOneShot(ButtonSounds[1]);
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

