   M S B A M L   `   `   `  ����   SetControl_WPF    SetControl_WPF.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
MainWindow��$	��700��$
��1150�� � Icon$ Images/icon.ico�� � 	Resources (  "System.Windows.Controls.DatePicker �� 
TargetType �� Property �� Value��%�   '        '�� q     �� "  .���  � �� �#333333��  � ��
 �   ���  � ��
 �������  � ��$
 Bold"��� " ��.���  � �� �#333333��  � ��
 �   ���  � ��$
 Bold"���  � ��
 �����.��� ����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #1E1E1E��	 � Offset$		 0.0Q�� $ #3D3D3D��$		 1.0Q�.+��� $��Images/set_control.png��$��Center=�$	��Top=�$	��100��$	��100��$��0,10,0,0q��� $��Tipo de producto:��$
��Left=�$	��Top=�$��
10,130,0,0q�$
��Bold"�
��������� -   
 ��Name$
 textBoxTipoProducto��$
��Left=�$	��Top=�$	��150��$��
10,150,0,0q����#333333
�������$
��Bold"��� $��Lote:��$
��Left=�$	��Top=�$��
10,190,0,0q�$
��Bold"�
��������� -   $
 textBoxLote��$
��Left=�$	��Top=�$	��150��$��
10,210,0,0q����#333333
�������$
��Bold"��� $��Elaboración:��$
��Left=�$	��Top=�$��
10,250,0,0q�$
��Bold"�
�������  -     Name$ dateTimePickerElaboracion��$
��Left=�$	��Top=�$	��150��$��
10,270,0,0q��� $��
Caducidad:��$
��Left=�$	��Top=�$��
10,310,0,0q�$
��Bold"�
�������  -   $ dateTimePickerCaducidad��$
��Left=�$	��Top=�$	��150��$��
10,330,0,0q��� $��Bidón:��$��Right=�$	��Top=�$��0,130,160,0q�$
��Bold"�
��������� -    ��Name$ comboBoxBidon��$��Right=�$	��Top=�$	��150��$��0,150,160,0q�.@��� $��Grande���� $��Chico���� -    ��Name$ pbStatusLed��$��Right=�$	��Top=�$��40��$��40��$��	0,10,50,0q��� -    ��Name$ 	lblEstado��$��Estado: Detenido��$��Right=�$	��Top=�$��	0,60,50,0q�
�������$
��Bold"��� $��Vista Previa��$
��Left=�$	��Top=�$	��500��$	��250��$��
10,400,0,0q�
�������.��� .+��� -   $ pictureBoxQRCode��$
��Left=�$	��Top=�$	��150��$	��150��$��	10,10,0,0q�$
��None=��� $
��Left=�$	��Top=�$��
170,10,0,0q�.+��� -	    ��Name$ textPreviewProducto��$��BLEEM30��$��20(�$
��Bold"�
��������� -
   $ textPreviewLote��$��LOTE: BLI2022024��$��16(�
�������$��0,10,0,0q��� -   $ textPreviewElab��$��ELAB: 12022024��$��16(�
�������$��0,5,0,0q��� -   $ textPreviewCad��$��CAD: 12022026��$��16(�
�������$��0,5,0,0q�� -    �Name$ progressBarMarcado��$
��Left=�$	��Top=�$	��350��$��750,352,0,0q��� -    ��Name$ btnPrevisualizar��$��Previsualizar��$
��Left=�$	��Top=�$	��150��$��
10,600,0,0q����#D32F2F
�������$
��Bold"�$��0q�$��10q��� -   $ btnIniciarMarcado��$��Iniciar Marcado��$
��Left=�$	��Top=�$	��150��$��170,600,0,0q����#1976D2
�������$
��Bold"�$��0q�$��10q��� -   $ 	btnLogout��$��Logout��$��Right=�$	��Top=�$	��150��$��0,525,108,0q����#D32F2F
�������$
��Bold"�$��0q�$��10q��� -   $
 txtContador��$
��Left=�$	��Top=�$	��100��$��901,464,0,0q����#333333
�������$
��Bold"��� -   $ btnShowHistory��$��Ver Historial��$��Right=�$	��Top=�$	��150��$��
0,583,99,0q����#FF9800
�������$
��Bold"�$��0q�$��10q�