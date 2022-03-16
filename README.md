# PowerShell : Menu Lib (Terminal)
<img src="http://j-bauer.fr/assets/img/proj/jb_ps_menulib.jpg" alt="Image DrawLib" width="50%" height="50%"><br>

(PowerShell Module) jb_ps_menulib :</br>
Allows you to easily create small menus in the PowerShell terminal.
</br>

# List of available elements
<table style="border-collapse: collapse; width: 100%; height: 126px;" border="1">
<tbody>
<tr style="height: 18px;">
<td style="width: 23.4375%; text-align: center; height: 18px;"><span style="text-decoration: underline;"><strong>Name</strong></span></td>
<td style="width: 41.9034%; text-align: center; height: 18px;"><span style="text-decoration: underline;"><strong>Description</strong></span></td>
<td style="width: 34.6591%; text-align: center; height: 18px;"><span style="text-decoration: underline;"><strong>Function available</strong></span></td>
</tr>
<tr style="height: 18px;">
<td style="width: 23.4375%; height: 18px; text-align: center;">base</td>
<td style="width: 41.9034%; height: 18px;">The bass class used by all menu.<br />All function discribe here is available in all other elements.</td>
<td style="width: 34.6591%; height: 18px;">SetName(string)<br />GetName()<br />SetInfo(string)<br />GetInfo()<br />IsError()<br />GetError()<br />SetBGColor(ConsoleColor)<br />SetFGColor(ConsoleColor)<br />ToString()</td>
</tr>
<tr style="height: 18px;">
<td style="width: 23.4375%; height: 18px; text-align: center;">textbox</td>
<td style="width: 41.9034%; height: 18px;">A simple text box.</td>
<td style="width: 34.6591%; height: 18px;">(same as base)</td>
</tr>
<tr style="height: 18px;">
<td style="width: 23.4375%; text-align: center; height: 18px;">spacer</td>
<td style="width: 41.9034%; height: 18px;">A empty space which can be added between two elements to separate them. The height is configurable.</td>
<td style="width: 34.6591%; height: 18px;">(same as base)<br />SetMaxHight(int)</td>
</tr>
<tr style="height: 18px;">
<td style="width: 23.4375%; text-align: center; height: 18px;">menu</td>
<td style="width: 41.9034%; height: 18px;">Creates a menu that allows you to select one of the available options. The choice is transferred to the scriptblock.</td>
<td style="width: 34.6591%; height: 18px;">(same as base)<br />AddMenu(string, string)<br />&nbsp;- [Name, Info]<br />EditMenu(int, string, string)<br />&nbsp;- [id, Name, Info]<br />RemMenu(int) [id]<br />CountMenu()<br />SetExit(bool)<br />SetOutput(ScriptBlock)</td>
</tr>
<tr style="height: 18px;">
<td style="width: 23.4375%; text-align: center; height: 18px;">input</td>
<td style="width: 41.9034%; height: 18px;">Creates a menu allowing you to enter various requested values. The values are transmitted in the scriptblock.</td>
<td style="width: 34.6591%; height: 18px;">(same as base)<br />AddInput(string,string,string)<br />&nbsp;- [Name, Type, defaultVal]<br />EditInput(int,string,string,string)<br />&nbsp;- [id, Name, Type, defaultVal]<br />RemInput(int) [id]<br />CountInput()<br />SetOutput(ScriptBlock)</td>
</tr>
<tr style="height: 18px;">
<td style="width: 23.4375%; height: 18px; text-align: center;">progress</td>
<td style="width: 41.9034%; height: 18px;">Cr&eacute;e une ou plusieurs bar de progression.</td>
<td style="width: 34.6591%; height: 18px;">(same as base)<br />AddProgress(string,int,int)<br />- [info, value, maxValue]<br />EditProgress(int,string,int,int)<br />- [id, info, value, maxValue]<br />RemProgress(int) [id]</td>
</tr>
</tbody>
</table>

# Get Started
To get started, download the latest version of the bianary files : <a href="https://github.com/jo57430/PowerShell_menulib/raw/master/jb_ps_menulib.dll" target="_blank">jb_ps_menulib.dll</a></br>
(Last update > 16/06/2022)</br>
</br>
Then copy the file to the same folder where you want to create your powershell script.</br>
After that, add the module in your powershell instance by adding the following line :
```
  Import-Module '.\jb_ps_menulib.dll';
```
# How to use this module ?
First, create the a new instence of 'New-JBMenuManager',</br>
this element is like a window/frame, it will contain all the items of your menu.
```
  # Create a new instance of 'JBMenuManage' and store it in $frame
  $frame = New-JBMenuManager;
```
After that we set the width of the window, by default it is 40 char. 
```
  $frame.SetMaxWidth(80);
```
By default when you create an element, its name and description is named 'base_menu_xx'. 
This option allows you to deactivate it and not to pre-fill this value.
```
  $frame.RemDefaultName($ture);
```
From here on your window is ready to receive elements !
# How to add and configure the elements.
For this go to the 'example' folder and you will find what you need.
