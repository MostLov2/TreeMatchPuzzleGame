using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    AudioClip[] clip;
    private void Awake()
    {
        clip = new AudioClip[10];
        clip[0] = Resources.Load<AudioClip>("Sound/Potion");
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(item != null)
            {
                if(item.itemType == Item.ItemType.SPENDINIVENTORYITEM)
                {
                    if (item.ItemQuantity >= 1)
                    {
                        item.ItemQuantity--;
                        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item.ItemQuantity.ToString();
                        if (item.itemName == "HeartPotion")
                        {
                            ItemManager.heartPotionCount -= 1;
                            StartCoroutine(ItemManager.instance.SetIHeartPotion());
                            MySqlSystem.energy++;
                            StartCoroutine(MySqlSystem.instance.SetEnergy(0));
                            SoundManager.instance.PlaySFX(clip, 0, 1, 1);
                            for (int i = 0; i < 5; i++)
                            {
                                GameObject heartEffect = LobbyEffectManager.instance.SetObject("HeartEffect");
                                heartEffect.transform.position = transform.position;
                            }
                        }
                    }
                }
            }
            else
            {
                return;
            }
        });
    }
}
