using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private SOCollectibles collectibleObject;
    [SerializeField] private Transform collectibleTransform;
    [SerializeField] private ParticleSystem collectedEffect;
    [SerializeField] private ParticleSystem respawnEffect;

    private int randomNum;
    [SerializeField] private float respawnTimer = 4f;
    [SerializeField] private float moveEndPosition = 0.3f;

    private const string PlayerTag = "Player";

    private PlayerController player;
    [SerializeField] private CollectibleAudioController collectibleAudioController;

    public SpriteRenderer collectibleSprite;
    public SpriteRenderer playerSprite;
    private Collider2D collectibleCollider2D;
    
    [SerializeField] private Sprite[] sprites;
    
    private void Start() 
    {
        collectibleAudioController = gameObject.GetComponent<CollectibleAudioController>();
        collectibleTransform = GetComponent<Transform>();
        collectibleObject.GetCollectibleType();
        collectibleObject.GetRespawnable();
        collectibleObject.GetSprite();
        collectibleObject.GetOutlineSprite();
        //Debug.Log(collectibleObject.GetCollectibleType());
        //Debug.Log(collectibleObject.GetCollectible());
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerSprite = GameObject.Find("Model").GetComponent<SpriteRenderer>();
        collectibleSprite = GetComponent<SpriteRenderer>();
        collectibleCollider2D = GetComponent<Collider2D>();

        if (gameObject.activeSelf && collectibleObject.GetCollectibleType() == CollectibleType.Random)
        {
            StartCoroutine(RandomCrystal());
        }

        transform.DOMoveY(collectibleTransform.position.y + moveEndPosition, 5f).SetEase(Ease.InOutQuad).SetLoops(-1, loopType:LoopType.Yoyo);
    }

    public void EnableCollectible()
    {
        collectibleCollider2D.enabled = true;
        collectibleSprite.sprite = collectibleObject.GetSprite();
    }

    private IEnumerator RandomCrystal()
    {
        collectibleSprite.sprite = sprites[0];
        yield return new WaitForSeconds(1f);
        if (collectibleObject.GetCollectibleType() == CollectibleType.Random)
        {
            randomNum = Random.Range (1, sprites.Length);
            //Debug.Log(randomNum);
            collectibleSprite.sprite = sprites[randomNum];
        }
        yield return new WaitForSeconds(1f);
        //Debug.Log("GameObject is Active!");
        StartCoroutine(RandomCrystal());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        respawnEffect.gameObject.SetActive(true);
        respawnEffect.Stop();
        respawnEffect.Play();
        EnableCollectible();
        collectibleAudioController.PlayRespawnedSound();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag(PlayerTag))
        {
            switch (collectibleObject.GetCollectibleType())
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
                case CollectibleType.DoubleJump:
                collectibleCollider2D.enabled = false;
                Debug.Log("Picked up Double Jump!");
                player.canDoubleJump = true;
                break;
                default:
                break;
            }
            if (!collectibleObject.GetRespawnable())
            {
                //Debug.Log("Picked up Unrespawnable!");
                gameObject.SetActive(false);
            }
            else
            {
                //Debug.Log("Picked up Respawnable!");
                collectibleSprite.sprite = collectibleObject.GetOutlineSprite();
                StartCoroutine(Respawn());
            }
            collectedEffect.gameObject.SetActive(true);
            collectedEffect.Stop();
            collectedEffect.Play();
            collectibleAudioController.PlayCollectedSound();
        }
    }
}
