using System;
using System.Reflection;

using Com.QueoFlow.Commons.MVVM.ViewModels;

namespace De.BerndNet2000.SummitLog.Ui.About.ViewModel {
    /// <summary>
    ///     View Model für den About Dialog
    /// </summary>
    public class AboutViewModel : WindowViewModelBase, IAboutViewModel {
        private readonly string _applicationName;
        private readonly string _city;
        private readonly string _companyName;
        private readonly string _eMail;
        private readonly string _phone;
        private readonly string _street;
        private readonly string _version;
        private readonly string _web;

        /// <summary>
        ///     Ctor.
        /// </summary>
        public AboutViewModel() {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            _applicationName =
                    ((AssemblyProductAttribute)
                            Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
                                    typeof(AssemblyProductAttribute),
                                    false)).Product;
            _version = executingAssembly.GetName().Version.ToString();
            _city = @"01159 Dresden";
            _companyName = "queoflow";
            _street = @"Tharandter Str. 13";
            _web = @"http://www.queo-flow.com";
            _eMail = @"info@queoflow.com";
            _phone = @"+49 (0)351 213038 238";
        }

        /// <summary>
        ///     Liefert die Adresse.
        /// </summary>
        public string Address {
            get { return CompanyName + Environment.NewLine + Street + Environment.NewLine + City; }
        }

        /// <summary>
        ///     Liefert den Anwendungsnamen.
        /// </summary>
        public string ApplicationName {
            get { return _applicationName; }
        }

        /// <summary>
        ///     Liefert den Stadtnamen.
        /// </summary>
        public string City {
            get { return _city; }
        }

        /// <summary>
        ///     Liefert den Firmennamen.
        /// </summary>
        public string CompanyName {
            get { return _companyName; }
        }
        /// <summary>
        ///     Liefert den Titel für das Fenster.
        /// </summary>
        public override string DisplayName {
            get { return "Info über Trackingtool Logistik"; }
        }

        /// <summary>
        ///     Liefert die Mail Adresse.
        /// </summary>
        public string EMail {
            get { return _eMail; }
        }
        /// <summary>
        ///     Liefert die Telefonnummer.
        /// </summary>
        public string Phone {
            get { return _phone; }
        }

        /// <summary>
        ///     Liefert die Straße.
        /// </summary>
        public string Street {
            get { return _street; }
        }

        /// <summary>
        ///     Liefert die Versionsnummer.
        /// </summary>
        public string Version {
            get { return _version; }
        }

        /// <summary>
        ///     Liefert die Webadresse.
        /// </summary>
        public string Web {
            get { return _web; }
        }

        /// <summary>
        /// Lädt die Daten dieses View Models.
        /// </summary>
        public override void LoadData() {
            
        }
    }
}