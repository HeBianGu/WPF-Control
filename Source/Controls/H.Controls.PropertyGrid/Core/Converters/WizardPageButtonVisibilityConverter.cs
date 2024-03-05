// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

//namespace H.Controls.PropertyGrid
//{
//  public class WizardPageButtonVisibilityConverter : IMultiValueConverter
//  {
//    public object Convert( object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture )
//    {
//      if( values == null || values.Length != 2 )
//        throw new ArgumentException( "Wrong number of arguments for WizardPageButtonVisibilityConverter." );

//      Visibility wizardVisibility = ( (values[ 0 ] == null) || (values[ 0 ] == DependencyProperty.UnsetValue) )
//                                    ? Visibility.Hidden
//                                    : ( Visibility )values[ 0 ];

//      WizardPageButtonVisibility wizardPageVisibility = ( (values[ 1 ] == null) || (values[ 1 ] == DependencyProperty.UnsetValue) )
//                                                        ? WizardPageButtonVisibility.Hidden
//                                                        : ( WizardPageButtonVisibility )values[ 1 ];

//      Visibility visibility = Visibility.Visible;

//      switch( wizardPageVisibility )
//      {
//        case WizardPageButtonVisibility.Inherit:
//          visibility = wizardVisibility;
//          break;
//        case WizardPageButtonVisibility.Collapsed:
//          visibility = Visibility.Collapsed;
//          break;
//        case WizardPageButtonVisibility.Hidden:
//          visibility = Visibility.Hidden;
//          break;
//        case WizardPageButtonVisibility.Visible:
//          visibility = Visibility.Visible;
//          break;
//      }

//      return visibility;
//    }

//    public object[] ConvertBack( object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture )
//    {
//      throw new NotImplementedException();
//    }
//  }
//}
