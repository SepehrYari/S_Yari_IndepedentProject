using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBox : MonoBehaviour
{
    public Color[] collisionColor; // The color to change to on collision
    public GameObject MasterBox;
    private Color originalColor; // Store the original color
    private Renderer Renderer;

    private void Start()
    {
        collisionColor = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.clear,
            Color.yellow,
            Color.magenta
        };
        Renderer = GetComponent<Renderer>();
        originalColor = Renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Renderer.material.color = collisionColor[Random.RandomRange(0, 5)];
    }

}
