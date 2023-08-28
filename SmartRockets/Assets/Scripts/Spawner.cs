using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int size, count, frame;
    [SerializeField] Rocket rocket;
    List<Rocket> rockets = new List<Rocket>();
    public int steering;
    public bool go;
    [SerializeField] int generation;
    Rocket bride, groom, child;

    private void Start()
    {
        child = new Rocket();
        bride = new Rocket();
        groom = new Rocket();
        generation = 0;
        go = false;
        Spawn();
    }
    void Spawn()
    {
        frame = 0;
        for(int i = 0; i < count; i++)
        {
            Rocket newRocket = Instantiate(rocket, transform.parent);
            newRocket.transform.localPosition = new Vector2(0, -540);
            newRocket.spawner = this;
            rockets.Add(newRocket);
            if(generation == 0)
            {
                newRocket.turns = new int[size];
                for (int o = 0; o < newRocket.turns.Length; o++)
                {
                    newRocket.turns[o] = Random.Range(-steering, steering + 1);
                }
            }
            else
            {
                newRocket.turns = Randomize(child.turns);
            }
        }
        generation++;
        go = true;
    }
    private void Update()
    {
        if (!go) return;
        if (frame >= size-1) Restart();
        frame++;
    }
    int[] Randomize(int[] current)
    {
        int[] c = current;
        for(int i = 0; i < c.Length; i++)
        {
            if(Random.value < 0.1f)
            {
                Debug.Log(i);
                c[i] = Random.Range(-steering, steering + 1);
            }
        }
        return c;
    }
    void Restart()
    {
        go = false;
        NewlyWeds();
        
        if (generation == 2)
        {
            return;
        }
        else{
            for (int i = rockets.Count - 1; i >= 0; i--)
            {
                Destroy(rockets[i].gameObject);
                rockets.RemoveAt(i);
            }
        }
        Spawn();
    }
    void NewlyWeds()
    {
        child.turns = new int[size];
        bride.turns = new int[size];
        groom.turns = new int[size];
        foreach (Rocket plane in rockets)
        {
            if(plane.score > bride.score)
            {
                if (plane.score > groom.score) 
                { 
                    groom = plane;
                    continue;
                }
                bride = plane;
            }
        }
        for(int i = 0; i < Mathf.FloorToInt(bride.turns.Length / 2); i++)
        {
            child.turns[i] = bride.turns[i];
        }
        for (int i = Mathf.FloorToInt(groom.turns.Length / 2); i < Mathf.FloorToInt(groom.turns.Length); i++)
        {
            child.turns[i] = groom.turns[i];
        }
    }
}
