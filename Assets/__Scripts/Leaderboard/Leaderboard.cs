using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Leaderboard: ISavable
{
    public List<HighscoreEntry> HighscoreEntries;

    public Leaderboard() 
    {
        HighscoreEntries = new List<HighscoreEntry>();
    }

    public void Save()
    {
        SaveManager<Leaderboard>.Save(this, GetDataFileName());
    }

    public void Load()
    {
        Leaderboard loaded = SaveManager<Leaderboard>.Load(GetDataFileName());
        HighscoreEntries = loaded.HighscoreEntries;
    }

    public void UpdateScore(HighscoreEntry entry)
    {
        HighscoreEntry foundEntry = HighscoreEntries.FirstOrDefault(item => entry == item);

        if (foundEntry == null)
        {
            HighscoreEntries.Add(entry);
        }
        else
        {
            if(entry.Score > foundEntry.Score)
            {
                foundEntry.Score = entry.Score;
            }
        }

        SortHighscores();
        Save();
    }

    private void SortHighscores()
    {
        HighscoreEntries = HighscoreEntries.OrderByDescending(e => e.Score).ToList();
    }

    public string GetDataFileName()
    {
        return "Leaderboard.dat";
    }
}
