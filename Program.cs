using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace BeaconGWConsoleApp
{
    public class Class1
    {
        public Class1()
        {
            static void Main(string[] args)
            {
                string clientId = Guid.NewGuid().ToString();

                MqttClient mqttClient = new MqttClient("broker.hivemq.com");

                mqttClient.MqttMsgPublishReceived += client_recievedMessage;

                mqttClient.Connect(clientId);

                Console.WriteLine("subscribe to : /gw/ac233fc04a91/status");

                mqttClient.Subscribe(new String[] { "/gw/ac233fc04a91/status" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            }

            static void client_recievedMessage(object sender, MqttMsgPublishEventArgs e)
            {

                var json5 = System.Text.Encoding.Default.GetString(e.Message);
                List<Item> jsons = JsonConvert.DeserializeObject<List<Item>>(json5);

                foreach (Item element in jsons)
                {
                    if (element.type != "Unknown")
                    {
                        Console.WriteLine("Time :" + element.timestamp + " Type :" + element.type + " Mac :" + element.mac + " Name :" + element.bleName + " rssi :" + element.rssi);
                    }

                }

                #region raw
                ////-*--------------------------------------------------------------
                //using (var reader = new JsonTextReader(new StringReader(System.Text.Encoding.Default.GetString(e.Message))))
                //{
                //    //while (reader.Read())
                //    //{
                //    //    Console.WriteLine("{0} - {1} - {2}",
                //    //                     reader.TokenType, reader.ValueType, reader.Value);

                //    //}               
                //}
                ////-*--------------------------------------------------------------
                ////-*--------------------------------------------------------------
                //var message = System.Text.Encoding.Default.GetString(e.Message);
                //System.Console.WriteLine("Message received: " + message);
                ////-*--------------------------------------------------------------

                //var myJsonString = File.ReadAllText(message);
                //JObject json = JObject.Parse(myJsonString);

                //System.Console.WriteLine("Message received: " + json);
                //throw new NotImplementedException();
                #endregion
            }
        }

        public class Item
        {
            public DateTime timestamp;
            public string type;
            public string mac;
            public string bleName;
            public string ibeaconMajor;
            public string rssi;
        }
    }
}


