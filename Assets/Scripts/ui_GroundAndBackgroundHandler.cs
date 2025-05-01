using UnityEngine;
using UnityEngine.UI;

public class ui_GroundAndBackgroundHandler : MonoBehaviour
{
    [System.Serializable]
    public class CustomGround
    {
        public Sprite background;
        public Sprite playerGround;
        public Sprite enemyGround;
    }

    [SerializeField] private Image playerGroundTarget;
    [SerializeField] private Image enemyGroundTarget;
    [SerializeField] private Image backgroundTarget;
    [SerializeField] private CustomGround[] grounds;

    void Awake()
    {
        SwapAllGrounds();
    }

    private void SwapAllGrounds()
    {
        int i = Random.Range(0, grounds.Length);
        playerGroundTarget.sprite = grounds[i].playerGround;
        enemyGroundTarget.sprite = grounds[i].enemyGround;
        backgroundTarget.sprite = grounds[i].background;
    }
}
