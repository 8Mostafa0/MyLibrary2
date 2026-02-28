using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores.Interfaces
{
    public class SettingsStore : ISettingsStore
    {
        #region Dependencies
        private ILogger _logger;
        private const string KeyPath = $@"Softwar\MyApp\MyLibrary";
        #endregion

        #region Contructor
        public SettingsStore()
        {
            _logger = LoggerService.Logger;
        }
        #endregion


        #region Methods

        /// <summary>
        /// checks hash of entered password with store password
        /// </summary>
        /// <param name="EnteredPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string EnteredPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(EnteredPassword, GetHashedPassword());
        }
        /// <summary>
        /// hash non hashed password and stored in registry
        /// </summary>
        /// <param name="password"></param>
        public void SaveNoneHashedPassword(string password)
        {
            SetDataToRegistry("Password", BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13));
        }

        /// <summary>
        /// return hashed value of store password
        /// </summary>
        /// <returns></returns>
        public string GetHashedPassword()
        {
            return GetDataFromRegistry("Password")!;
        }

        /// <summary>
        /// get store loan settings
        /// </summary>
        /// <returns>Dictionary<string, int></returns>
        public Dictionary<string, int> GetLoansSetting()
        {
            Dictionary<string, int> Data = [];
            List<string> Keys = [
                "MaxBooksLoan",
                "MaxLoanDays"
            ];
            foreach (string key in Keys)
            {
                if (int.TryParse(GetDataFromRegistry(key), out int _))
                {
                    Data.Add(key, int.Parse(GetDataFromRegistry(key)!));
                }
                else
                {
                    SetDataToRegistry(key, key == "MaxBooksLoan" ? "5" : "30");
                    Data.Add(key, key == "MaxBooksLoan" ? 5 : 30);
                }
            }
            return Data;
        }

        /// <summary>
        /// Save setting dedicated to loans
        /// </summary>
        /// <param name="settings"></param>
        public void SaveLoanSettings(Dictionary<string, int> settings)
        {
            foreach (var value in settings)
            {
                SetDataToRegistry(value.Key, value.Value.ToString());
            }
        }

        /// <summary>
        /// load layout setting from registry path
        ///"ShowClientsCount",
        ///"ShowBooksCount",
        ///"ShowLoanedBooksCount",
        ///"ShowNotReturnedLoans"
        /// </summary>
        /// <returns> Dictionary<string, bool></returns>
        public Dictionary<string, bool> GetLayoutSettings()
        {
            Dictionary<string, bool> Data = [];
            List<string> Keys = [
                "ShowClientsCount",
                "ShowBooksCount",
                "ShowLoanedBooksCount",
                "ShowNotReturnedLoans"
            ];
            foreach (string key in Keys)
            {
                if (bool.TryParse(GetDataFromRegistry(key), out bool _))
                {
                    Data.Add(key, bool.Parse(GetDataFromRegistry(key)!));
                }
                else
                {
                    SetDataToRegistry(key, "true");
                    Data.Add(key, false);
                }
            }
            return Data;
        }
        /// <summary>
        /// save setting dedicated to layouts
        /// </summary>
        /// <param name="settings"></param>
        public void SaveLayoutSettings(Dictionary<string, bool> settings)
        {
            foreach (var value in settings)
            {
                SetDataToRegistry(value.Key, value.Value.ToString());
            }
        }


        /// <summary>
        /// get data from registry path
        /// </summary>
        /// <param name="key">the key for stored value</param>
        /// <returns></returns>
        private string? GetDataFromRegistry(string key)
        {
            try
            {

                using (RegistryKey? AppKey = Registry.CurrentUser.OpenSubKey(KeyPath))
                {
                    if (AppKey == null)
                    {

                        _logger.Warning("Cannot Create/Open Registery Key!");
                        return "";
                    }
                    string? value = AppKey.GetValue(key) as string;
                    return value ?? "";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return "";
            }
        }
        /// <summary>
        /// Save data to registry path
        /// </summary>
        /// <param name="key">unque key for store value</param>
        /// <param name="value">the value wants to store in the key address</param>
        private void SetDataToRegistry(string key, string value)
        {
            try
            {
                using (RegistryKey? AppKey = Registry.CurrentUser.CreateSubKey(KeyPath))
                {
                    if (AppKey == null)
                    {
                        _logger.Warning("Cannot Create/Open Registery Key!");
                        return;
                    }

                    AppKey.SetValue(key, value);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error("Access denied – run as administrator?\n" + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SetDataToRegistry");
            }
        }
        #endregion
    }
}
