using GorillaStartupSoundsRemake;
using MelonLoader;
using MelonLoader.Utils;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

//not sure why we are using melonloader all of a sudden
[assembly: MelonInfo(typeof(Plugin), "GorillaStartupSoundsRemake", "1.0.0", "miniusbhater")]

namespace GorillaStartupSoundsRemake
{
    public class Plugin : MelonMod
    {
        private AudioSource GSSAudio;
        private GameObject obj;

        public override void OnInitializeMelon()
        {
            obj = new GameObject("GorillaStartupSounds");
            UnityEngine.Object.DontDestroyOnLoad(obj);

            GSSAudio = obj.AddComponent<AudioSource>();

            string sound = Path.Combine(MelonEnvironment.ModsDirectory, "GSS Sound", "StartSound.wav");

            if (File.Exists(sound))
            {
                MelonCoroutines.Start(Play(sound));
            }
            else
            {
                MelonLogger.Error($"Sound was not found in: {sound}");
            }
        }

        private IEnumerator Play(string path)
        {
            using (WWW www = new WWW("file://" + path))
            {
                yield return www;

                AudioClip aud = www.GetAudioClip(false, false);
                GSSAudio.clip = aud;
                GSSAudio.Play();
            }
        }
    }
}