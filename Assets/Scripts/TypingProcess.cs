using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Managers;
using Questions;
using Enemies.Stage1;

public class TypingProcess : MonoBehaviour
{
    [SerializeField] private BossHPManager BHM;
    [SerializeField] private RepositionQuestions RPQ;
    [SerializeField] private ResizeQuestionsScale RQS;
    [SerializeField] private WordGenerator WG;
    [SerializeField] private EyeAttack EA;
    [SerializeField] private GameObject[] Questions; //問題
    [SerializeField] private GameObject[] questions;
    [SerializeField] private GameObject[] japanese; //日本語
    [SerializeField] private GameObject[] romaji; //ローマ字
    [SerializeField] private GameObject[] choice_mark;
    [SerializeField] private AudioClip[] SE;

    private AudioSource SESource;

    private string correct_romaji_log; //正しく打ったローマ字のログ
    private List<List<string>> romaji_list; //ローマ字の変換パターンリスト
    private List<List<string>> romaji_first_list; //ローマ字の変換パターンリストの一文字目
    private Dictionary<int, string[]> questions_dictionary; //問題のディレクトリ
    private int[] use_index_array; //現在使っているインデックス番号配列
    private int hiragana_index; //現在打っている文字のインデックス
    private int hiragana_length; //文字(ひらがな)の長さ
    private int romaji_count; //現在打っているローマ字の場所
    private int romaji_length; //現在打っている文字のローマ字の長さ
    private int select_question_number; //現在打っている問題番号
    private int typo_count; //ミスした回数
    private int total_count; //打った回数の合計
    private bool nn_flag; //んの例外処理に使用
    private bool question_select_flag; //どの問題を選択するかのフラグ
    private bool ForciblyTerminateFlag;

    public void ForciblyTerminateTyping()
    {
        ForciblyTerminateFlag = true;
    }

    public int[] GetTypoAndTotal()
    {
        int[] array = new int[2] { total_count, typo_count };
        return array;
    }

    private void word_set(int select_number)
    {
        Debug.Log(questions_dictionary[select_number][1]);
        romaji_list = WG.romaji_list(questions_dictionary[select_number][1]); //リスト作成
        use_index_array = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        romaji_count = 0;
        nn_flag = false;
        hiragana_length = questions_dictionary[select_number][1].Length;
        //小文字の文字数のみ削除（WordGeneratorの関係で）
        hiragana_length -= komoji_counter(questions_dictionary[select_number][1]);
    }

    private int komoji_counter(string hiragana)
    {
        int komoji_count = 0;

        foreach (char c in hiragana)
        {
            switch (c)
            {
                case 'ぁ':
                    komoji_count++;
                    break;
                case 'ぃ':
                    komoji_count++;
                    break;
                case 'ぅ':
                    komoji_count++;
                    break;
                case 'ぇ':
                    komoji_count++;
                    break;
                case 'ぉ':
                    komoji_count++;
                    break;
                case 'ゃ':
                    komoji_count++;
                    break;
                case 'ゅ':
                    komoji_count++;
                    break;
                case 'ょ':
                    komoji_count++;
                    break;
                default:
                    break;
            }
        }

        return komoji_count;
    }

    private void romaji_changer(List<List<string>> r_list, int[] u_array)
    {
        string change_str = "";
        string color_change_str = "";
        int[] array;
        array = u_array;

        for (int i = 0; i < r_list.Count; i++)
        {
            
            if (hiragana_index == i)
            {
                Debug.Log(correct_romaji_log);
                color_change_str += correct_romaji_log;
                change_str += r_list[i][array[i]].Substring(correct_romaji_log.Length, r_list[i][array[i]].Length - correct_romaji_log.Length);
                continue;
            }
            if (hiragana_index > i) color_change_str += r_list[i][array[i]];
            if (hiragana_index < i) change_str += r_list[i][array[i]];
        }

        this.romaji[select_question_number].GetComponent<Text>().text = $"<color=#999999>{color_change_str}</color><color=#ffffff>{change_str}</color>";
    }

    private void init()
    {
        string romaji_str = ""; //ローマ字
        correct_romaji_log = ""; //現在から一つ前に打った正解のローマ字文字列
        questions_dictionary = new Dictionary<int, string[]>();
        question_select_flag = true;
        ForciblyTerminateFlag = false;

        romaji_first_list = new List<List<string>>();

        for (int i = 0; i < Questions.Length; i++)
        {
            string[] jphi = WG.japanese_and_hiragana(); //表示する日本語とひらがな

            List<List<string>> list = new List<List<string>>();
            list = WG.romaji_list(jphi[1]);

            romaji_first_list.Add(new List<string>());
            romaji_first_list[i] = WG.romaji_first_list(list);
            
            for (int j = 0; j > romaji_first_list[i].Count; j++)
            {
                Debug.Log(romaji_first_list[i][j]);
            }
            
            romaji_str = WG.romaji_str(list); //ローマ字生成
            Debug.Log(romaji_str);
            this.japanese[i].GetComponent<Text>().text = jphi[0];
            this.romaji[i].GetComponent<Text>().text = romaji_str;

            string[] jprm = { jphi[0], jphi[1], romaji_str };
            questions_dictionary.Add(i, jprm);
            Debug.Log(questions_dictionary[i][0]);
        }

        romaji_list = new List<List<string>>();
        use_index_array = new int[20] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };

        hiragana_index = 0;
        romaji_count = 0;
        select_question_number = 0;
        nn_flag = false;

        RQS.Resize();
    }

    void Start()
    {
        init();
        
        this.SESource = this.GetComponent<AudioSource>();

        for (int c_i = 0; c_i < choice_mark.Length; c_i++) this.choice_mark[c_i].SetActive(false);

        typo_count = 0;
        total_count = 0;
    }

    void Update()
    {
        char input_c;

        if (ForciblyTerminateFlag)
        {
            init();
            ForciblyTerminateFlag = false;
        }

        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    input_c = (char)code;

                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        if (Input.GetKey(KeyCode.Alpha1))
                        {
                            Debug.Log("!");
                            input_c = '!';
                        }
                        else if (Input.GetKey(KeyCode.Slash))
                        {
                            Debug.Log("?");
                            input_c = '?';
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    Char.ToLower(input_c);
                    bool miss_flag = true;

                    //問題選択処理
                    if (question_select_flag)
                    {
                        Debug.Log(romaji_first_list.Count);
                        for (int q_i = 0; q_i < questions_dictionary.Count; q_i++)
                        {
                            for (int j = 0; j < romaji_first_list[q_i].Count; j++)
                            {
                                if (input_c == romaji_first_list[q_i][j][0] && this.Questions[q_i].activeSelf)
                                {
                                    word_set(q_i);
                                    select_question_number = q_i;
                                    this.choice_mark[select_question_number].SetActive(true);
                                    question_select_flag = false;
                                    break;
                                }
                            }

                            if (question_select_flag == false) break;
                        }

                        if (question_select_flag)
                        {
                            this.SESource.PlayOneShot(this.SE[1]);
                            typo_count++;
                            break;
                        }
                    }

                    for (int romaji_index = 0; romaji_index < romaji_list[hiragana_index].Count; romaji_index++)
                    {
                        if (romaji_list[hiragana_index][romaji_index].Length - 1 < romaji_count) continue;

                        //nnflagが立ったときの「ん」の例外処理
                        if (input_c == 'n' && nn_flag)
                        {
                            Debug.Log("例外発生！");
                            use_index_array[hiragana_index - 1] = 1;
                            romaji_changer(romaji_list, use_index_array);
                            miss_flag = false;
                            nn_flag = false;
                            break;
                        }
                        else
                        {
                            nn_flag = false;
                        }

                        //正誤処理
                        if (input_c == romaji_list[hiragana_index][romaji_index][romaji_count])
                        {
                            this.SESource.PlayOneShot(this.SE[0]);
                            total_count++;
                            Debug.Log("OK");
                            //一つの文字を入力し終えた時の処理
                            if(romaji_list[hiragana_index][romaji_index].Length - 1 == romaji_count)
                            {
                                if(romaji_list[hiragana_index][romaji_index][romaji_count] != 'n')
                                {
                                    if (romaji_list[hiragana_index][romaji_index].Substring(0, romaji_count + 1) == correct_romaji_log + input_c)
                                    {
                                        miss_flag = false;
                                        correct_romaji_log += input_c;
                                        use_index_array[hiragana_index] = romaji_index;
                                        romaji_changer(romaji_list, use_index_array);
                                        correct_romaji_log = "";
                                        hiragana_index++;
                                        romaji_count = 0;
                                        break;
                                    }
                                }

                                if (romaji_list[hiragana_index][romaji_index][romaji_count] == 'n')
                                {
                                    if (romaji_count == 0)
                                    {
                                        miss_flag = false;
                                        nn_flag = true;
                                        correct_romaji_log += input_c;
                                        use_index_array[hiragana_index] = romaji_index;
                                        romaji_changer(romaji_list, use_index_array);
                                        correct_romaji_log = "";
                                        hiragana_index++;
                                        romaji_count = 0;
                                        break;
                                    }
                                    if (romaji_list[hiragana_index][romaji_index][romaji_count - 1] == 'n' || romaji_list[hiragana_index][romaji_index][romaji_count - 1] == 'x')
                                    {
                                        miss_flag = false;
                                        correct_romaji_log += input_c;
                                        use_index_array[hiragana_index] = romaji_index;
                                        romaji_changer(romaji_list, use_index_array);
                                        correct_romaji_log = "";
                                        hiragana_index++;
                                        romaji_count = 0;
                                        break;
                                    }
                                }
                            }
                            //正解時の処理
                            if(romaji_list[hiragana_index][romaji_index].Length - 1 > romaji_count)
                            {
                                if (romaji_count == 0)
                                {
                                    miss_flag = false;
                                    correct_romaji_log += input_c;
                                    //Debug.Log(correct_romaji_log);
                                    use_index_array[hiragana_index] = romaji_index;
                                    romaji_changer(romaji_list, use_index_array);
                                    romaji_count++;
                                    break;
                                }

                                if (romaji_count != 0 && romaji_list[hiragana_index][romaji_index].Length > romaji_count-1)
                                {
                                    if (romaji_list[hiragana_index][romaji_index].Substring(0,romaji_count+1) == correct_romaji_log + input_c)
                                    {
                                        //Debug.Log($"{romaji_list[hiragana_index][romaji_index].Substring(0, romaji_count + 1)},{correct_romaji_log + input_c}");
                                        miss_flag = false;
                                        correct_romaji_log += input_c;
                                        use_index_array[hiragana_index] = romaji_index;
                                        romaji_changer(romaji_list, use_index_array);
                                        romaji_count++;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (miss_flag)
                    {
                        Debug.Log("NO");
                        this.SESource.PlayOneShot(this.SE[1]);
                        typo_count++;
                    }

                    //すべての文字を正しく入力した時の処理→次の問題へ
                    if (hiragana_index == hiragana_length)
                    {
                        Debug.Log("正解!");
                        this.SESource.PlayOneShot(this.SE[2]);
                        hiragana_index = 0;
                        correct_romaji_log = "";
                        question_select_flag = true;

                        this.questions[select_question_number].GetComponent<Image>().color = new Color(255, 255, 255, 240);
                        this.Questions[select_question_number].SetActive(false);
                        this.choice_mark[select_question_number].SetActive(false);

                        for (int q_i = 0; q_i < questions_dictionary.Count; q_i++)
                        {
                            if (Questions[q_i].activeSelf)
                            {
                                break;
                            }
                            if (q_i == questions_dictionary.Count - 1)
                            {
                                EA.DrillAttackForcedEnd();
                                EA.ArrowAttackForcedEnd();
                                if (!(ForciblyTerminateFlag)) init();
                            }
                        }

                        BHM.DecreaseBossHP();
                    }
                }
            }
        }
    }
}
