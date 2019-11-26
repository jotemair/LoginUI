using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButtonsManager : MonoBehaviour
{
    private List<GameObject> _buttons = new List<GameObject>();

    private void Update()
    {
        if (_buttons.Count != CurrentUserManager.Instance.Characters.Count)
        {
            InitButtons();
        }
    }

    public void InitButtons()
    {
        foreach (var button in _buttons)
        {
            Destroy(button);
        }
        _buttons.Clear();

        GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs/CharacterButton");

        float offset = 0f;

        foreach (var charData in CurrentUserManager.Instance.Characters)
        {
            GameObject instance = Object.Instantiate(buttonPrefab, this.transform);

            RectTransform recttr = instance.GetComponent<RectTransform>();
            recttr.anchoredPosition = new Vector2(recttr.anchoredPosition.x, offset);
            offset -= 35f;

            CharacterButton characterButton = instance.GetComponentInChildren<CharacterButton>();
            characterButton.Name = charData.Item1;
            characterButton.Code = charData.Item2;
            _buttons.Add(instance);
        }
    }
}
