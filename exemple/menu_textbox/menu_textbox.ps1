# Name : JB PS Menu Lib (Exemple)
# By   : Jonathan Bauer
# Date : 15/03/2022
# ===================================

# Import the module
# ===================================
Import-Module '.\jb_ps_menulib.dll';

# Create the frame
# ===================================
# Create a new instance of 'JBMenuManage' and store it in $frame
$frame = New-JBMenuManager;
# Set the Width of the frame, by default is 40.
$frame.SetMaxWidth(60);
# By default when you create an element, its name and description is named 'base_menu_xx'. 
# This option allows you to deactivate it and not to pre-fill this value.
$frame.RemDefaultName($ture);

# Create the elmment : 'textbox'
# ===================================
# Create a new elmment in the frame, in this case is the 'textbox'. retrive the id of the element in $textboxID.
$textboxID = $frame.AddElement("textbox");
# Get the 'textbox' element in the frame with the ID recive previously.
$textboxClass = $frame.GetElement($textboxID); 
# Change the title of the text box to "My First TextBox"
# The SetName method is available for all elements.
$textboxClass.SetName("My First TextBox");
# Change the info of the text box to "OwO it's work !\nAnd line break to !
# The SetInfo method is available for all elements.
$textboxClass.SetInfo("OwO it's work !\nAnd line break to !");

# Draw on the screen
# ===================================
# Simply call the draw method.
$frame.Draw()