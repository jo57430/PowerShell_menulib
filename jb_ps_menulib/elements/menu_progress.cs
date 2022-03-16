using System;
using System.Collections.Generic;
using System.Management.Automation.Host;
using System.Text.RegularExpressions;

namespace jb_ps_menulib.elements
{
    class menu_progress : base_menu
    {
        public menu_progress(int id, menu_manager parent) : base(id, parent)
        {
        }

        private List<string> infoList = new List<string>();
        private List<int> valList = new List<int>();
        private List<int> maxList = new List<int>();

        public int AddProgress(string info, int curVal = 0, int max = 100)
        {
            if (curVal < 0)
                curVal = 0;
            if (curVal > max)
                curVal = max;

            infoList.Add(info);
            valList.Add(curVal);
            maxList.Add(max);

            return infoList.Count;
        }
        public void EditProgress(int id, string info, int val, int valMax)
        {
            if (id > 0 && id <= infoList.Count)
            {
                if (val < 0)
                    val = 0;
                if (val > valMax)
                    val = valMax;

                infoList[id - 1] = info;
                valList[id - 1] = val;
                maxList[id - 1] = valMax;
            }
        }
        public void EditProgress(int id, string info)
        {
            if (id > 0 && id <= infoList.Count)
                EditProgress(id, info, valList[id-1], maxList[id - 1]);
        }
        public void EditProgress(int id, string info, int val)
        {
            if (id > 0 && id <= infoList.Count)
                EditProgress(id, info, val, maxList[id - 1]);
        }
        public void EditProgress(int id, int val)
        {
            if (id > 0 && id <= infoList.Count)
                EditProgress(id, infoList[id - 1], val, maxList[id - 1]);
        }
        public void EditProgress(int id, int val, int valMax)
        {
            if (id > 0 && id <= infoList.Count)
                EditProgress(id, infoList[id - 1], val, valMax);
        }
        public void RemProgress(int id)
        {
            if (id > 0 && id <= infoList.Count)
            {
                infoList.RemoveAt(id - 1);
                valList.RemoveAt(id - 1);
                maxList.RemoveAt(id - 1);
            }
        }

        // ===================================================================================================
        // Parent Function
        //Other
        public override void Init()
        {

        } // On Creation and Reset
        public override void Clear()
        {
            infoList.Clear();
            valList.Clear();
            maxList.Clear();
        } // Clear element data (but not the parent value)
        public override void Reset()
        {
            this.Clear();
            this.Init();
        } // Set all value to the default one
          // Drawing
        public override void PreDraw(PSHostUserInterface UI)
        {
            int ligneNB = 0;
            if (this.GetName().Length > 0)
                ligneNB++;

            for (int i = 0; i < infoList.Count; i++)
            {
                ligneNB++;
                if(infoList[i].Length > 0)
                    ligneNB++;
            }

            this.SetMaxHight(ligneNB);
        } // PreDraw calc
        public override void Draw(PSHostUserInterface UI)
        {
            if (this.GetName().Length > 0)
                UI.WriteLine(this.color_fg_title, this.color_bg,
                    this.StringAlign(this.GetName(), this.GetMaxWidth(), "1", 1));

            for(int i = 0; i < infoList.Count; i++)
            {
                string complite = "";
                string empty = "";

                

                int value = (int) Math.Ceiling( (Convert.ToSingle(valList[i]) / Convert.ToSingle(maxList[i])) * Convert.ToSingle(this.GetMaxWidth() - 4));
                int p = (int) Math.Ceiling((Convert.ToSingle(valList[i]) / Convert.ToSingle(maxList[i])) * 100);
                string p_s = p.ToString() + "%";
                int p_s_p = (int) Math.Floor( ((float)(this.GetMaxWidth() - 4) - p_s.Length)/2 );

                if (infoList[i].Length > 0)
                {
                    UI.WriteLine(this.color_fg_info, this.color_bg,
                        this.StringAlign(" > " + infoList[i], this.GetMaxWidth(), "0", 1));
                }
                else
                {
                    UI.WriteLine(this.color_fg_info, this.color_bg, this.GetSeparator(" "));
                }

                for(int u = 0; u < this.GetMaxWidth() - 4; u++)
                {
                    if(u < value)
                    {
                        if(u >= p_s_p && u < p_s_p+ p_s.Length)
                        {
                            complite += p_s[u - p_s_p];
                        }
                        else
                        {
                            complite += " ";
                        }
                    }
                    else 
                    {
                        if (u >= p_s_p && u < p_s_p + p_s.Length)
                        {
                            empty += p_s[u - p_s_p];
                        }
                        else
                        {
                            empty += " ";
                        }
                    }
                }

                UI.Write(this.color_fg, this.color_bg, " [");
                UI.Write(ConsoleColor.Black, ConsoleColor.White, complite);
                UI.Write(ConsoleColor.White, ConsoleColor.Black, empty);
                UI.WriteLine(this.color_fg, this.color_bg, "] ");

            }
            if (infoList.Count > 0)
                UI.WriteLine(this.color_fg_info, this.color_bg, this.GetSeparator(" "));

        } // Draw the element
        public override void PostDraw(PSHostUserInterface UI)
        {

        } // PostDraw calc
    }
}
