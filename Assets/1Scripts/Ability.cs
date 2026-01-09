using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public AbilityData data;
    public int level;

    Player player;
    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.abilityIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.abilityName;
        textDesc.text = data.abilityDesc;
    }

    void Start()
    {
        player = GameManager.instance.player;
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + level;
    }

    public void OnClick()
    {
        switch(data.abilityType)
        {
            case AbilityData.AbilityType.Range:
                if (level == 0)
                {
                    player.autoMissile.gameObject.SetActive(true);
                } else
                {
                    player.autoMissile.damage += data.damages[level];
                    player.autoMissile.attackTime -= data.additions[level];
                }
                break;
            case AbilityData.AbilityType.Divine:
                if (level == 0)
                {
                    player.divineAura.gameObject.SetActive(true);
                } else
                {
                    player.divineAura.damage += data.damages[level];
                    player.divineAura.transform.localScale += Vector3.one * data.additions[level];
                }
                break;
            case AbilityData.AbilityType.Atk:
                player.SetShotDamage(data.damages[level]);
                break;
            case AbilityData.AbilityType.Shoe:
                player.moveSpeed += data.damages[level];
                break;
            case AbilityData.AbilityType.Heal:
                player.SetHealth(data.damages[0]);
                break;

        }
        level++;

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

}
