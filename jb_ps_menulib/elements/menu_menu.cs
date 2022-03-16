using System;
using System.Collections.Generic;
using System.Management.Automation.Host;
using System.Text.RegularExpressions;

namespace jb_ps_menulib.elements
{
    class menu_menu : base_menu
    {
        public menu_menu(int id, menu_manager parent) : base(id, parent)
        {
        }

        private List<string> name_array = new List<string>();
        private List<string> info_array = new List<string>();
        private bool exit = false;
        private System.Management.Automation.ScriptBlock output = null;

        public int AddMenu(string name, string info = "empty")
        {
            name_array.Add(name);
            info_array.Add(info);

            return name_array.Count;
        }
        public void EditMenu(int id, string name, string info = "empty")
        {
            if(id > 0 && id <= name_array.Count)
            {
                name_array[id - 1] = name;
                info_array[id - 1] = info;
            }
        }
        public void RemMenu(int id)
        {
            if (id > 0 && id <= name_array.Count)
            {
                name_array.RemoveAt(id - 1);
                info_array.RemoveAt(id - 1);
            }
        }
        public int CountMenu()
        {
            return name_array.Count;
        }

        public void SetExit(bool val = false)
        {
            exit = val;
        }
        public bool GetExit()
        {
            return exit;
        }
        
        public void SetOutput(System.Management.Automation.ScriptBlock func)
        {
            this.output = func; 
        }
        // ===================================================================================================
        // Parent Function
        //Other
        public override void Init()
        {
            this.SetMaxHight(1);// default value
        } // On Creation and Reset
        public override void Clear()
        {
            name_array.Clear();
            info_array.Clear();
            exit = false;
            output = null;
        } // Clear element data (but not the parent value)
        public override void Reset()
        {
            this.Clear();
            this.Init();
        } // Set all value to the default one
          // Drawing
        public override void PreDraw(PSHostUserInterface UI)
        {
            int ligneNB = 1;
            if (this.GetName().Length > 0)
                ligneNB++;

            if (this.GetInfo().Length > 0)
                ligneNB++;

            if (this.exit)
                ligneNB += 2;

            ligneNB += name_array.Count;
            this.SetMaxHight(ligneNB);
        } // PreDraw calc
        public override void Draw(PSHostUserInterface UI)
        {
            if (this.GetName().Length > 0 || this.GetInfo().Length > 0)
                UI.WriteLine(this.color_fg_spacer, this.color_bg, this.GetSeparator());

            if (this.GetName().Length > 0)
                UI.WriteLine(this.color_fg_title, this.color_bg, 
                    this.StringAlign(this.GetName(), this.GetMaxWidth(), "1", 1));

            if (this.GetInfo().Length > 0)
                UI.WriteLine(this.color_fg_info, this.color_bg,
                    this.StringAlign(this.GetInfo(), this.GetMaxWidth(), "0", 1));

            UI.WriteLine(this.color_fg_spacer, this.color_bg, this.GetSeparator());

            for (int i = 0; i < this.name_array.Count; i++)
            {
                string id = (i+1).ToString();
                if (id.Length < 2)
                    id = "0" + id;

                UI.WriteLine(this.color_fg, this.color_bg,
                                   this.StringAlign(" ["+id+"] - " + this.name_array[i], this.GetMaxWidth(), "0", 1));
            }

            if(this.exit)
            {
                UI.WriteLine(this.color_fg, this.color_bg,
                                   this.StringAlign("", this.GetMaxWidth(), "0", 0));
                UI.WriteLine(ConsoleColor.Red, this.color_bg,
                                   this.StringAlign(" [00] - Exit Script", this.GetMaxWidth(), "0", 1));
            }

        } // Draw the element
        public override void PostDraw(PSHostUserInterface UI)
        {
            if (this.name_array.Count > 0)
            {
                string str = "Select a option (";
                if (this.exit)
                    str += "0/";
                if (this.name_array.Count == 1)
                    str += "1)";
                if (this.name_array.Count == 2)
                    str += "1/2)";
                if (this.name_array.Count > 2)
                    str += "1/.../" + this.name_array.Count.ToString() + ")";


                UI.Write(this.color_fg, this.color_bg, (str + " :"));

                string recive = UI.ReadLine();
                int r_int = -1;
                if ( !Regex.Match(recive, "[^0-9]").Success )
                {
                    string result = Regex.Replace(recive, "[^0-9]", "");
                    r_int = Int32.Parse(result);
                }

                if ( (this.exit && r_int == 0) || (r_int > 0 && r_int <= this.name_array.Count))
                {
                    if(r_int == 0)
                    {
                        this.parent.ClearHost(UI);
                        UI.WriteLine(ConsoleColor.Red, UI.RawUI.BackgroundColor, "The user has exited program !");
                        UI.WriteLine("Press ENTER to quit...");
                        string trach = UI.ReadLine();
                    }
                    else 
                    {
                        if(this.output != null)
                            this.output.Invoke(r_int);
                    }
                }
                else
                {
                    this.parent.Update(this.id);
                    this.PostDraw(UI);
                }
            }
            else
            {
                UI.WriteLine(ConsoleColor.Red, UI.RawUI.BackgroundColor, "The menu is not configured !");
                UI.WriteLine(ConsoleColor.Red, UI.RawUI.BackgroundColor, "The user has exited program !");
                UI.WriteLine("Press ENTER to quit...");
                string trach = UI.ReadLine();
            }
        } // PostDraw calc
    }
}
