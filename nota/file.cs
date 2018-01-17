using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;

namespace nota
{
    public class file
    {
        public void wFile(string path, string file, string[] conteudo, bool tp)
        {

            //nFile = Caminho e nome do arquivo
            //conteudo = O que será escrito no arquivo
            //tp = Modo de abertura do arquivo se true append

            int nLinhas = 0;

            string nFile = path + @"\" + file;

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            StreamWriter writer = new StreamWriter(nFile, tp);

            for (nLinhas = 0; nLinhas < conteudo.Length; nLinhas++)
            {
                writer.WriteLine(conteudo[nLinhas]);
            }

            writer.Close();

        }

        public List<string> rFile(string path, string file)
        {

            List<string> result = new List<string>();

            string line = "";

            int c = 0;

            string nFile = path + @"\" + file;

            try
            {

                StreamReader sReade = new StreamReader(nFile);

                while ((line = sReade.ReadLine()) != null)
                {

                    result.Add(line);

                    c++;

                }

            }
            catch (Exception e)
            {

                e.ToString();

                result.Add("FALSE");

            }

            return result;

        }

    }
}