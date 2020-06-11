﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pokemon
{
    public BasePokemon BasePokemon { get; set; }
    public int CurrentLevel { get; set; }
    public PokemonNature Nature { get; set; }
    public Gender Gender { get; set; }
    public bool IsShiny { get; set; }
    public string Nickname { get; set; }
    public List<PokemonMove> LearnedMoves { get; set; }
    public PokemonAbility CurrentAbility { get; set; }
    public Item HeldItem { get; set; }
    private readonly System.Random random;

    public Pokemon(System.Random random)
    {
        this.random = random;
    }
    internal void CalculateStats()
    {
        int increasedStatId = Nature.IncreasedStat?.Id ?? -1;
        int decreasedStatId = Nature.DecreasedStat?.Id ?? -1;

        List<PokemonStat> persistentStats = BasePokemon.Stats.Where( it => it.BaseStat.IsBattleOnly == false).ToList();
        foreach (PokemonStat stat in persistentStats)
        {
            if (stat.IV == null)
            {
                UnityEngine.Debug.LogWarning($"Pokemon {BasePokemon.Name} ({BasePokemon.Id}) has no {stat.BaseStat.Name} IV. Generating a new IV value!");
                stat.IV = GenerateIV();
            }

            stat.CalculateStat(increasedStatId, decreasedStatId, CurrentLevel);
        }
    }

    private int GenerateIV()
    {
        int minIV = 1;
        int maxIV = 31;
        return random.Next(minIV, maxIV + 1);   // Random.Next(a,b) 'b' is exclusive
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"{BasePokemon.Name} ({Gender}) @ {HeldItem?.Name}");
        builder.AppendLine($"Lv. {CurrentLevel}");
        builder.AppendLine($"Ability: {CurrentAbility.BaseAbility.Name}");
        foreach (PokemonStat stat in BasePokemon.Stats)
        {
            if (stat.BaseStat.Name == StatName.HP)
            {
                builder.AppendLine($"{stat.BaseStat.Name}: {stat.CurrentValue}/{stat.CalculatedValue} ({stat.BaseValue})");
            }
            else
            {
                builder.Append($"{stat.BaseStat.Name}: {stat.CalculatedValue}({stat.BaseValue}) ");
            }
        }
        builder.Append($"\nTypes: ");
        foreach (PokemonType type in BasePokemon.Types)
        {
            builder.Append($"{type}  ");
        }
        builder.AppendLine($"\nNature: {Nature.Name}");
        foreach (PokemonMove move in LearnedMoves)
        {
            builder.AppendLine($"   - {move.BaseMove.Name}  ({move.CurrentPP}/{move.BaseMove.PP})");
        }

        builder.AppendLine($"Meta: {{ \nNickname: {Nickname}");
        builder.AppendLine($"is Shiny?: {IsShiny}");
        builder.AppendLine($"Evolves from?: {BasePokemon?.Species?.EvolvesFrom?.PokemonSpeciesId.ToString() ?? "N/A"} }}");
        return builder.ToString();
    }
}