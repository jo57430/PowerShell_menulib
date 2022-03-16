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

# Create the elmment : 'menu'
# ===================================
# Create a new elmment in the frame, in this case is the 'menu'. retrive the id of the element in $menuID.
$menuID = $frame.AddElement("menu");
# Get the 'menu' element in the frame with the ID recive previously.
$menuClass = $frame.GetElement($menuID); 
# Change the title of the menu to "My First Menu"
# The SetName method is available for all elements.
$menuClass.SetName("My First Menu");
# Change the info of the menu to "Select someting :)"
# The SetInfo method is available for all elements.
$menuClass.SetInfo("Select someting :");
# If you want, you can add the option to allow the user to quit the menu with :
$menuClass.SetExit($true);

# Add menu
# ===================================
# Create a new option in the menu, named "Option 1". retrive the id of the option in $IdOptionA.
$IdOptionA = $menuClass.AddMenu("Option 1");
# You can modify the information of this option with the method '$menuClass.EditMenu(id, "NewName");'
# You can remove this option with the method '$menuClass.RemMenu(id);'

# For this demonstration we will create several other 'options'
$_ = $menuClass.AddMenu("Option 2");
$_ = $menuClass.AddMenu("Option 3");
$_ = $menuClass.AddMenu("Option 4");

# To receive the option chosen by the user you must create a new powershell method like this one:
function menuSelect{
    param (
        [Parameter (Mandatory = $true)] [int]$recive_id
        )
    # When a option is selected this function whit be called with int argument,
    # The int recive, here is $recive_id is the ID of the option selected.

    # For the demonstration we will display the received value.
    Write-Host "Recive : $($recive_id.ToString())"
}
# But for this method to work you must pass it as an argument to the following method:
$menuClass.SetOutput($function:menuSelect); 

# Draw on the screen
# ===================================
# Simply call the draw method.
$frame.Draw()