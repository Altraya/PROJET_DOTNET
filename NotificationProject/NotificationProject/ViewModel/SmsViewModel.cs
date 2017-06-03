﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationProject.HelperClasses;
using System.Windows.Input;
using DataAccess.Model;
using System.Collections.ObjectModel;

namespace NotificationProject.ViewModel
{
    class SmsViewModel : ObservableObject, IPageViewModel
    {

        #region constructor
        public SmsViewModel()
        {
            //TODO enlever Desktop
            ButtonCommand = new RelayCommand(o => SendMessage("Desktop", PhoneNumber, SelectedDevice.Name, SmsText), n => CanSend());
            _devicesController = DevicesController.getInstance();
        }
        #endregion

        #region Fields
        private string _phoneNumber;
        private string _smsText;
        public Device _selectedDevice;
        private DevicesController _devicesController;
        public ICommand ButtonCommand { get; set; }
        #endregion


        #region Properties
        public string Name
        {
            get
            {
                return "Sms View";
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }

        public string SmsText
        {
            get
            {
                return _smsText;
            }
            set
            {
                _smsText = value;
            }
        }

        public ObservableCollection<Device> ListDevices
        {
            get
            {
                return _devicesController.Devices;
            }
            set
            {
                _devicesController.Devices = value;
                OnPropertyChanged("ListDevices");
            }
        }
        #endregion
        public Device SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;

                // Tell to the view that SelectedDevice has changed
                OnPropertyChanged("SelectedDevice");
            }
        }

        #region Command
        private void SendMessage(string author, string receiver, string appareil, string message)
        {
            string messageToSend = JSONHandler.creationSMSString(author, receiver, appareil, message);
            SelectedDevice.sendMessage(messageToSend);
        }

        private bool CanSend()
        {
            return !String.IsNullOrEmpty(SmsText) && !String.IsNullOrEmpty(PhoneNumber) && SelectedDevice!=null;
        }
        #endregion


    }
}
