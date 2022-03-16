using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace jb_ps_menulib
{
    //Base Menu
    [Cmdlet(VerbsCommon.New, "JBMenuManager")]
    [OutputType(typeof(menu_manager))]
    public class menu_manager : PSCmdlet
    {
        private int menu_width = 40;
        private List<elements.base_menu> c_menuList = new List<elements.base_menu>();
        private bool defName = true;

        public void SetMaxWidth(int w)
        {
            if (w < 10)
                w = 10;

            menu_width = w;
        }

        public void RemDefaultName(bool val)
        {
            defName = !val;
        }
        public int AddElement(string type)
        {
            type = type.ToLower();
            int newID = c_menuList.Count + 1;
            elements.base_menu t_menu = null;

            switch (type)
            {
                case "progress":
                    t_menu = new elements.menu_progress(newID, this);
                    break;
                case "input":
                    t_menu = new elements.menu_input(newID, this);
                    break;
                case "menu":
                    t_menu = new elements.menu_menu(newID, this);
                    break;
                case "textbox":
                    t_menu = new elements.menu_textbox(newID, this);
                    break;
                case "spacer":
                    t_menu = new elements.menu_spacer(newID, this);
                    break;
                default:
                    t_menu = new elements.base_menu(newID, this);
                    break;
            }

            if(t_menu != null)
            {
                c_menuList.Add(t_menu);
                if (!defName)
                {
                    t_menu.SetName("");
                    t_menu.SetInfo("");
                }
                t_menu.Init();
                return newID;
            }
            return -1;
        }
        public void RemElement(int id)
        {
            if(id > 0 && id <= c_menuList.Count)
            {
                c_menuList.RemoveAt(id-1);
            }
        }
        public elements.base_menu GetElement(int id)
        {
            if (id > 0 && id <= c_menuList.Count)
            {
                return c_menuList[id-1];
            }
            return null;
        }
        public elements.base_menu[] GetElementByType(string str)
        {
            str = "jb_ps_menulib.elements.menu_" + str;
            int count = 0;

            foreach (elements.base_menu obj in c_menuList)
            {
                if (obj.GetType().ToString() == str)
                    count++;
            }

            if(count > 0)
            {
                elements.base_menu[] r_array = new elements.base_menu[count];
                count = 0;

                foreach (elements.base_menu obj in c_menuList)
                {
                    if (obj.GetType().ToString() == str)
                    {
                        r_array[count] = obj;
                        count++;
                    }
                }

                return r_array;
            }

            return null;
        }
        public int CountElement()
        {
            return c_menuList.Count;
        }
        public void ClearElement()
        {
            foreach (elements.base_menu el in c_menuList)
            {
                el.Reset();
            }
            c_menuList.Clear();
        }

        public void ClearHost(System.Management.Automation.Host.PSHostUserInterface UI)
        {
            System.Management.Automation.Host.PSHostRawUserInterface RawUI = UI.RawUI;
            RawUI.CursorPosition = new System.Management.Automation.Host.Coordinates(0, 0);
            System.Management.Automation.Host.Rectangle rectangle = new System.Management.Automation.Host.Rectangle(-1, -1, -1, -1);
            RawUI.SetBufferContents(rectangle, new System.Management.Automation.Host.BufferCell(' ', ConsoleColor.Gray, ConsoleColor.Black, System.Management.Automation.Host.BufferCellType.Complete) );
        }

        public void Draw()
        {
            System.Management.Automation.Host.PSHostUserInterface UI = Host.UI;
            this.ClearHost(UI);
            foreach (elements.base_menu el in c_menuList)
            {
                el.SetMaxWidth(menu_width);
                el.PreDraw(UI);
            }
            foreach (elements.base_menu el in c_menuList)
            {
                el.Draw(UI);
                el.PostDraw(UI);
            }
        }

        public void Update(int id = 0)
        {
            if (id == 0)
                id = c_menuList.Count;

            System.Management.Automation.Host.PSHostUserInterface UI = Host.UI;
            this.ClearHost(UI);

            for(int i = 0; i < id; i++)
            {
                c_menuList[i].SetMaxWidth(menu_width);
                c_menuList[i].PreDraw(UI);
                c_menuList[i].Draw(UI);
            }
        }

        public override string ToString()
        {
            string rstr = "Class : " + this.GetType() + " | MenuCount : " + c_menuList.Count + " | MenuList :";
            foreach (elements.base_menu el in c_menuList)
            {
                rstr += "\n- " + el.ToString();
            }
            return rstr;
        } // ToString

        // ===================================================================================================
        // Cmdlet Function !
        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            // start
        }
        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            Host.UI.WriteLine(ConsoleColor.Green, Host.UI.RawUI.BackgroundColor, "Test2");
            WriteObject(this);
        }
        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            // End
        }
    }
}
