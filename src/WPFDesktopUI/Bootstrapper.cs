using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDesktopUI.ViewModels;

namespace WPFDesktopUI {
  public class Bootstrapper : BootstrapperBase {
    private SimpleContainer _container = new SimpleContainer();

    public Bootstrapper() {
      Initialize();
    }

    protected override void Configure() {
      _container.Instance(_container);

      _container
        .Singleton<IWindowManager, WindowManager>()
        .Singleton<IEventAggregator, EventAggregator>();

      GetType().Assembly.GetTypes()
        .Where(type => type.IsClass)
        .Where(type => type.Name.EndsWith("ViewModel"))
        .ToList()
        .ForEach(viewModelType => _container.RegisterPerRequest(
          viewModelType, viewModelType.ToString(), viewModelType));
    }

    protected override void OnStartup(object sender, StartupEventArgs e) {
      // Update user file-save location to program data
      var environmentVariables = System.Environment.GetEnvironmentVariables();
      var programData = environmentVariables["ALLUSERSPROFILE"];
      AppDomain.CurrentDomain.SetData("DataDirectory", programData);

      // Add folders to save files to
      var logFilePath = System.IO.Path.Combine(programData.ToString(), "Sangwa Solutions/Invoice Importer by Sangwa/Logs");
      System.IO.Directory.CreateDirectory(logFilePath);
      var dbFilePath = System.IO.Path.Combine(programData.ToString(), "Sangwa Solutions/Invoice Importer by Sangwa/Database");
      System.IO.Directory.CreateDirectory(dbFilePath);


      DisplayRootViewFor<ShellViewModel>();
    }

    protected override object GetInstance(Type service, string key) {
      return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service) {
      return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance) {
      _container.BuildUp(instance);
    }
  }
}
