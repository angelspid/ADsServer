   M S B A M L   `   `   `  ����D  @ADsServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ADsServer	ADsServer  5   	       ADsServer.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ADsServer     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   ���	#FFDDF2FF.��5	      �  � ColumnDefinitions 5
   
   �� 5      .+�5   
   �� $��10,116,10,10q�6<   ��6   ��  �� Path$ Clients���5      �� 5      .R�5      �� 
������� �� CornerRadius$ 10,10,10,10��61   $	��442��6L   $	��240��6X   $��5,5,5,5q�6e   ._�5      � .+�5      �� $
��Left=�6X   $	��Top=�6s   $��	10,63,0,0q�6�   $��Segoe UI Semibold)�6�   $��15(�6�   ��6$   �� *	IpAddress+6�   �� 5      $��Client��6$   $
��Left=�66   $	��Top=�6Q   $��9,10,0,0q�6i   $��Segoe UI Semibold)�6{   $��20(�6�   �� 5      $��
IP Address��6$   $
��Left=�6:   $	��Top=�6U   $��	10,47,0,0q�6m   6�   �� 5      $
��Left=�6N   $	��Top=�6i   $��
10,109,0,0q�6�   $��Segoe UI Semibold)�6�   $��15(�6�   ��6$   �� *Port+6�   �� 5      $
��Port��6$   $
��Left=�64   $	��Top=�6O   $��	10,93,0,0q�6g   �� 5      $��	Connected��6$   $
��Left=�69   $	��Top=�6T   $��
10,139,0,0q�6l   �� 5      $��	Connected��6'   ${�False��6]   $
��Left=�6o   $	��Top=�6�   $��
11,165,0,0q�6�   $��Segoe UI Semibold)�6�   $��15(�6�   $��Center=�6�   $��Center=�6   d� 	IsChecked 6;   �� *IsConnected+6  �� 5      $��Remove client��6%   $
��Left=�6=   $��31��6X   $��8,0,0,10q�6d   $��Bottom=�6v   $	��109��6�   �� 5      $
��Left=�6M   $	��Top=�6h   $��	69,12,0,0q�6�      Symbols#���   6�   $��25(�6�   ��6$   �� *Status+6�   �� 5      $��	User list��6$   $
��Left=�69   $	��Top=�6T   $��
168,12,0,0q�6l   $��Segoe UI Semibold)�6�   $��15(�6�   �� 5      $��168,42,9,10q�6+   $
��Auto=�6@   .��5   "   �� ��6/   �� *Users+��5   "   �� 5   &   .R�5    *   �� ���	#FFF3F3F3$ 10,10,10,10��6H   $	��250��6c   $��49��6o   $��0,0,0,10q�6{   ._�5!   .   � .+�5"   2   �� $
��Left=�6k   $	��Top=�6�   $��	10,17,0,0q�6�   $��Segoe UI Semibold)�6�   $��15(�6�   ��68   �� *
Username+6�   �� 5#   2   $��Username��68   $
��Left=�6L   $	��Top=�6g   $��10,1,0,0q�6   �� 5$   2   $��Remove user��69   $
��Left=�6O   $��31��6j   $��	142,0,0,0q�6v   $��Center=�6�   $��98��6�   5%   /   5&   +   5'   '   ��5)   "   �� 5*   &   .R�5+   *   � 5,   '   5.      5/      50      51      52      ��54      �� 55      .R�56      � 57      59      � 5:   
   $	��112��6   $	��Top=�6   .+�5;      �� $��Status��6   $��Right=�6%   $	��Top=�6A   $��	0,10,10,0q�6Y   �� 5<      $��Right=�6F   $	��Top=�6b   $��	0,28,10,0q�6z   $��Segoe UI Semibold)�6�   ��6   �� *	IsRunning+6�   �� 5=      $��MySQL��6   $��Right=�6$   $	��Top=�6@   $��	0,56,10,0q�6X   �� 5>      $��Right=�6J   $	��Top=�6f   $��	0,74,10,0q�6~   $��Segoe UI Semibold)�6�   ��6   �� *IsConnected+6�   �� 5?      $��
IP Address��6   $
��Left=�6)   $	��Top=�6D   $��	10,10,0,0q�6\   �� 5@      $
��Left=�6H   $	��Top=�6c   $��	10,28,0,0q�6{   $��Segoe UI Semibold)�6�   ��6   �� *	IpAddress+6�   �� 5A      $
��Port��6   $
��Left=�6#   $	��Top=�6>   $��	10,56,0,0q�6V   �� 5B      $
��Left=�6>   $	��Top=�6Y   $��	10,74,0,0q�6q   $��Segoe UI Semibold)�6�   ��6   �� *Port+6�   5C      5E      5F      