using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] GameObject backPanel;
    [SerializeField] Button backBtn;
    [Space]
    [SerializeField] TMP_Text difficultyText;
    [SerializeField] Transform leaderboardContainer;
    [SerializeField] LeaderboardRow leaderboardEntry;
    private void Awake()
    {
        backBtn.onClick.AddListener(HideLeaderboard);
    }

    private void OnEnable()
    {

        UpdateLeaderboard();
    }

    void HideLeaderboard()
    {
        backPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    void UpdateLeaderboard()
    {
        string difficultyName = Systems.Instance.difficultyLevel.DifficultyName;
        difficultyText.text = difficultyName;

        foreach (Transform child in leaderboardContainer.transform)
        {
            Destroy(child.gameObject);
        }

        int lp = 1;
        Leaderboard leaderboard = ScoreManager.Instance.Leaderboard;
        foreach (HighscoreEntry entry in leaderboard.HighscoreEntries)
        {
            if (entry.DifficultyName != difficultyName)
                continue;

            LeaderboardRow leaderboardRow = Instantiate(leaderboardEntry, leaderboardContainer);
            leaderboardRow.Init(lp, entry);
            lp++;
        }

    }
}