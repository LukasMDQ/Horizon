using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class RewardManager
{
    private static UI _ui;

    private static Dictionary<string, int> gameStats = new Dictionary<string, int>
    {
        { GameStats.JewelsCollected, 0 },
        { GameStats.PostersCollected, 0 },
        { GameStats.EnemiesKilled, 0 }
    };

    private static Dictionary<string, bool> rewards = new Dictionary<string, bool>
    {
        { "AllJewelsCollected", false },
        { "AllPostersCollected", false },
        { "FirstEnemiesKilled", false }
    };

    public static void RegisterStat(string statName, int initialValue = 0)
    {
        if (!gameStats.ContainsKey(statName))
        {
            gameStats[statName] = initialValue;
        }
    }

    public static void RegisterReward(string rewardName)
    {
        if (!rewards.ContainsKey(rewardName))
        {
            rewards[rewardName] = false;
        }
    }

    public static void AddGameStatCount(string gameStatsName)
    {
        if (gameStats.ContainsKey(gameStatsName))
        {
            gameStats[gameStatsName]++;
            ValidateRewardRequirement(gameStatsName);
        }
        else
        {
            Debug.LogWarning($"La estadística {gameStatsName} no está registrada.");
        }
    }

    public static void ValidateRewardRequirement(string gameStatsName)
    {
        if (!gameStats.ContainsKey(gameStatsName)) return;

        switch (gameStatsName)
        {
            case GameStats.JewelsCollected:
                if (gameStats[GameStats.JewelsCollected] >= 3)
                {
                    UnlockReward("AllJewelsCollected");
                }
                break;

            case GameStats.PostersCollected:
                if (gameStats[GameStats.PostersCollected] >= 3)
                {
                    UnlockReward("AllPostersCollected");
                }
                break;

            case GameStats.EnemiesKilled:
                if (gameStats[GameStats.EnemiesKilled] >= 5)
                {
                    UnlockReward("FirstEnemiesKilled");
                }
                break;
        }
    }

    public static void SetUIReference(UI ui)
    {
        _ui = ui;
    }

    public static void UnlockReward(string rewardName)
    {
        if (rewards.ContainsKey(rewardName) && !rewards[rewardName])
        {
            rewards[rewardName] = true;
            Debug.Log($"¡Logro desbloqueado: {rewardName}!");
            _ui?.AchievementShow(rewardName);
        }
    }

    public static bool IsRewardUnlocked(string rewardName)
    {
        return rewards.ContainsKey(rewardName) && rewards[rewardName];
    }
}
