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
# Create a new elmment in the frame, in this case is the 'menu'. retrive the id of the element in $inputID.
$inputID = $frame.AddElement("input");
# Get the 'input' element in the frame with the ID recive previously.
$inputClass = $frame.GetElement($inputID); 
# Change the title of the menu to "My First Input"
# The SetName method is available for all elements.
$inputClass.SetName("My First Input");
# Change the info of the menu to "Write someting :)"
# The SetInfo method is available for all elements.
$inputClass.SetInfo("Write someting :)");

# Add input field
# ===================================
# Create a new input field in the menu, named "Option 1". retrive the id of the option in $IdOptionA.
$IdOptionA = $inputClass.AddInput("enter a string", "string", "defaultValue");
# You can modify the information of this option with the method '$inputClass.EditInput(id, "NewName");'
# You can remove this option with the method '$inputClass.RemInput(id);'

# For this demonstration we will create several other 'input field'
$_ = $inputClass.AddInput("enter a int", "int", "0");
$_ = $inputClass.AddInput("enter a website", "url", "www.studio-fcs.com");

# To receive the option chosen by the user you must create a new powershell method like this one:
function menuSelect{
    param (
        [Parameter (Mandatory = $true)] [int]$recive_id,
        [Parameter (Mandatory = $true)] [string]$recive_value
        )
    # When a input is recive this function whit be called with int and a string argument,
    # The int recive, here is $recive_id is the ID of the input that was completed.
    # The string recive, here is $recive_value is value enter by the user (and the type was validate).

    # For the demonstration we will display the received value.
    Write-Host "Recive : $($recive_id.ToString()) | $($recive_value)"

    # When all the input field is completed this method will be called with the argument :
    # [int]$recive_id = 0
    # [string]$recive_value = "end"
}
# But for this method to work you must pass it as an argument to the following method:
$inputClass.SetOutput($function:menuSelect); 

# Draw on the screen
# ===================================
# Simply call the draw method.
$frame.Draw()