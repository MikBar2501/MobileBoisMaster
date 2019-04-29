using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAccel : MonoBehaviour
{
    public float smoothing = 0.8f;
    public float velocity = 5;
    private Vector3 currentAcceleration, initialAcceleration;
    public static Rigidbody2D rb;
    public static bool lose;
    public static int points;
    public static int deaths;
    public TextMeshProUGUI pointsTekst;
    public GameObject loseText;
    public static GameObject staticLoseText;

    void Awake()
    {
        staticLoseText = loseText;
    }

    void Start()
    {
        initialAcceleration = Input.acceleration;
        currentAcceleration = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        lose = false;
        points = 0;
        loseText.SetActive(false);
        rb.gravityScale = 0;
        StartCoroutine(AddPoints());
    }

    void Update()
    {
        currentAcceleration = Vector3.Lerp(currentAcceleration, Input.acceleration - initialAcceleration, Time.deltaTime / smoothing);
        rb.velocity = new Vector3(0,currentAcceleration.y,0) * velocity;
        //transform.Translate(0, currentAcceleration.y, 0);
        if(Input.GetKeyDown(KeyCode.Space)) {
            Death();
        }
    }

    IEnumerator AddPoints() {
        while(!lose) {
            points += 1;
            pointsTekst.text = "Points: " + points;
            yield return new WaitForSeconds(1);
        }
    }

    public static void Death() {
        lose = true;
        staticLoseText.SetActive(true);
        rb.gravityScale = 1;
        PlayServices.AddScoreToLeaderboard(GPGSIds.leaderboard_high_score__highest, points);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        deaths += 1;
        Death();
    }
}
