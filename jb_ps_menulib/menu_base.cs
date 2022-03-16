using System;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace jb_ps_menulib.elements
{
    //Base Menu
    public class base_menu : PSCmdlet
    {
        public int id;
        public menu_manager parent;

        private bool c_error = false;
        private int c_error_code = 0;
        private string c_error_str = "";

        private string c_name = "base_menu_name";
        private string c_info = "base_menu_info";
        private int c_startligne = 0;
        private int c_maxwidth = 0; // max elmement size
        private int c_maxhight = 0; // max elmement size

        public ConsoleColor color_bg = ConsoleColor.Cyan;
        public ConsoleColor color_fg = ConsoleColor.Black;

        public ConsoleColor color_fg_secondary = ConsoleColor.DarkGray;
        public ConsoleColor color_fg_title = ConsoleColor.DarkGreen;
        public ConsoleColor color_fg_info = ConsoleColor.DarkBlue;
        public ConsoleColor color_fg_spacer = ConsoleColor.DarkMagenta;


        private string separator = "";

        // Constructor
        public base_menu(int id, menu_manager parent)
        {
            this.id = id;
            this.parent = parent;
        }

        // Base Element Function
        public void SetName(string name)
        {
            c_name = name;
        }
        public string GetName()
        {
            return c_name;
        }
        public void SetInfo(string info)
        {
            c_info = info;
        }
        public string GetInfo()
        {
            return c_info;
        }
        public void SetStartLigne(int nb)
        {
            if (nb <= 0) { nb = 0; };
            c_startligne = nb;
        }
        public int GetStartLigne()
        {
            return c_startligne;
        }
        public void SetMaxWidth(int nb)
        {
            if (nb <= 0) { nb = 0; };
            c_maxwidth = nb;
        }
        public int GetMaxWidth()
        {
            return c_maxwidth;
        }
        public void SetMaxHight(int nb)
        {
            if (nb <= 0) { nb = 0; };
            c_maxhight = nb;
        }
        public int GetMaxHight()
        {
            return c_maxhight;
        }

        // Error Magmt
        public void SetError()
        {
            c_error = false;
            c_error_code = 0;
            c_error_str = "";
        } // Clear Error
        public void SetError(int code)
        {
            if (code > 0)
            {
                c_error = true;
                c_error_code = code;
                c_error_str = "[ERROR] ps_menulib (code : " + code.ToString() + ") > An error occurred : Unknown";
            }
        }
        public void SetError(string str)
        {
            c_error = true;
            c_error_code = -1;
            c_error_str = "[ERROR] ps_menulib (code : -1) > An error occurred : " + str;
        }
        public void SetError(int code, string str)
        {
            if (code > 0)
            {
                c_error = true;
                c_error_code = code;
                c_error_str = "[ERROR] ps_menulib (code : " + code.ToString() + ") > An error occurred : " + str;
            }
        }

        public bool IsError()
        {
            return c_error;
        }
        public string GetError()
        {
            return c_error_str;
        }

        //Utiles Function
        // Other
        public string FirstLetterToUpercase(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
        public string StringTrim(string str, int maxLength, string endStr = "...")
        {
            if (str.Length > maxLength)
            {
                return (str.Substring(0, maxLength - endStr.Length) + endStr);
            };
            return str;
        }
        public string StringAlign(string str, int maxLength, string type = "start", int margin = 0)
        {
            maxLength = maxLength - (margin * 2);
            type = type.ToLower();
            str = this.StringTrim(str, maxLength);
            string rstr = str;

            if (type == "start" || type == "left" || type == "0")
            {
                rstr = str;
                for (int i = str.Length; i < maxLength; i++)
                    rstr += " ";
            }
            else if (type == "center" || type == "1")
            {
                int t_i = (int)Math.Floor((maxLength - str.Length) * 0.5);
                rstr = str;

                for (int i = 0; i < t_i; i++)
                    rstr = " " + rstr + " ";

                if (rstr.Length < maxLength)
                    rstr += " ";
            }
            else if (type == "end" || type == "right" || type == "2")
            {
                rstr = str;
                for (int i = str.Length; i < maxLength; i++)
                    rstr = " " + rstr;
            };

            for (int i = 0; i < margin; i++)
                rstr = " " + rstr + " ";

            return rstr;
        }
        public int GetBigestLength(string[] strArray, int margin = 0)
        {
            int count = 0;
            foreach (string str in strArray)
            {
                if (count < str.Length)
                {
                    count = str.Length;
                };
            };

            return count + (margin * 2);
        }
        // StringAlignMulti
        public string StringAlignMulti(int maxLength, string strLeft = "", string strCenter = "", string strRight = "", int margin = 0)
        {
            maxLength = maxLength - (margin * 2);
            string rstr = "";
            int t_div = 0;
            if (strLeft.Length > 0)
                t_div++;
            if (strCenter.Length > 0)
                t_div++;
            if (strRight.Length > 0)
                t_div++;


            int t_nb = (int)Math.Floor((float)(maxLength / t_div));

            if (strLeft.Length > 0)
                rstr += this.StringAlign(strLeft, t_nb - 1, "0", 0) + " ";
            if (strCenter.Length > 0)
                rstr += this.StringAlign(strCenter, t_nb - 1, "1", 0) + " ";
            if (strRight.Length > 0)
                rstr += this.StringAlign(strRight, t_nb, "2", 0);

            if (rstr.Length < maxLength)
                rstr = " " + rstr;
            for (int i = 0; i < margin; i++)
                rstr = " " + rstr + " ";

            return rstr;
        }
        public string StringAlignMulti(int maxLength, string strLeft = "", int margin = 0)
        {
            return this.StringAlignMulti(maxLength, strLeft, "", "", margin);
        }
        public string StringAlignMulti(int maxLength, string strLeft, string strRight = "", int margin = 0)
        {
            return this.StringAlignMulti(maxLength, strLeft, "", strRight, margin);
        }
        // Color
        public void SetBGColor(ConsoleColor col)
        {
            color_bg = col;
        }
        public void SetFGColor(ConsoleColor col)
        {
            color_fg = col;
        }
        // Ligne
        public string GetSeparator(string custom = "")
        {
            string temp = this.separator;
            if (this.separator.Length != this.GetMaxWidth())
            {
                for (int i = 0; i < this.GetMaxWidth(); i++)
                {
                    if(custom.Length > 0)
                    {
                        temp += custom;
                    }
                    else{
                        if (i % 2 == 1)
                        {
                            temp += "=";
                        }
                        else
                        {
                            temp += "-";
                        }
                    }
                }
            }

            return temp;
        }

        // ===================================================================================================
        // Parent Function
        //Other
        public virtual void Init()
        {

        } // On Creation and Reset
        public virtual void Clear()
        {

        } // Clear element data (but not the parent value)
        public virtual void Reset()
        {
            this.Init();
        } // Set all value to the default one
          // Drawing
        public virtual void PreDraw(PSHostUserInterface UI)
        {

        } // PreDraw calc
        public virtual void Draw(PSHostUserInterface UI)
        {
            string str = this.StringAlign(this.GetType().ToString(), this.GetMaxWidth(), "center", 1);
            UI.WriteLine(this.color_fg, this.color_bg, str);
            //UI.RawUI.ForegroundColor, UI.RawUI.BackgroundColor
        } // Draw the element
        public virtual void PostDraw(PSHostUserInterface UI)
        {

        } // PostDraw calc
        public override string ToString()
        {
            return "Class : "+this.GetType()+" | Parent : "+this.parent.GetType()+" | Id : "+this.id.ToString();
        } // ToString

    }

}
