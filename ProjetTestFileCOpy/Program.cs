using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTestFileCOpy
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileStream f = new FileStream(@"Z:\DicoFrancais\liste_francais.txt", FileMode.Open, FileAccess.ReadWrite);
            FileStream f2 = new FileStream(@"Z:\DicoFrancais\liste_francaisCopy.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //using (FileStream stream = File.OpenRead(@"Z:\DicoFrancais\liste_francais.txt"))
            //using (FileStream writeStream = File.OpenWrite(@"Z:\DicoFrancais\liste_francaisCopy.txt"))
            //{

            //    byte[] buffer = new Byte[2000];
            //    long bytesRead;

            //    do
            //    {
            //        bytesRead = stream.Read(buffer, 0, buffer.Length);
            //        writeStream.Write(buffer, 0, (int)bytesRead);
            //    } while (bytesRead < buffer.Length);
            //}

            byte[] tabBytes = File.ReadAllBytes(@"Z:\DicoFrancais\liste_francais.txt");
            long[] splitSizes = new long[] { 45648, 45648, 45648, 45648, 45647 };
            string[] fileNames = new string[] {
            @"Z:\DicoFrancais\liste_francais1.txt" ,
            @"Z:\DicoFrancais\liste_francais2.txt",
            @"Z:\DicoFrancais\liste_francais3.txt",
            @"Z:\DicoFrancais\liste_francais4.txt",
            @"Z:\DicoFrancais\liste_francais5.txt"
            };

            var rem = tabBytes.AsEnumerable();
            for (int i = 0; i < splitSizes.Length; i++)
            {
                // Taille de la ponction
                var chunksize = splitSizes[i];

                // Nombre de bits prit
                var chunk = rem.Take((int)chunksize);
                int chunkCount = chunk.Count();


                // Le restant apres ponction

                 rem = rem.Skip((int)chunksize);
                //int remCount = rem.Count();

                // Tant qu'il reste des bits
                //while (chunk.Take(1).LongCount() > 0)
                //{
                    // Déclaration d'un StreamWriter pour écrire dans les fichier
                    using (StreamWriter sW = new StreamWriter(fileNames[i]))
                    {
                        //Conversion des bits en string pour l'écriture
                        foreach (var bit in chunk)
                        {
                            sW.Write(System.Text.Encoding.Default.GetString(new[] { bit }));
                        }
                    }
            }

            //int chunksize = 50000;
            //int cycle = 1;

            //var chunck = tabBytes.Take(chunksize);
            //var rem = tabBytes.Skip(chunksize);

            //while (chunck.Take(1).Count() > 0)
            //{
            //    string fileName = "file" + cycle.ToString() + ".txt";
            //    using (StreamWriter sW = new StreamWriter(fileName))
            //    {
            //        foreach (byte bit in chunck)
            //        {
            //            sW.Write(System.Text.Encoding.UTF8.GetString(new[] { bit }));
            //        }
            //    }
            //    chunck = rem.Take(chunksize);
            //    rem = rem.Skip(chunksize);
            //    cycle++;
            //}

        }
    }
}
