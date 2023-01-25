using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // cached component refs
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] GameObject sBlockBreakAnim;
    GameSession gameStatus;
    [SerializeField] Sprite[] hitSprites;

    Level level;

    // config params
    
    [SerializeField] Transform vFXLocation;
    [SerializeField] Transform breakAnimLocation;
    
    // state vars
    [SerializeField] int timesHit;
    [SerializeField] bool canBeHit = true;

    // script refs
    SoundHub soundHub;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        soundHub = FindObjectOfType<SoundHub>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageBlock(collision);
    }

    private void DamageBlock(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
        
    }

    private  void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array in " + gameObject.name);
        }
        
    }

    private void HandleHit()
    {
        if(canBeHit)
        {
            timesHit++;
            int maxHits = hitSprites.Length + 1;
            if (timesHit >= maxHits)
            {
                GetComponent<Animator>().Play("BlockShrink");
                level.BlockDestroyed();
                PlayBlockDamageSFX();
                //Destroy(gameObject);
                //TriggerSparklesVFX();
                //TriggerBreakAnim();
                gameStatus.AddToScore();
                canBeHit = false;
                Destroy(gameObject, .25f);
            }
            else
            {
                ShowNextHitSprite();
            }
        }
        
    }

    private void PlayBlockDamageSFX()
    {
        //SoundManager.PlaySound(SoundManager.Sound.blockHitSound0);
        soundHub.BreakSound();
        //AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, vFXLocation.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

    private void TriggerBreakAnim()
    {
        GameObject Break_Prefab = Instantiate(sBlockBreakAnim, breakAnimLocation.position, transform.rotation);
        Break_Prefab.SetActive(true);
    }
    //private void OnDestroy()
    //{
    //    if(level != null)
    //    {
    //        level.BlockDestroyed();
    //    }
    //}
}
