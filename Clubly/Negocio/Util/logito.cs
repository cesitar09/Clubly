using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace Negocio.Util
{
    public class logito
    {
        public static void ElLogeador(string evento, string nombre)
        {
            //string path = "https://www.dropbox.com/sh/68f3c8390ljb8o1/Gko3Jbp08U/log.txt";
            //string path = AppDomain.CurrentDomain.BaseDirectory+"log.txt";
           // var path = String.Format("log.txt", AppDomain.CurrentDomain.BaseDirectory);
            
            //if (File.Exists(path))
            //{
            //    DateTime fecha2 = DateTime.Now;
            //    string lines = "\n " + evento + "     " + nombre + "       " + fecha2.ToString();
            //    System.IO.StreamWriter file = new System.IO.StreamWriter(path, true);
            //    file.WriteLine(lines);
            //    file.Close();

            //}
            //else
            //{
                //File.Create(path);
                DateTime fecha2 = DateTime.Now;
                string lines = "\n " + evento + "     " + nombre + "       " + fecha2.ToString();
                //System.IO.StreamWriter file = new System.IO.StreamWriter(path, true);
                //file.WriteLine(lines);
                //file.Close();
            //}
        }
    }
}
