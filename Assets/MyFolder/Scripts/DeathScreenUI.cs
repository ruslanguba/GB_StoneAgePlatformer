using UnityEngine;

public class DeathScreenUI : MonoBehaviour
{

    [SerializeField] private GameObject _deathPanel; // ������ ������
    [SerializeField] private DeathTrigger _deathTrigger; // ������ �� �������� ���������

    private void Start()
    {
        _deathPanel.SetActive(false); // �������� ������ ��� ������
        _deathTrigger.OnDeath += ShowDeathScreen; // ������������� �� ������� ������
    }

    private void ShowDeathScreen()
    {
        _deathPanel.SetActive(true); // ���������� ������ ��� ������
    }

    private void OnDestroy()
    {
        _deathTrigger.OnDeath -= ShowDeathScreen; // ������������ �� �������
    }
}
