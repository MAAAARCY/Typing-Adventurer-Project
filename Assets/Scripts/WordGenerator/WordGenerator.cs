using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    [SerializeField] private WordDataBase WordDataBase;

    public string[] japanese_and_hiragana()
    {
        int n = Random.Range(0, 100);
        string[] s = { WordDataBase.GetWordDataLists()[n].GetShowName(), WordDataBase.GetWordDataLists()[n].GetHideName() };

        return s;
    }

    public List<List<string>> romaji_list(string japanese)
    {
        var list = new List<List<string>>();
        string jp = japanese;
        int convert_delta = 0; //小文字を変換した際の差分
        bool xtu_flag = false;

        for (int jp_i = 0; jp_i < jp.Length; jp_i++)
        {
            list.Add(new List<string>());

            string key_to_string = jp[jp_i].ToString(); //現在の文字
            string next_key_to_string = ""; //次の文字
            bool key_flag = false;
            
            //zya→zixyaなどの例外処理
            if(jp_i+1 != jp.Length)
            {
                next_key_to_string = jp[jp_i+1].ToString();
                key_flag = mp.ContainsKey(jp.Substring(jp_i,2));
                if(key_flag)
                {
                    string[] kts_value = mp[key_to_string];
                    string[] nkts_value = mp[next_key_to_string];
                    string substr = jp.Substring(jp_i,2);

                    for(int sb_j = 0; sb_j < mp[substr].Length; sb_j++)
                    {
                        string first_str = mp[substr][sb_j][0].ToString();

                        if (xtu_flag)
                        {
                            list[jp_i - convert_delta].Add(mp[substr][sb_j].Replace(first_str, ""));
                        }
                        else
                        {
                            list[jp_i - convert_delta].Add(mp[substr][sb_j]);
                        }
                    }
                    
                    for(int kts_j = 0; kts_j < kts_value.Length; kts_j++)
                    {
                        for(int nkts_k = 0; nkts_k < nkts_value.Length; nkts_k++)
                        {
                            string conbine_str = kts_value[kts_j] + nkts_value[nkts_k];
                            string first_str = conbine_str[0].ToString();

                            if (xtu_flag)
                            {
                                list[jp_i - convert_delta].Add(conbine_str.Replace(first_str, ""));
                            }
                            else
                            {
                                list[jp_i - convert_delta].Add(conbine_str);
                            }
                        }
                    }
                    convert_delta++;
                    jp_i++; //この処理がないとgyouzaがgyoxyouzaになってしまう
                    xtu_flag = false;
                    continue;
                }
            }
            
            //ん,っの例外処理
            switch(key_to_string)
            {
                case "ん":
                    if(jp_i+1 != jp.Length)
                    {
                        if(mp[next_key_to_string][0][0] != 'n')
                            list[jp_i - convert_delta].Add("n");
                    }
                    break;
                case "っ":
                    if(jp_i+1 != jp.Length)
                    {
                        for (int i = 0; i < mp[next_key_to_string].Length; i++)
                        {
                            list[jp_i - convert_delta].Add(mp[next_key_to_string][i][0].ToString() + mp[next_key_to_string][i][0].ToString());
                        }
                        xtu_flag = true;
                    }
                    break;
                default:
                    break;
            }

            //上記以外の処理
            for(int kts_j = 0; kts_j < mp[key_to_string].Length; kts_j++)
            {
                string first_str = mp[key_to_string][kts_j][0].ToString();
                if (xtu_flag)
                {
                    if (key_to_string == "っ")
                    {
                        for (int nkts_k = 0; nkts_k < mp[next_key_to_string].Length; nkts_k++)
                        {
                            list[jp_i - convert_delta].Add(mp[key_to_string][kts_j] + mp[next_key_to_string][nkts_k][0].ToString());
                        }
                    }
                    else
                    {
                        list[jp_i - convert_delta].Add(mp[key_to_string][kts_j].Replace(first_str, ""));
                    }
                }
                else
                {
                    list[jp_i - convert_delta].Add(mp[key_to_string][kts_j]);
                }

                if (key_to_string != "っ" && kts_j == mp[key_to_string].Length - 1)
                {
                    xtu_flag = false;
                }
            }
        }

        return list;
    }

    public string romaji_str(List<List<string>> r_list)
    {
        string s="";

        for(int r_i = 0; r_i < r_list.Count; r_i++)
        {
            s += r_list[r_i][0];
        }

        return s;
    }

    public List<string> romaji_first_list(List<List<string>> r_list)
    {
        var list = new List<string>();

        for(int r_i = 0; r_i < r_list[0].Count; r_i++)
        {
            list.Add(r_list[0][r_i]);
        }

        return list;
    }

    private Dictionary<int, string[]> wJP = new Dictionary<int, string[]>
    {
        {0, new string[2] {"カニ","かに"}},
        {1, new string[2] {"寿司","すし"}},
        {2, new string[2] {"車","くるま"}},
        {3, new string[2] {"スズメ","すずめ"}},
        {4, new string[2] {"健康","けんこう"}},
        {5, new string[2] {"たこ焼き","たこやき"}},
        {6, new string[2] {"図鑑","ずかん"}},
        {7, new string[2] {"マイコン","まいこん"}},
        {8, new string[2] {"ラーメン","らーめん"}},
        {9, new string[2] {"イカ","いか"}},
        {10, new string[2] {"タコ","たこ"}},
        {11, new string[2] {"カラス","からす"}},
        {12, new string[2] {"ハンバーグ","はんばーぐ"}},
        {13, new string[2] {"電池","でんち"}},
        {14, new string[2] {"財布","さいふ"}},
        {15, new string[2] {"椅子","いす"}},
        {16, new string[2] {"マスク","ますく"}},
        {17, new string[2] {"マウス","まうす"}},
        {18, new string[2] {"毛布","もうふ"}},
        {19, new string[2] {"団子","だんご"}},
        {20, new string[2] {"卵","たまご"}},
        {21, new string[2] {"かっこ","かっこ"}},
        {22, new string[2] {"もんじゃ","もんじゃ"}},
        {23, new string[2] {"神社","じんじゃ"}},
        {24, new string[2] {"じゃんけん","じゃんけん"}},
        {25, new string[2] {"茶碗","ちゃわん"}},
        {26, new string[2] {"積み木", "つみき"}},
        {27, new string[2] {"うどん", "うどん"}},
        {28, new string[2] {"英語", "えいご"}},
        {29, new string[2] {"電卓", "でんたく"}},
        {30, new string[2] {"スマホ", "すまほ"}},
        {31, new string[2] {"筆箱","ふでばこ"}},
        {32, new string[2] {"ペン","ぺん"}},
        {33, new string[2] {"カード","かーど"}},
        {34, new string[2] {"イヤホン","いやほん"}},
        {35, new string[2] {"机","つくえ"}},
        {36, new string[2] {"リモコン","りもこん"}},
        {37, new string[2] {"レモン","れもん"}},
        {38, new string[2] {"ゲーム","げーむ"}},
        {39, new string[2] {"はさみ","はさみ"}},
        {40, new string[2] {"スーツ","すーつ"}},
        {41, new string[2] {"エプロン","えぷろん"}},
        {42, new string[2] {"ペンチ","ぺんち"}},
        {43, new string[2] {"虎","とら"}},
        {44, new string[2] {"本","ほん"}},
        {45, new string[2] {"サーバー","さーばー"}},
        {46, new string[2] {"ソーダ","そーだ"}},
        {47, new string[2] {"時計","とけい"}},
        {48, new string[2] {"鰯","いわし"}},
        {49, new string[2] {"ベルト","べると"}},
        {50, new string[2] {"ドライバー","どらいばー"}},
        {51, new string[2] {"のれんに腕押し","のれんにうでおし"}},
        {52, new string[2] {"一石二鳥","いっせきにちょう"}},
        {53, new string[2] {"ベストセラー","べすとせらー"}},
        {54, new string[2] {"スルメイカ","するめいか"}},
        {55, new string[2] {"虎の威を借りる狐","とらのいをかりるきつね"}},
        {56, new string[2] {"収納ケース","しゅうのうけーす"}},
        {57, new string[2] {"玉手箱","たまてばこ"}},
        {58, new string[2] {"卒業研究","そつぎょうけんきゅう"}},
        {59, new string[2] {"秋の扇","あきのおうぎ"}},
        {60, new string[2] {"麻の中の蓬","あさのなかのよもぎ"}},
        {61, new string[2] {"後の祭り","あとのまつり"}},
        {62, new string[2] {"虻蜂取らず","あぶはちとらず"}},
        {63, new string[2] {"急がば回れ","いそがばまわれ"}},
        {64, new string[2] {"海老で鯛を釣る","えびでたいをつる"}},
        {65, new string[2] {"鬼に金棒","おににかなぼう"}},
        {66, new string[2] {"河童の川流れ","かっぱのかわながれ"}},
        {67, new string[2] {"猿も木から落ちる","さるもきからおちる"}},
        {68, new string[2] {"釈迦に説法","しゃかにせっぽう"}},
        {69, new string[2] {"知らぬが仏","しらぬがほとけ"}},
        {70, new string[2] {"損して得取る","そんしてとくとる"}},
        {71, new string[2] {"ドップラー効果", "どっぷらーこうか"}},
        {72, new string[2] {"鉄拳制裁", "てっけんせいさい"}},
        {73, new string[2] {"ファッション", "ふぁっしょん"}},
        {74, new string[2] {"ファンクション", "ふぁんくしょん"}},
        {75, new string[2] {"チョコレート", "ちょこれーと"}}
    };

    private Dictionary<string, string[]> mp = new Dictionary<string, string[]>
    {
        {"あ", new string[1] {"a"}},
        {"い", new string[2] {"i", "yi"}},
        {"う", new string[3] {"u", "wu", "whu"}},
        {"え", new string[1] {"e"}},
        {"お", new string[1] {"o"}},
        {"か", new string[2] {"ka", "ca"}},
        {"き", new string[1] {"ki"}},
        {"く", new string[3] {"ku", "cu", "qu"}},
        {"け", new string[1] {"ke"}},
        {"こ", new string[2] {"ko", "co"}},
        {"さ", new string[1] {"sa"}},
        {"し", new string[3] {"si", "ci", "shi"}},
        {"す", new string[1] {"su"}},
        {"せ", new string[2] {"se", "ce"}},
        {"そ", new string[1] {"so"}},
        {"た", new string[1] {"ta"}},
        {"ち", new string[2] {"ti", "chi"}},
        {"つ", new string[2] {"tu", "tsu"}},
        {"て", new string[1] {"te"}},
        {"と", new string[1] {"to"}},
        {"な", new string[1] {"na"}},
        {"に", new string[1] {"ni"}},
        {"ぬ", new string[1] {"nu"}},
        {"ね", new string[1] {"ne"}},
        {"の", new string[1] {"no"}},
        {"は", new string[1] {"ha"}},
        {"ひ", new string[1] {"hi"}},
        {"ふ", new string[2] {"hu", "fu"}},
        {"へ", new string[1] {"he"}},
        {"ほ", new string[1] {"ho"}},
        {"ま", new string[1] {"ma"}},
        {"み", new string[1] {"mi"}},
        {"む", new string[1] {"mu"}},
        {"め", new string[1] {"me"}},
        {"も", new string[1] {"mo"}},
        {"や", new string[1] {"ya"}},
        {"ゆ", new string[1] {"yu"}},
        {"よ", new string[1] {"yo"}},
        {"ら", new string[1] {"ra"}},
        {"り", new string[1] {"ri"}},
        {"る", new string[1] {"ru"}},
        {"れ", new string[1] {"re"}},
        {"ろ", new string[1] {"ro"}},
        {"わ", new string[1] {"wa"}},
        {"を", new string[1] {"wo"}},
        {"が", new string[1] {"ga"}},
        {"ぎ", new string[1] {"gi"}},
        {"ぐ", new string[1] {"gu"}},
        {"げ", new string[1] {"ge"}},
        {"ご", new string[1] {"go"}},
        {"ん", new string[2] {"nn","xn"}},
        {"ざ", new string[1] {"za"}},
        {"じ", new string[2] {"zi", "ji"}},
        {"ず", new string[1] {"zu"}},
        {"ぜ", new string[1] {"ze"}},
        {"ぞ", new string[1] {"zo"}},
        {"だ", new string[1] {"da"}},
        {"ぢ", new string[1] {"di"}},
        {"づ", new string[1] {"du"}},
        {"で", new string[1] {"de"}},
        {"ど", new string[1] {"do"}},
        {"ば", new string[1] {"ba"}},
        {"び", new string[1] {"bi"}},
        {"ぶ", new string[1] {"bu"}},
        {"べ", new string[1] {"be"}},
        {"ぼ", new string[1] {"bo"}},
        {"ぱ", new string[1] {"pa"}},
        {"ぴ", new string[1] {"pi"}},
        {"ぷ", new string[1] {"pu"}},
        {"ぺ", new string[1] {"pe"}},
        {"ぽ", new string[1] {"po"}},
        {"ぁ", new string[2] {"xa", "la"}},
        {"ぃ", new string[2] {"xi", "li"}},
        {"ぅ", new string[2] {"xu", "lu"}},
        {"ぇ", new string[2] {"xe", "le"}},
        {"ぉ", new string[2] {"xo", "lo"}},
        {"ゃ", new string[2] {"xya", "lya"}},
        {"ゅ", new string[2] {"xyu", "lyu"}},
        {"ょ", new string[2] {"xyo", "lyo"}},
        {"っ", new string[2] {"xtu", "ltu"}},
        {"うぁ", new string[1] {"wha"}},
        {"うぃ", new string[2] {"wi", "whi"}},
        {"うぇ", new string[2] {"we", "whe"}},
        {"うぉ", new string[1] {"who"}},
        {"きゃ", new string[1] {"kya"}},
        {"きぃ", new string[1] {"kyi"}},
        {"きゅ", new string[1] {"kyu"}},
        {"きぇ", new string[1] {"kye"}},
        {"きょ", new string[1] {"kyo"}},
        {"ぎゃ", new string[1] {"gya"}},
        {"ぎぃ", new string[1] {"gyi"}},
        {"ぎゅ", new string[1] {"gyu"}},
        {"ぎぇ", new string[1] {"gye"}},
        {"ぎょ", new string[1] {"gyo"}},
        {"しゃ", new string[2] {"sya","sha"}},
        {"しぃ", new string[1] {"syi"}},
        {"しゅ", new string[2] {"syu","shu"}},
        {"しぇ", new string[2] {"sye", "she"}},
        {"しょ", new string[2] {"syo", "sho"}},
        {"じゃ", new string[3] {"ja", "zya", "jya"}},
        {"じぃ", new string[2] {"zyi", "jyi"}},
        {"じゅ", new string[3] {"ju", "zyu", "jyu"}},
        {"じぇ", new string[3] {"je", "zye", "jye"}},
        {"じょ", new string[3] {"jo", "zyo", "jyo"}},
        {"ちゃ", new string[3] {"tya", "cya", "cha"}},
        {"ちぃ", new string[2] {"tyi", "cyi"}},
        {"ちゅ", new string[3] {"tyu", "cyu", "chu"}},
        {"ちぇ", new string[3] {"tye", "cye", "che"}},
        {"ちょ", new string[3] {"tyo", "cyo", "cho"}},
        {"てゃ", new string[1] {"tha"}},
        {"てぃ", new string[1] {"thi"}},
        {"てゅ", new string[1] {"thu"}},
        {"てぇ", new string[1] {"the"}},
        {"てょ", new string[1] {"tho"}},
        {"でゃ", new string[1] {"dha"}},
        {"でぃ", new string[1] {"dhi"}},
        {"でゅ", new string[1] {"dhu"}},
        {"でぇ", new string[1] {"dhe"}},
        {"でょ", new string[1] {"dho"}},
        {"にゃ", new string[1] {"nya"}},
        {"にぃ", new string[1] {"nyi"}},
        {"にゅ", new string[1] {"nyu"}},
        {"にぇ", new string[1] {"nye"}},
        {"にょ", new string[1] {"nyo"}},
        {"ひゃ", new string[1] {"hya"}},
        {"ひぃ", new string[1] {"hyi"}},
        {"ひゅ", new string[1] {"hyu"}},
        {"ひぇ", new string[1] {"hye"}},
        {"ひょ", new string[1] {"hyo"}},
        {"びゃ", new string[1] {"bya"}},
        {"びぃ", new string[1] {"byi"}},
        {"びゅ", new string[1] {"byu"}},
        {"びぇ", new string[1] {"bye"}},
        {"びょ", new string[1] {"byo"}},
        {"ぴゃ", new string[1] {"pya"}},
        {"ぴぃ", new string[1] {"pyi"}},
        {"ぴゅ", new string[1] {"pyu"}},
        {"ぴぇ", new string[1] {"pye"}},
        {"ぴょ", new string[1] {"pyo"}},
        {"ふぁ", new string[1] {"fa"}},
        {"ふぃ", new string[1] {"fi"}},
        {"ふぇ", new string[1] {"fe"}},
        {"ふぉ", new string[1] {"fo"}},
        {"ヴァ", new string[1] {"va"}},
        {"ヴィ", new string[1] {"vi"}},
        {"ヴ", new string[1] {"vu"}},
        {"ヴェ", new string[1] {"ve"}},
        {"ヴォ", new string[1] {"vo"}},
        {"みゃ", new string[1] {"mya"}},
        {"みぃ", new string[1] {"myi"}},
        {"みゅ", new string[1] {"myu"}},
        {"みぇ", new string[1] {"mye"}},
        {"みょ", new string[1] {"myo"}},
        {"りゃ", new string[1] {"rya"}},
        {"りぃ", new string[1] {"ryi"}},
        {"りゅ", new string[1] {"ryu"}},
        {"りぇ", new string[1] {"rye"}},
        {"りょ", new string[1] {"ryo"}},
        {"！", new string[1] {"!"}},
        {"？", new string[1] {"?"}},
        {"ー", new string[1] {"-"}}
    };
}
