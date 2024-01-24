﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    internal class SerialPortsService
    {
        public static void Write(string zpl)
        {
            string[] portNames = GetPortNames();
            if (portNames.Length == 0)
            {
                Console.WriteLine("No se encontraron puertos");
                return;
            }

            Console.Write("Puertos encontrados: ");
            foreach (string portName in portNames)
            {
                Console.Write($"{portName} ");
            }

            SerialPort serialPort = CreateSerialPortInstance(portNames[0], 9600);

            try
            {
                serialPort.Open();
                if (serialPort == null || !serialPort.IsOpen)
                {
                    Console.WriteLine("Error al abrir el puerto");
                    return;
                }

                serialPort.Write(zpl);
                Console.WriteLine("\n\nZPL enviado correctamente\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                serialPort.Close();
            }
        }

        private static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        private static SerialPort CreateSerialPortInstance(string portName, int baudRate)
        {
            SerialPort serialPort = new SerialPort(portName, baudRate);
            return serialPort;
        }
    }
}
