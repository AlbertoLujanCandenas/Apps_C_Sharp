   M S B A M L   `   `   `  ????I  EWpfNTelefonos1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfNTelefonos1.ViewWpfNTelefonos1.View  5   	   C&clr-namespace:WpfNTelefonos1.ConverterWpfNTelefonos1.Converter  5   	   $    WpfNTelefonos1.View.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WpfNTelefonos1.View     5   	   <	converter&clr-namespace:WpfNTelefonos1.Converter     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)    ? 	Resources 5         ListBoxItemTemplate1 ?? XPath4   .WpfNTelefonos1.Converter.NombreImagenConverter  	converter??%?   &	        &	 ]     ?? 5   
   .R?5      ? .+?5      ?? ??6   ?? $ nombre??5      5        5   
   6*   5      .??5      ?  ? DataContext 6   ??  ?? Mode$ OneWay=?  xmlTelefonos ?? Source# [ .+?5   
   ?? -   5         ??Name$ listPersonas??5   H   #???   6   $
??Left=?6^   $	??389??6y   $??	21,35,0,0q?6?   $	??Top=?6?   $	??286??6?   ??6?   ?? $ /listatelefonos/persona???? 5   
   $??Listado de Personas:??6   $
??Left=?6/   $??21,4,0,0q?6J   $	??Top=?6\   $	??286??6t   ? 5   
   $
??Left=?6   $	??389??6*   $??
312,35,0,0q?67   $	??Top=?6K   $	??478??6c    6o   ?? *SelectedItem+ ?? ElementName$ listPersonas??.+?5      ?? $
??Left=?6   $??50??63   $??	22,10,0,0q?6?   $
??Wrap=?6p   $	??Top=?6?   $	??304??6?   ??6R   ?? $ nombre??6?   ?? 5      $
??Left=?6   $??50??63   $??
22,104,0,0q?6?   $
??Wrap=?6s   $	??Top=?6?   $	??304??6?   ??6S   ?? $ telefono??6?   ?? 5      $??Center=?6   $??50??65   $??	0,205,0,0q?6A   $
??Wrap=?6t   $	??Top=?6?   $	??434??6?   ??6T   ?? $ es_movil??6?   ?? 5      $??Center=?6   $??50??65   $??	0,316,0,0q?6A   $
??Wrap=?6u   $	??Top=?6?   $	??434??6?   ??6T   ?? $ 	direccion??6?   ?? 5      $
??Left=?6   $	??100??6/   $??
357,22,0,0q?6<   $	??Top=?6P   $	??100??6h   ??6t   ?? $
 foto?? ?? 	Converter# [ 5       5!      5"      