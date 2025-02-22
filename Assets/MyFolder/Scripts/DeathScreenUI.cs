using UnityEngine;

public class DeathScreenUI : MonoBehaviour
{

    [SerializeField] private GameObject _deathPanel; // Панель смерти
    [SerializeField] private DeathTrigger _deathTrigger; // Ссылка на здоровье персонажа

    private void Start()
    {
        _deathPanel.SetActive(false); // Скрываем панель при старте
        _deathTrigger.OnDeath += ShowDeathScreen; // Подписываемся на событие смерти
    }

    private void ShowDeathScreen()
    {
        _deathPanel.SetActive(true); // Активируем панель при смерти
    }

    private void OnDestroy()
    {
        _deathTrigger.OnDeath -= ShowDeathScreen; // Отписываемся от события
    }
}
