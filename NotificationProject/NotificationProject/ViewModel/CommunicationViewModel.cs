﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationProject.HelperClasses;

namespace NotificationProject.ViewModel
{
    class CommunicationViewModel : ObservableObject, IPageViewModel
    {
        public String Name
        {
            get
            {
                return "Communication";
            }
        } 
    }
} 