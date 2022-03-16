using System;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace jb_ps_menulib.elements
{
    class menu_spacer : base_menu
    {
        public menu_spacer(int id, menu_manager parent) : base(id, parent)
        {
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

        } // Clear element data (but not the parent value)
        public override void Reset()
        {
            this.Init();
        } // Set all value to the default one
          // Drawing
        public override void PreDraw(PSHostUserInterface UI)
        {
            //this.SetMaxWidth(1); user set !
        } // PreDraw calc
        public override void Draw(PSHostUserInterface UI)
        {
            string str = "";
            for (int i = 0; i < this.GetMaxWidth(); i++)
            {
                str += " ";
            }
            for (int i = 0; i < this.GetMaxHight(); i++)
            {
                UI.WriteLine(this.color_fg, this.color_bg, str);
                //UI.RawUI.ForegroundColor, UI.RawUI.BackgroundColor, str
            }
        } // Draw the element
        public override void PostDraw(PSHostUserInterface UI)
        {

        } // PostDraw calc
    }
}
