using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public enum CollectibleType
    {
        Red,
        Green,
        Blue,
        Random,
    }

    CollectibleType _collectibletype = CollectibleType.Random;

    private Transform player;
    private int randomNum;

    public SpriteRenderer sprite;
    public SpriteRenderer playerSprite;
    
    [SerializeField] CollectibleType collectibleType;
    [SerializeField] private Sprite[] sprites;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSprite = player.GetComponent<SpriteRenderer>();
        StartCoroutine(RandomCrystal());
    }

    IEnumerator RandomCrystal()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        yield return new WaitForSeconds(1f);
        if (_collectibletype == CollectibleType.Random)
        {
            randomNum = Random.Range (1, sprites.Length);
            //Debug.Log(randomNum);
            GetComponent<SpriteRenderer>().sprite = sprites[randomNum];
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomCrystal());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerController ChangeColor = other.GetComponent<PlayerController>();
        switch (collectibleType)
        {
            case CollectibleType.Red:
            playerSprite.color = Color.red;
            //Debug.Log("Picked up Red Crystal!");
            break;
            case CollectibleType.Green:
            playerSprite.color = Color.green;
            //Debug.Log("Picked up Green Crystal!");
            break;
            case CollectibleType.Blue:
            playerSprite.color = Color.blue;
            //Debug.Log("Picked up Blue Crystal!");
            break;
            case CollectibleType.Random:
            if (randomNum == 1)
            {
                playerSprite.color = Color.red;
            }
            else if (randomNum == 2)
            {
                playerSprite.color = Color.green;
            }
            else if (randomNum == 3)
            {
                playerSprite.color = Color.blue;
            }
            break;
            default:
            break;
        }
        Destroy(gameObject);
    }
}
