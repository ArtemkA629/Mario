using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _textWindow;
    [SerializeField] private Image _restartPanelImage;
    [SerializeField] private GameObject _restartPanel;
    [SerializeField] private Hero _hero;

    private static Color _winColor = Color.green;
    private static Color _loseColor = Color.red;

    private void Start()
    {
        _winColor.a = 0.5f;
        _loseColor.a = 0.5f;
    }

    private void SetWindowParam(string text, Color windowColor)
    {
        _textWindow.text = text;
        _restartPanelImage.color = windowColor;
        _restartPanel.SetActive(true);
    }

    private void StopGame()
    {
        _hero.enabled = false;
    }

    public void ShowWindow(bool isWinner)
    {
        if (isWinner)
            SetWindowParam("Вы победили", _winColor);
        else
            SetWindowParam("Вы проиграли", _loseColor);
        StopGame();
    }
}