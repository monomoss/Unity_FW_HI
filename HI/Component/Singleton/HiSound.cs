using HI;
using HI.Abstract;
using HI.ExMethods;
using UnityEngine;

public class HiSound : HiSingletonMono<HiSound>
{
	public AudioSource AudioSource_BGM;
	public AudioSource AudioSource_Sound;
	public AudioClip[] AudioClips_BGM;
	public AudioClip[] AudioClips_Sound;

	public enum BGM
	{
		None = -1,
		Music1,
		Max
	}
	public enum SOUND
	{
		None = -1,
		Click,
		Success,
		Failure,
		Max
	}

	//override protected void Awake()
	//{
	//	base.Awake();
	//}

	private void OnEnable()
	{
		AudioSource_BGM.enabled = false;
		AudioSource_Sound.enabled = false;
	}

	public void PlayAudio_BGM(BGM _clip)
	{
		AudioSource_BGM.clip = AudioClips_BGM[(int)_clip];
		if (AudioSource_BGM.clip != null && AudioSource_BGM.enabled == true)
		{
			AudioSource_BGM.Play();
		}
	}

	public void PlayAudio_Sound(SOUND _clip)
	{
		AudioSource_Sound.clip = AudioClips_Sound[(int)_clip];
		if (AudioSource_Sound.clip != null && AudioSource_Sound.enabled == true)
		{
			AudioSource_Sound.Play();
		}
	}

	public void AudioON_BGM(bool _isMute)
	{
		AudioSource_BGM.enabled = _isMute;
	}

	public void AudioON_Sound(bool _isMute)
	{
		AudioSource_Sound.enabled = _isMute;
	}
}
