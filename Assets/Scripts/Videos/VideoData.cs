using UnityEngine;
using UnityEngine.Video;　　//　<=　☆　宣言が異なるため、注意してください


[System.Serializable]
public class VideoData {
    public int videoNo;
    public VideoClip videoClip;
    public Sprite thumbnailSprite;  // ムービーのサムネイル画像
}