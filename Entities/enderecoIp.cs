using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CorrigeAdobe.Entities
{
    class enderecoIp
    {
        public string ipAdress { get; set; }

        public enderecoIp()
        {
        }

        public enderecoIp(string ip)
        {
            ipAdress = ip;
        }

        public Boolean isIPValid(string IPAdress)
        {
            Ping testPing = new Ping();
            try
            {
                if (testPing.Send(IPAdress).Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(IPAdress + " offline or invalid.");
                    return false;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Incorrect IP format.");
                return false;
            }
            catch (System.ArgumentOutOfRangeException emsg)
            {
                MessageBox.Show(emsg.ToString());
                return false;
            }
        }
    }
}
