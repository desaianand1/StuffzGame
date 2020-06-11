﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

public class AbilityMapper : DataMapper
{
    protected override string FileName { get => "abilities"; }
    protected override JObject JsonObject { get; }
    private List<JObject> AbilityList { get; }

    public AbilityMapper()
    {
        this.JsonObject = LoadJSON();
        this.AbilityList = (JsonObject[FileName] as JArray).Select(obj => obj as JObject).ToList();
    }

    public override object GetObjectById(int id)
    {
        JObject ability = AbilityList.Where(it => (int)it["id"] == id).SingleOrDefault();
        if (ability != null)
        {
            return new BasePokemonAbility
            {
                EffectEntries = JSONParsingUtil.GetEffectEntries(ability["effect_entries"]),
                FlavorText = ability["flavor_text"].Value<string>(),
                Id = ability["id"].Value<int>(),
                Name = ability["name"].Value<string>()
            };
        }

        UnityEngine.Debug.LogWarning($"No ability found for ability ID: {id}");
        return null;
    }
}