using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefData
{
    public static int CurWeaponId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_WEAPON_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_WEAPON_ID, 0);
    }

    public static int CurHairId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_SKINHAIR_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_SKINHAIR_ID, -1);
    }
    public static int CurPantId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_SKINPANT_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_SKINPANT_ID, -1);
    }
    public static int CurShieldId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_SKINSHIELD_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_SKINSHIELD_ID, -1);
    }


    public static int Coins
    {
        set => PlayerPrefs.SetInt(PrefConst.COIN_KEY, value);

        get => PlayerPrefs.GetInt(PrefConst.COIN_KEY, 200);
    }

    public static string NamePlayer
    {
        set => PlayerPrefs.SetString(PrefConst.NAME_PLAYER, value);

        get => PlayerPrefs.GetString(PrefConst.NAME_PLAYER, "???");
    }


    public static void SetBool(string key, bool isOn)
    {
        if (isOn) PlayerPrefs.SetInt(key, 1);

        else PlayerPrefs.SetInt(key, 0);
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}
public class PrefConst
{
    public const string WEAPON_PEFIX = "weapon_";
    public const string SKINHAIR_PEFIX = "hair_";
    public const string SKINSHIELD_PEFIX = "shield_";
    public const string SKINPANT_PEFIX = "pant_";

    public const string CUR_WEAPON_ID = "cur_weapon_id";
    public const string CUR_SKINHAIR_ID = "cur_hair_id";
    public const string CUR_SKINSHIELD_ID = "cur_shield_id";
    public const string CUR_SKINPANT_ID = "cur_pant_id";

    public const string COIN_KEY = "coins";

    public const string NAME_PLAYER = "namePlayer";



}