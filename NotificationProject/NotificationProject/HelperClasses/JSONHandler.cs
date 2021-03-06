﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotificationProject.HelperClasses
{
    public class JSONHandler
    {
        public static JObject stringToJson(string chaine)
        {
            JObject message = null;
            try
            {
                message = JObject.Parse(chaine);
            }
            catch (Exception)
            {
                Console.WriteLine("Problem in parsing string to json");
            }
            return message;
        }

        public static string[] interpretation(JObject json)
        {
            try
            {
                string[] res = new string[3] { "", "", "" };
                string type = null;
                type = (string)json["type"];
                string conn = null;
                conn = (string)(json["conn"]);
                if (type != "")
                {  
                    res[0] = "Connection";
                    res[1] = author;
                    res[2] = ipAddress.ToString() + ":" + port.ToString() + ":" + pairaineKey[1];
            
                    Console.WriteLine("Demande Connection");
                    Console.WriteLine("L'appareil " + author + "(" + ipAddress.ToString() + ":" + port.ToString() + ") souhaite se connecter.");
                }
                else if (type.ToLower() == "disconnect") //1.Type 2.Author 3.IPaddress@Port
                {
                    res[0] = "Disconnection";
                    res[1] = author;
                    res[2] = ipAddress.ToString() + ":" + port.ToString();
                    Console.WriteLine("Demande Connection");
                    Console.WriteLine("L'appareil " + author + "(" + ipAddress.ToString() + ":" + port.ToString() + ") souhaite se deconnecter.");
                }

                else if (type.ToLower() == "notification") //1.Type 2.Application 3.Message
                {
                    res[0] = "Notification";
                    Console.WriteLine("Notification");
                    IList<string> allObject = json["object"].Select(t => (string)t).ToList();
                    string application = allObject[0];
                    string date = allObject[2]; 
                    string message = allObject[1];
                    res[1] = application;
                    res[2] = message;  
                    //Démonstration utilisation des objets obtenus depuis le JSON
                    Console.WriteLine("L'application " + application + " a reçu le message suivant: '" + message + "' depuis l'appareil de " + author + " à " + date + ".");
                }
                else
                    Console.WriteLine("Message invalide.");
                return res;
            }
            catch(Exception)
            {
                Console.WriteLine("Problem in JSON interpretation");
            }
            return null;
            
        }

        public static JObject messageRetour(string case1, string case2, string case3)
        {
            JObject json;
            string res = "";
            if(case1.ToLower() == "connect" || case1.ToLower() == "disconnect")
            {
                res = @"{type: '"+ case1 +"', conn: '"+case3+"', author: '"+case2+"', object: null}";
            }
            json = stringToJson(res);
            return json;
        }

        public static string creationSMSString(string author, string receiver, string appareil, string message)
        {
            var dt = DateTime.Now;
            return @"{type: 'smsToSend', conn: '" + appareil + "',author: '" + author + "', receiver: '" + receiver + "',object: {application: 'com.google.android.apps.messaging',Message: '" + message + "',heureDate: '" + dt + "'}}";
        }
    }
}
