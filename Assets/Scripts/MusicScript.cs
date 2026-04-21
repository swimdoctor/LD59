using System.Collections;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
	public AudioSource audio1;
	public AudioSource audio2;

	public void swapSong(int songIndex)
	{
		audio1.Stop();
		audio2.Stop();
		if(songIndex == 0)
		{
			audio1.Play();
		}
		if(songIndex == 1)
		{
			audio2.Play();
		}
	}
}
