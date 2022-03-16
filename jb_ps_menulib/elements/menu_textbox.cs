using System;
using System.Collections.Generic;
using System.Management.Automation.Host;

namespace jb_ps_menulib.elements
{
    //TextBox
    public class menu_textbox : base_menu
    {
        public menu_textbox(int id, menu_manager parent):base(id, parent)
        {
        }

        private List<string> info_array = new List<string>();
        private int lastBuildLID = -1;

        private void buildText()
        {
            info_array.Clear();
            string t_str = this.GetInfo();
            string t_building = "";
            int count = 0;

            for (int i = 0; i < t_str.Length; i++)
            {
                count++;
                char k = t_str[i];
                char nk = ' ';
                if (i + 1 < t_str.Length)
                    nk = t_str[i + 1];

                if ((k == '\\' && nk == 'n'))
                {
                    i++;
                    count = 0;
                    info_array.Add(t_building);
                    t_building = "";
                }
                else if (count >= (this.GetMaxWidth() - 2) || i == t_str.Length-1)
                {
                    count = 0;
                    t_building += k;
                    info_array.Add(t_building);
                    t_building = "";
                }
                else
                {
                    t_building += k;
                }
            }

            lastBuildLID = t_str.Length*this.GetMaxWidth();
        }
        // ===================================================================================================
        // Parent Function
        //Other
        public override void Init()
        {

        } // On Creation and Reset
        public override void Clear()
        {

        } // Clear element data (but not the parent value)
        public override void Reset()
        {
            this.Init();
        } // Set all value to the default one
          // Drawing
        public override void PreDraw(PSHostUserInterface UI)
        {
            int ligneNB = 0;
            if (this.GetName().Length > 0)
                ligneNB++;

            if (this.GetInfo().Length * this.GetMaxWidth() != lastBuildLID)
            {
                this.buildText();
            }
            ligneNB += info_array.Count;

            this.SetMaxHight(ligneNB);
        } // PreDraw calc
        public override void Draw(PSHostUserInterface UI)
        {
            if (this.GetName().Length > 0)
            {
                string t_str = this.StringAlign(this.GetName(), this.GetMaxWidth(), "center", 1);
                UI.WriteLine(this.color_fg_title, this.color_bg, t_str);
                //UI.RawUI.ForegroundColor, UI.RawUI.BackgroundColor
            }

            foreach(string str in info_array)
            {
                string t_str = this.StringAlign(str, this.GetMaxWidth(), "0", 1);
                UI.WriteLine(this.color_fg_info, this.color_bg, t_str);
                //UI.RawUI.ForegroundColor, UI.RawUI.BackgroundColor
            }

        } // Draw the element
        public override void PostDraw(PSHostUserInterface UI)
        {

        } // PostDraw calc

    }
}
