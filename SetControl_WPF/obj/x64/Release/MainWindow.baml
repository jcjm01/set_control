   M S B A M L   `   `   `  ����   SetControl_WPF    SetControl_WPF.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
MainWindow��$	��600��$	��567�� � Icon$ Images/icon.ico�� � 	Resources (  "System.Windows.Controls.DatePicker �� 
TargetType �� Property �� Value��%�   '        '�� q     �� "  .���  � �� �#333333��  � ��
 �   ���  � ��
 �������  � ��$
 Bold"��� " ��.���  � �� �#333333��  � ��
 �   ���  � ��$
 Bold"���  � ��
 �����.��� $��0q�����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #1E1E1E��	 � Offset$		 0.0Q�� $ #3D3D3D��$		 1.0Q�.+��� $��Images/set_control.png��$��Center=�$	��Top=�$��80��$��80��$��0,10,0,0q��� $��Tipo de producto:��$
��Left=�$	��Top=�$��	36,30,0,0q�$
��Bold"�
��������� -   
 ��Name$
 textBoxTipoProducto��$
��Left=�$	��Top=�$	��120��$��	36,50,0,0q����#333333
�������$
��Bold"��� $��Lote:��$
��Left=�$	��Top=�$��	36,90,0,0q�$
��Bold"�
��������� -   $
 textBoxLote��$
��Left=�$	��Top=�$	��120��$��
36,110,0,0q����#333333
�������$
��Bold"��� $��Elaboración:��$
��Left=�$	��Top=�$��
36,150,0,0q�$
��Bold"�
�������  -     Name$ dateTimePickerElaboracion��$
��Left=�$	��Top=�$	��120��$��
36,170,0,0q��� $��
Caducidad:��$
��Left=�$	��Top=�$��
36,210,0,0q�$
��Bold"�
�������  -   $ dateTimePickerCaducidad��$
��Left=�$	��Top=�$	��120��$��
36,236,0,0q��� $��Bidón:��$��Right=�$	��Top=�$��
0,42,144,0q�$
��Bold"�
��������� -    ��Name$ comboBoxBidon��$
��Left=�$	��Top=�$	��150��$��226,189,0,0q�.@��� $��Grande���� $��Chico���� -    ��Name$ pbStatusLed��$��Right=�$	��Top=�$��30��$��30��$��
0,68,164,0q��� -    ��Name$ 	lblEstado��$��Estado: Detenido��$��Center=�$	��Top=�$��	0,145,0,0q�
�������$
��Bold"��� -    ��Name$ groupBoxVistaPrevia��$��Vista Previa��$
��Left=�$	��Top=�$	��496��$	��200��$��
42,272,0,0q�
�������.��� .+��� -	   $ pictureBoxQRCode��$
��Left=�$	��Top=�$	��500��$	��500��$��Uniform=��� $
��Left=�$	��Top=�$��
130,10,0,0q�.+��� -
    ��Name$ textPreviewProducto��$��BLEEM30��$��16(�$
��Bold"�
��������� -   $ textPreviewLote��$��LOTE: BLI2022024��$��14(�
�������$��0,10,0,0q��� -   $ textPreviewElab��$��ELAB: 12022024��$��14(�
�������$��0,5,0,0q��� -   $ textPreviewCad��$��CAD: 12022026��$��14(�
�������$��0,5,0,0q�� -    �Name$ progressBarMarcado��$
��Left=�$	��Top=�$	��496��$��20��$��
36,480,0,0q��� -    ��Name$ btnPrevisualizar��$��Previsualizar��$
��Left=�$	��Top=�$	��120��$��
72,422,0,0q����#D32F2F
�������$
��Bold"�$��0q�$��5q��� -   $ btnIniciarMarcado��$��Iniciar Marcado��$
��Left=�$	��Top=�$	��131��$��301,422,0,0q����#1976D2
�������$
��Bold"�$��0q�$��5q��� -   $ 	btnLogout��$��Logout��$��Right=�$	��Top=�$	��120��$��	0,84,43,0q����#D32F2F
�������$
��Bold"�$��0q�$��5q��� -   $ btnGuardarQR��$��
Guardar QR��$
��Left=�$	��Top=�$	��120��$��306,372,0,0q����#1976D2
�������$
��Bold"�$��0q�$��5q��� -   $
 txtContador��$
��Left=�$	��Top=�$��80��$��
419,49,0,0q����#333333
�������$
��Bold"� J� RenderTransformOrigin$ 
0.488,0.67&��� -   $ btnShowHistory��$��Ver Historial��$��Right=�$	��Top=�$	��120��$��
0,119,47,0q����#FF9800
�������$
��Bold"�$��0q�$��5q�