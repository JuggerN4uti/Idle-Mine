using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public Transform TextOrigin;
    public Rigidbody2D Body;
    public GameObject DamageTextObject;
    public DamageText DamageTextScript;

    public void PopUpText(float value) //bool crit = false)
    {
        TextOrigin.position = new Vector3(transform.position.x + Random.Range(-0.24f, 0.24f), transform.position.y + Random.Range(-0.24f, 0.24f), 0f);
        TextOrigin.rotation = Quaternion.Euler(TextOrigin.rotation.x, TextOrigin.rotation.y, Body.rotation + Random.Range(-20f, 20f));
        GameObject text = Instantiate(DamageTextObject, TextOrigin.position, transform.rotation);
        Rigidbody2D text_body = text.GetComponent<Rigidbody2D>();
        DamageTextScript = text.GetComponent(typeof(DamageText)) as DamageText;
        DamageTextScript.SetText(value); //crit);
        text_body.AddForce(TextOrigin.up * Random.Range(1.66f, 2.3f), ForceMode2D.Impulse);
    }
}
