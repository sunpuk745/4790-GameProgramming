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
    private int randomNum;
    public SpriteRenderer sprite;
    [SerializeField] CollectibleType collectibleType;
    [SerializeField] private Sprite[] sprites;

    private void Start() 
    {
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
            ChangeColor.ChangeRed();
            Destroy(this.gameObject);
            //Debug.Log("Picked up Red Crystal!");
            break;
            case CollectibleType.Green:
            ChangeColor.ChangeGreen();
            Destroy(this.gameObject);
            //Debug.Log("Picked up Green Crystal!");
            break;
            case CollectibleType.Blue:
            ChangeColor.ChangeBlue();
            Destroy(this.gameObject);
            //Debug.Log("Picked up Blue Crystal!");
            break;
            case CollectibleType.Random:
            if (randomNum == 1)
            {
                ChangeColor.ChangeRed();
                Destroy(this.gameObject);
            }
            else if (randomNum == 2)
            {
                ChangeColor.ChangeGreen();
                Destroy(this.gameObject);
            }
            else if (randomNum == 3)
            {
                ChangeColor.ChangeBlue();
                Destroy(this.gameObject);
            }
            break;
            default:
            break;
        }
    }
}
