using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Ability[] abilities;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        abilities = GetComponentsInChildren<Ability>();
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        abilities[index].OnClick();
    }

    void Next()
    {
        foreach (Ability ability in abilities)
        {
            ability.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while(true)
        {
            ran[0] = Random.Range(0, abilities.Length);
            ran[1] = Random.Range(0, abilities.Length);
            ran[2] = Random.Range(0, abilities.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Ability ability = abilities[ran[index]];

            if (ability.level == ability.data.damages.Length)
            {
                abilities[4].gameObject.SetActive(true);
            } else
            {
                ability.gameObject.SetActive(true);
            }
        }
    }
}
