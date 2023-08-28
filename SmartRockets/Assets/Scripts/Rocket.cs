using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed;
    public Spawner spawner;
    public int[] turns;
    public float score;

    private void Start()
    {
        StartCoroutine(Edges());
    }

    private void Update()
    {
        if (!spawner.go) return;
        score = transform.localPosition.y;
        transform.eulerAngles += Vector3.forward * turns[spawner.frame];
        transform.position += speed * transform.up * Time.deltaTime;
    }
    IEnumerator Edges()
    {
        yield return new WaitUntil(() => Mathf.Abs(transform.localPosition.x)>610 || Mathf.Abs(transform.localPosition.y) > 540);
        speed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "rocket") speed = 0;
    }
}
