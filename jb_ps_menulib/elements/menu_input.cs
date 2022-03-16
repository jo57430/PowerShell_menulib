using System;
using System.Collections.Generic;
using System.Management.Automation.Host;
using System.Text.RegularExpressions;

namespace jb_ps_menulib.elements
{
    class menu_input : base_menu
    {
        public menu_input(int id, menu_manager parent) : base(id, parent)
        {
        }

        private menuConvert convert = new menuConvert();
        private List<string> name_array = new List<string>();
        private List<string> type_array = new List<string>();
        private List<string> def_array = new List<string>();
        private List<string> val_array = new List<string>();
        private System.Management.Automation.ScriptBlock output = null;


        public int AddInput(string name, string type = "string", string defaultVal = "")
        {
            name_array.Add(name);
            type_array.Add(type);
            def_array.Add(defaultVal);
            val_array.Add("");

            return name_array.Count;
        }
        public void EditInput(int id, string name, string type = "string", string defaultVal = "")
        {
            if (id > 0 && id <= name_array.Count)
            {
                name_array[id - 1] = name;
                type_array[id - 1] = type;
                def_array[id - 1] = defaultVal;
            }
        }
        public void RemInput(int id)
        {
            if (id > 0 && id <= name_array.Count)
            {
                name_array.RemoveAt(id - 1);
                type_array.RemoveAt(id - 1);
                def_array.RemoveAt(id - 1);
                val_array.RemoveAt(id - 1);
            }
        }
        public int CountInput()
        {
            return name_array.Count;
        }

        public void SetOutput(System.Management.Automation.ScriptBlock func)
        {
            this.output = func;
        }
        // ===================================================================================================
        // Parent Function
          // Other
        public override void Init()
        {
            
        } // On Creation and Reset
        public override void Clear()
        {
            output = null;
            val_array.Clear();
            def_array.Clear();
            type_array.Clear();
            name_array.Clear();
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

            ligneNB += name_array.Count;
            foreach(string temp in val_array)
            {
                if (temp.Length > 0)
                    ligneNB++;
            }

            if (name_array.Count > 0)
                ligneNB++;

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

            for (int i = 0; i < name_array.Count; i++)
            {
                UI.WriteLine(this.color_fg_secondary, this.color_bg,
                   this.StringAlign("" + name_array[i] + " :", this.GetMaxWidth(), "0", 1));
                if (val_array[i].Length > 0)
                    UI.WriteLine(this.color_fg, this.color_bg,
                        this.StringAlign("|--> " + val_array[i], this.GetMaxWidth(), "0", 1));
            }

            if(name_array.Count > 0)
                UI.WriteLine(this.color_fg_spacer, this.color_bg, this.GetSeparator());

        } // Draw the element
        public override void PostDraw(PSHostUserInterface UI)
        {
            if (this.name_array.Count > 0)
            {
                int lastid = 0;
                foreach (string val in val_array)
                {
                    if (val.Length > 0)
                        lastid++;
                }

                if (lastid < name_array.Count)
                {
                    UI.WriteLine(this.color_fg, this.color_bg,
                        this.StringAlign("" + name_array[lastid] + " :", this.GetMaxWidth(), "0", 1));

                    string temp = "Entrer une valeur de type " + "'" + type_array[lastid] + "'";
                    if (def_array[lastid].Length > 0)
                        temp += " [" + def_array[lastid] + "] ";
                    temp += " :";
                    UI.Write(temp);
                    string value = UI.ReadLine();

                    if (value.Length <= 0)
                        value = def_array[lastid];

                    if (value.Length > 0 && convert.IsValid(value, type_array[lastid]) ){
                        val_array[lastid] = value;
                        if (this.output != null)
                            this.output.Invoke(lastid+1, value);
                    }
                    this.parent.Update(this.id);
                    this.PostDraw(UI);
                }
                else // end
                {
                    if (this.output != null)
                        this.output.Invoke(0, "end");

                    for (int i = 0; i < val_array.Count; i++)
                        val_array[i] = "";
                }

            }
        } // PostDraw calc
    }
}
namespace jb_ps_menulib
{
    class menuConvert
    {
        public menuConvert()
        {
        }

        public bool IsValid(string value, string type = "all")
        {
            bool result = false;
            type = type.ToLower();

            switch (type)
            {
                case "string":
                    result = true;
                    break;
                case "text":
                    result = IsValidText(value);
                    break;
                case "char":
                    result = IsValidChar(value);
                    break;
                case "int":
                    result = IsValidInt(value);
                    break;
                case "float":
                    result = IsValidFloat(value);
                    break;
                case "mail":
                    result = IsValidEmail(value);
                    break;
                case "url":
                    result = IsValidURL(value);
                    break;
                default:
                    result = true;
                    break;
            }

            return result;
        }

        private bool IsValidInt(string val)
        {
            return !Regex.Match(val, "[^0-9]").Success;
        }
        private bool IsValidFloat(string val)
        {
            return !Regex.Match(val, "[^0-9.,]").Success;
        }
        private bool IsValidEmail(string val)
        {
            return (Regex.Match(val, ".").Success && Regex.Match(val, "@").Success);
        }
        private bool IsValidURL(string val)
        {
            return (Regex.Match(val, "https://www.").Success || Regex.Match(val, "http://www.").Success || Regex.Match(val, "www.").Success);
        }
        private bool IsValidChar(string val)
        {
            return (val.Length == 1);
        }
        private bool IsValidText(string val)
        {
            return !Regex.Match(val, "[^a-zA-Z,?!. ]").Success;
        }

    }
}