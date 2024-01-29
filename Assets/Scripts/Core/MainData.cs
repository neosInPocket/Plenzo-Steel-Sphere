using System;

[Serializable]
public class MainData
{
	public int diamonds = 10;
	public bool firstGamePlaying = true;

	public int score = 1;
	public int skinIndex = 1;
	public bool[] skinBought = { false, true, false };
	public bool musicEnabled = true;
	public bool sfxEnabled = true;
}
