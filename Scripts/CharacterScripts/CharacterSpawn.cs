using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [Header("味方キャラ")]
    public GameObject bg;                   // キャラクターBG
    public GameObject bs;                   // キャラクターBS
    public GameObject ba;                   // キャラクターBA
    public GameObject bw;                   // キャラクターBW
    public GameObject bgu;                  // キャラクターBGU
    public GameObject bsu;                  // キャラクターBSU
    public GameObject bau;                  // キャラクターBAU
    public GameObject bwu;                  // キャラクターBWU

    [Header("キャラクター出現位置")]
    public Transform characterSpawnPos;     // キャラクター出現位置
    [Header("BW出現位置")]
    public Transform bwSpawnPos;            // キャラクター(BW)出現位置

    protected float bgTimer;                // BGの計算用タイマー
    protected float bsTimer;                // BSの計算用タイマー
    protected float baTimer;                // BAの計算用タイマー

    protected float bguTimer;               // BGUの計算用タイマー
    protected float bsuTimer;               // BSUの計算用タイマー
    protected float bauTimer;               // BAUの計算用タイマー

    public bool bwFlg = false;              // BWが存在しているかどうかのフラグ
    public bool bwuFlg = false;             // BWUが存在しているかどうかのフラグ

    protected float bgIntarval = 2.0f;      // BGの生成クールタイム
    protected float bsIntarval = 5.0f;      // BSの生成クールタイム
    protected float baIntarval = 7.0f;      // BAの生成クールタイム
    protected float bguIntarval = 10.0f;    // BGUの生成クールタイム
    protected float bsuIntarval = 24.0f;    // BSUの生成クールタイム
    protected float bauIntarval = 30.0f;    // BAUの生成クールタイム

    [Header("現在の味方キャラクターの数")]
    public int characterCount = 0;          // 現在の味方キャラクターの数
    protected int characterCountMax = 10;   // 味方キャラクターの場に出せる最大数

    [Header("死亡した味方の数")]
    public int deathCharacterCount = 0;          // 死亡した味方の数

    [Header("死亡した味方の数の最大数")]
    public int deathCharacterCountMax = 10;      // 死亡した味方の数の最大数

    [Header("テキスト用死亡数")]
    public int deathCharacterCountText = 0;      // テキスト用死亡数

    [Header("BUW用の死亡した味方の数の最大数")]
    public int deathCharacterCountMaxBWU = 20;   // BWU用の死亡した味方の数の最大数

    [Header("BWU用のテキスト用死亡数")]
    public int deathCharacterCountTextBWU = 0;   // BWU用のテキスト用死亡数
}
