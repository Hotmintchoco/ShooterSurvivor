using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public AbilityData data;
    public int level;

    Transform playerT;
    Player player;
    Image icon;
    Text textLevel;

    void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerT.GetComponent<Player>();
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.abilityIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
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
                    player.autoMissile.SetActive(true);
                }
                break;
            case AbilityData.AbilityType.Divine:
                if (level == 0)
                    player.divineAura.SetActive(true);

                break;
            case AbilityData.AbilityType.Atk:
                break;
            case AbilityData.AbilityType.Shoe:
                break;
        }
        level++;

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

}
