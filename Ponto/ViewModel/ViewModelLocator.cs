using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Ponto.Navigation;

namespace Ponto.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<NavigationService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ExtratoViewModel>();
            SimpleIoc.Default.Register<FiltroBatidaViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ExtratoViewModel Extrato
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExtratoViewModel>();
            }
        }
        public FiltroBatidaViewModel FiltroBatida
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FiltroBatidaViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}