   M S B A M L   `   `   `  ����>  :ELM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ELMELM  5   	       ELM.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:ELM     5   	     � Title$	  ELM��5   	   $	��410��6   $
��1055��6"   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $��0* �6    � ColumnDefinitions 5   
   �� 5      $
��370* �6   �� 5      $
��680* �6   .+�5   
   �� -   5         ��Name$ 	msgHeader��5      $
��Left=�6%   $��25��6@   $��	25,45,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��190��6�    � ToolTip$Z THeader should begin with E for Email, S for SMS or T for Tweet, followed by 9 digits��6�   �� 5   
   $��Message Header��6   $
��Left=�6)   $��30��6D   $��	25,10,0,0q�6P   $	��Top=�6c   $	��190��6{   �� 5   
   $��Message Body��6   $
��Left=�6'   $��30��6B   $��
235,10,0,0q�6N   $	��Top=�6b   $	��225��6z   $��2��6�   �� 5   
   -   5        $ msgBody��5      $
��Left=�6#   $	��315��6>   $��
235,45,0,0q�6K   $
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��225��6�   $� �If message is an Email then the body should contain: email address - subject line - message text.
If the message is SMS then the body should contain: international number (beginning with +) - message text.
If the message is a Tweet then the body should contain: twitter ID - message text.
The - signifies a new line.��6�    �� AcceptsReturn$
 True��6�  $��2��6
  �� 5   
   -   5         ��Name$ msgInput��5      $
��Left=�6#   $��25��6>   $��	25,80,0,0q�6J   $	��Top=�6]   $	��190��6u   $��Message Input��6�   �� 5   
   $��Message Output��6   $
��Left=�6)   $��30��6D   $��
110,10,0,0q�6P   $	��Top=�6d   $	��190��6|   $��1��6�   �� 5   
   -   5        $ 	msgOutput��5      $
��Left=�6%   $	��315��6@   $��
110,45,0,0q�6M   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��240��6�    �� 
IsReadOnly$
 True��6�   ${�False��6�   	 �� IsUndoEnabled$	 False��6�   $��1��6�   �� 5   
   -   5        $ 	msgSelect��5      $��Message Select��6$   $
��Left=�6=   $��25��6X   $��
25,115,0,0q�6d   $	��Top=�6x   $	��190��6�   $L FTo select a message enter either: Email/SMS/Tweet followed by 9 digits��6�   �� 5   
   -   5        $ trending��5      $
��Left=�6$   $��70��6?   $��
25,180,0,0q�6K   $
 Wrap=�6_   $	��Top=�6s   $	��190��6�   ${�False��6�   $
 True��6�   �� 5   
   $��Trending List��6   $
��Left=�6(   $��30��6C   $��
25,150,0,0q�6O   $	��Top=�6c   $	��190��6{   �� 5   
   -   5        $ mentions��5      $
��Left=�6$   $��70��6?   $��
25,290,0,0q�6K   $
 Wrap=�6_   $	��Top=�6s   $	��190��6�   ${�False��6�   $
 True��6�   �� 5   
   $��Mentions List��6   $
��Left=�6(   $��30��6C   $��
25,260,0,0q�6O   $	��Top=�6c   $	��190��6{   �� 5   
   $��Index��6   $
��Left=�6    $��30��6;   $��
370,10,0,0q�6G   $	��Top=�6[   $	��115��6s   $��1��6   �� 5   
   -   5        $ 	TypeIndex��5      $
��Left=�6%   $	��315��6@   $��
370,45,0,0q�6M   $����MessageType:
0 - Email
1 - SMS
2 - Tweet

EmailType:
0 - Standard
1 - SIR

NoIType:
0 - Bomb Threat
1 - Customer Attack
2 - Device Damage
3 - Personal Info Leak
4 - Raid
5 - Sport Injury
6 - Staff Abuse
7 - Staff Attack
8 - Suspicious Incident
9 - Terrorism
10 - Theft of Properties��6a   $
 Wrap=�6�  $	��Top=�6�  $	��115��6�  $��1��6  $��10(�6  $
 True��6)  �� 5    
   $��Nature of Incident List��6   $��1��62   $
��Left=�6B   $��30��6]   $��
505,10,0,0q�6i   $	��Top=�6}   $	��150��6�   �� 5!   
   -	   5        $ natureOfIncident��5!      $��1��6,   $
��Left=�6<   $	��315��6W   $��
505,45,0,0q�6d   $
 Wrap=�6x   $	��Top=�6�   $	��150��6�   ${�False��6�   $
 True��6�   5"      5#      