using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public HitPoints hitPoints;
    [SerializeField] private HealthBar healthBarPrefap;
    private HealthBar HealthBar;
    [SerializeField] private Inventory inventoryPrefab;
    Inventory inventory;
    public bool livePlayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {


            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {

                bool shouldDisappear = false;
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);

                }
            }
        }
    }
    
    public bool AdjustHitPoints( int quantity)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + quantity; 
            print(" Adjusted HP by: " + quantity + ". New value: " + hitPoints.value); 
            return true;
        }
        return false;
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints.value = hitPoints.value - damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                if (livePlayer == false)
                    LoadLevelScene();

                break;
            }
            if (interval >= float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
            
        }
    }
    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(HealthBar.gameObject);
        Destroy(inventory.gameObject);
        livePlayer = false;
    }
    public override void ResetCharacter()
    {
        livePlayer = true;
        HealthBar = Instantiate(healthBarPrefap);
        HealthBar.character = this;
        inventory = Instantiate(inventoryPrefab);
        hitPoints.value = startingHitPoints;
    }
    private void OnEnable()
    {
        ResetCharacter();
    }
    public void LoadLevelScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
