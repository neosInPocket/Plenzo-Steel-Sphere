using System;

[Serializable]
public class MainData
{
	public int diamonds = 10;
	public bool firstGamePlaying = true;

	public int score = 1;
	public int skinIndex;
	public bool[] skinBought = { true, false, false };
	public bool musicEnabled = false;
	public bool sfxEnabled = false;
}
