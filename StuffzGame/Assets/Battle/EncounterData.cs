﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EncounterData : MonoBehaviour
    {
    // Start is called before the first frame update
    
        public Pokemon CurrentEnemyPokemon { get; set; }
        public List<Pokemon> Party { get; set; }
        public bool persistent;

        void Start()
        {
            if(persistent)
                DontDestroyOnLoad(gameObject);
        }
        void Update()
        {
        }
    }   



