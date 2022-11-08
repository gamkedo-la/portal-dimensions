using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class MusicPlayer : MonoBehaviour
    {
        List<AudioSource> layerSources = new List<AudioSource>();

        private void Awake()
        {
            //set up our audio sources
            CreateLayerSources();    
        }

        private void CreateLayerSources()
        {
            //create and attach a few audiosources
            for(int i = 0; i < MusicManager.MaxLayerCount; i++)
            {
                layerSources.Add(gameObject.AddComponent<AudioSource>());
                //set up the audiosource
                layerSources[i].playOnAwake = false;
                layerSources[i].loop = true;
            }
        }

        public void Play(MusicEvent musicEvent, float fadeTime)
        {
            for(int i = 0; i < layerSources.Count 
                && (i < musicEvent.MusicLayers.Length); i++)
            {
                //if we have content in music layer, assign it
                if(musicEvent.MusicLayers[i] != null)
                {
                    layerSources[i].clip = musicEvent.MusicLayers[i];
                    layerSources[i].Play();
                }
            }
        }
    }
}


