﻿using System.Collections.Generic;
using Events;
 using GamePlay;
 using UnityEngine;
using UnityEngine.Serialization;

namespace Sounds
{
    public class RadioEvent : EventTrigger
    {
        private List<PlayerManager> _players = new List<PlayerManager>();
        private Vector3 _holePosition;
        private MusicManager _musicManager;
        private GameManager _gameManager;

        private int _lastIndex = -1;
        private bool _triggered;
        
        public float around = 1f; // Un mètre de distance
        

        protected override void EventInit()
        {
            _holePosition = GameObject.FindGameObjectWithTag("Finish")
                .gameObject.transform.position;
            _gameManager = FindObjectOfType<GameManager>();
        }
     
        protected override bool EventEndCondition()
        {
            return _gameManager.GameModeVar != GameManager.GameMode.Running;
        }
        
        private bool isBetween(float a, float b, float c)
        {
            return a > b && a < c;
        }

        protected override bool CheckEvent()
        {
            if (_lastIndex != _players.Count) // Avoid to reset all along the party
            {
                _players = new List<PlayerManager>(FindObjectsOfType<PlayerManager>());
                _lastIndex = _players.Count;
            }
            
            foreach (PlayerManager player in _players)
            {
                bool ret = true;
                Vector3 pos = _holePosition - player.transform.position;
                if (isBetween(pos.x, -around, around)
                    && isBetween(pos.z, -around, around))
                {
                    ret = !_triggered;
                    _triggered = true;
                    return ret;
                }
            }

            if (_triggered)
            {
                _triggered = false;
                return true;
            }
            return false;
        }

        protected override void Trigger()
        {
            if (!_musicManager)
            {
                _musicManager = FindObjectOfType<MusicManager>();
            }
            if (_triggered)
                _musicManager.ChangeMusic("Suspens");
            else
                _musicManager.ResetMusic();
        }
    }
}
