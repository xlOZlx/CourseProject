   M S B A M L   `   `   `  ����I  EFantasyLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FantasyLibraryFantasyLibrary  5   	       FantasyLibrary.BookReader   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:FantasyLibrary     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
BookReader��5	   	   $	��600��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$
 None=�5
   	   $	��600��6   $	��800��6,    � 
ResizeMode$ NoResize=�6;    � 	Resources 5         MenuOpen �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value	 p� KeyTime
 p� Value  	MenuClose��%�  &	        &	 u    �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridContentBook��6a   .\�5        $ 0��6'   $ 0Q�63     5      $ 0:0:0.3��6'   $	 200Q�69   5      r� 5      $ Margin�6/   $ GridBookText��6R   .	�5      �� $	 0��6*   $
 0,35,0,0q�66   �� 5      $	 0:0:0.3��6*   $
 
200,35,0,0q�6<   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridContentBook��6a   .\�5        $ 0��6'   $	 200Q�63     5      $ 0:0:0.3��6'   $ 0Q�69   5      r� 5      $ Margin�6/   $ GridBookText��6R   .	�5      �� $	 0��6*   $
 
200,35,0,0q�66   �� 5      $	 0:0:0.3��6*   $
 0,35,0,0q�6<   5      5      5        � Triggers 5"      :� 5#   
    :� RoutedEvent$ ButtonBase.Click��6    :� 
SourceName$ ButtonOpenMenu��66   .[�5$      �� #��[  6   5%      :� 5&   
   $ ButtonBase.Click��6   $ ButtonCloseMenu��66   .[�5'      �� #��[ 6   5(      .��5*      � .+�5+   
   � $	��Top=�6   ���	#FFBD00BD$��35��6>   .+�5,      �� -   5         ��Name$ ButtonCloseMenu��5,      $��Center=�6[   $
��Left=�6v   ��6.   Z� ��6D   Z� .��5-      '  !MaterialDesignThemes.Wpf.PackIcon  
��������  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]]   Kind$ 	ArrowLeft=�6=   $��0q�6N   $��25��6Z   $��25��6e   5.      �� 5/      -   5        $ ButtonOpenMenu��5/      $��Center=�6Z   $
��Left=�6u   $y�	Collapsed=�6�   ��6-   Z� ��6C   Z� .��50        
�������$
 Menu=�6=   $��0q�6I   $��25��6U   $��25��6`   51      �� 52      $
��Left=�6   $��FLReader��63   
�������$��Center=�6V   $��16(�6q   $��70,0,0,0q�6   �� 53      -   5         ��Name$ NameBookInReader��53      $
��Name��62   $��Center=�6>   $��Center=�6Y   
�������$��18(�6�   �� 54      -   5        $ FullScreenOpenButton��54      $��0,0,35,0q�63   $��35��6E   $��35��6P   $��Center=�6\   $��Right=�55      $��0q�61   ��6=   Z� ��6S   Z� .��56        $ 
Fullscreen=�6*   $��35��6<   $��35��6H   $��Center=�57   *   $��Center=�6E   58      �� 59      -   5        $ FullScreenCloseButton��59      $��0,0,35,0q�64   $��35��6F   $��35��6Q   $��Center=�6]   $��Right=�5:      $��0q�61   $y�	Collapsed=�6j   ��6=   Z� ��6S   Z� .��5;        $ FullscreenExit=�6*   $��35��6@   $��35��6L   $��Center=�5<   *   $��Center=�6E   5=      �� 5>      -   5        $ CloseWindowButton��5>      $��35��60   $��35��6;   $��Right=�6G   $��0q�6c   $��Center=�6o   ��5?      Z� ��6+   Z� .��5@        $ Close=�6*   $��35��67   $��35��6C   $��Center=�5A   *   $��Center=�6E   5B      5C      � 5D   
   -   5         �Name$ GridContentBook��5D      $	��200��6(   $��0,35,0,0q�65   $
��Left=�6G   .+�5E      �� $
��Auto=�6   $��Disabled=�6>   .��5F      R� -   5         R�Name$ TV_ContentBook��5F      ���	#FF2E002E���	#FFBD00BD6J   5G      5H      � 5I   
   -	   5        $ GridBookText��5I      $��
200,35,0,0q�6%   .+�5J      �� -
   5         ��Name$ 	SVForBook��5J      $
��Auto=�6.   .��5K      �� -   5         ��Name$ FormForBook��5K      ���#fefcaf62   5L      5M      5N      5O      