   M S B A M L   `   `   `  ����G  CMusic Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Music_PlayerMusic_Player  5          Music_Player.Player   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Music_Player     5      .��5	      � .+�5
   
   �� -   5          ��Name$  listaDeReproduccion��5
      $
��Left=�6/   $	��362��6J   $��
452,10,0,0q�6W   $	��Top=�6k   $	��158��6�   6�   �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $	��293��6:   $��	77,48,0,0q�6G   $	��Top=�6Z   $	��294��6r   $��Icon/music.png��6~   r� 5   
   -   5         r�Name$ mePlayer��5      $
��Left=�6)   $	��362��6D   $��	10,10,0,0q�6Q   $	��Top=�6d   $	��437��6|    r� LoadedBehavior$ Manual=�5       r� UnloadedBehavior$ Close=�6/    r� Stretch$
 Fill=�5   ;    r� Volume 6   �� *Value+ �� ElementName$ sliderVolumen�� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 9Q�6#   
   ShadowDepth$
 8Q�62   5      �� 5   
   -   5         ��Name$ 	btn_music��5      $��Button��6$   $
��Left=�65   $��133,398,0,0q�6P   $	��Top=�6e   $��37��6}   $��31��6�   ��6�   Z� ��6�   Z�  J� OpacityMask 5      �� 5       �� ImageSource$ Icon/playlist.png��6   ��5      �� 5      $ Icon/playlist.png��6   5      �� 5   
   -   5        $ 	btn_atras��5      $��Button��6$   $
��Left=�65   $��173,398,0,0q�6P   $	��Top=�6e   $��36��6}   $��31��6�   ��6�   Z� ��6�   Z� ��5      �� 5      $ Icon/previous.png��6    5       �� 5!      $ Icon/previous.png��6   5#      �� 5$   
   -   5        $ btn_stop��5$      $��Button��6#   $
��Left=�64   $��212,398,0,0q�6O   $	��Top=�6d   $��37��6|   $��31��6�   ��6�   Z� ��6�   Z�  5%      �� 5&      $ Icon/stop.png��6   ��5(      �� 5)      $ Icon/stop.png��6   5+      �� 5,   
   -   5        $ 	btn_pause��5,      $��Button��6$   $
��Left=�65   $��251,398,0,0q�6P   $	��Top=�6e   $��37��6}   $��31��6�   ��6�   Z� ��6�   Z�  5-      �� 5.      $ Icon/pause.png��6   ��50      �� 51      $ Icon/pause.png��6   53      �� 54   
   -   5        $ btn_play��54      $��Button��6#   $
��Left=�64   $��251,398,0,0q�6O   $	��Top=�6d   $��37��6|   $��31��6�   ��6�   Z� ��6�   Z�  55      �� 56      $ Icon/play.png��6   ��58      �� 59      $ Icon/play.png��6   5;      �� 5<   
   -	   5        $ btn_adelante��5<      $��Button��6'   $
��Left=�68   $��290,398,0,0q�6S   $	��Top=�6h   $��37��6�   $��31��6�   ��6�   Z� ��6�   Z� ��5=      �� 5>      $ Icon/next.png��6    5@      �� 5A      $ Icon/next.png��6   5C      �� 5D   
   -
   5         ��Name$ sliderVolumen��5D      $
��Left=�6(   $��385,404,0,15q�6C   $��Center=�6Y   $��80��6t   $��18��6    � Maximum$ 1Q�6�   $��80��6�    � Value$	 0.5Q�5E       �� TickFrequency$	 0.1Q�6    � SmallChange$	 0.1Q�5F       �� IsMoveToPointEnabled . 6   �� 5G   
   -   5        $ image_sonido��5G      $
��Left=�6&   $��31��6A   $��336,398,0,0q�6M   $	��Top=�6b   $��34��6z   $��Icon/volume.png��6�   �� 5H   
   -   5        $ sliderTimeLine��5H      $��10,379,10,39q�6)   $	��600��6?    .    SliderHazardStyle#���   5I      #��� ��5L   P   $��
2147483646��5M      $	 100Q�6'   ��5L   9   Z� 5M   5   �� 5O   
   -   5         ��Name$ lbl_total_time��5O      $��00:00��6(   $
��Left=�68   $��539,404,0,0q�6S   $	��Top=�6h   $��54��6�   �� 5P   
   -   5        $ 	lbl_timer��5P      $��00:00��6#   $
��Left=�63   $��470,404,0,0q�6N   $	��Top=�6c   $��55��6{   5R      5S      