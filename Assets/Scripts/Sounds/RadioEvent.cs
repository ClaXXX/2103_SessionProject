﻿using System.Collections.Generic;
 using System.Linq;
 using Events;
 using GamePlay;
 using UnityEngine;
using UnityEngine.Serialization;

namespace Sounds
{
    public class RadioEvent : EventTrigger
    {
        private List<GameObject> _players = new List<GameObject>();
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
            if (EventEndCondition())
                return true;
            
            if (_lastIndex != _players.Count) // Avoid to reset all along the party
            {
                _players = GameObject.FindGameObjectsWithTag("Player").ToList(); // Get all components
                _lastIndex = _players.Count;
            }


            foreach (GameObject player in _players)
            {
                if (isBetween(player.transform.position.x, _holePosition.x - around, _holePosition.x + around)
                    && isBetween(player.transform.position.z, _holePosition.z - around, _holePosition.z + around))
                {
                    bool ret = !_triggered;
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
            if (EventEndCondition())
                return;
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
