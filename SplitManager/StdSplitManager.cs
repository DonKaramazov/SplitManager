using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitManager
{
    public class StdSplitManager
    {
        private FileStream file;
        private const int MIN_FRAG_SIZE = 1024;
        private long[] _fragSize;
        private string[] _fragsName;

        private string _filename;     

        public FileStream File
        {
            get
            {
                return file;
            }
            set { file = value; }
        }

        public int Max_nb_frags { get; set; } = 100;

        public string[] FragsName
        {
            get
            {
                int fragSizesLength = FragSizes.Length;
                String[] fragsName = new string[fragSizesLength];
                for (int i = 0; i < fragSizesLength; i++)
                {
                    fragsName[i] = File.Name.Substring(0, File.Name.LastIndexOf('.')) + i + File.Name.Substring(File.Name.LastIndexOf('.'));
                }
                return fragsName;
            }
            set
            {
                _fragsName = value;
            }
        }

        public long[] FragSizes
        {
            get
            {
                return getFragsSizes();
            }
            set
            {
                _fragSize = value;
            }
        }

        public long[] getFragsSizes()
        {

            double frag = Math.Max(MIN_FRAG_SIZE, Math.Ceiling((double)(File.Length / Max_nb_frags))); // 45 647 * 5 = 228 234;                                                                                                                                   
            long q = File.Length / Max_nb_frags;

            long[] gSS = new long[Max_nb_frags];
            for (int i = 0; i < gSS.Length; i++)
            {
                gSS[i] += q;
            }

            if (q < MIN_FRAG_SIZE)
            {
                if (gSS.Length == Math.Ceiling((double)Max_nb_frags / MIN_FRAG_SIZE))
                {
                    long sum = 0;
                    for (int i = 0; i < gSS.Length - 2; i++)
                    {
                        sum += gSS[i];
                    }
                    for (int j = 0; j < gSS.Length - 1; j++)
                    {
                        if (gSS[gSS.Length - 1] == Max_nb_frags - sum)
                        {
                            gSS[j] = MIN_FRAG_SIZE;
                        }
                    }
                }
            }
            else if (q >= MIN_FRAG_SIZE)
            {
                if (gSS.Length == Max_nb_frags)
                {
                    for (int i = 0; i < File.Length % gSS.Length; i++)
                    {
                        gSS[i] = q + 1;
                    }
                    for (int j = (int)File.Length % gSS.Length; j < gSS.Length; j++)
                    {
                        gSS[j] = q;
                    }
                }
            }

            return gSS;
        }

        private long[] getSplitSize(long[] _fragSize)
        {
            long[] gSS = _fragSize;
            long sum = 0;
            foreach (long frag in _fragSize)
            {
                // Longeure total des tailles de fragments
                sum += frag;
            }

            if (File.Length <= sum)
            {
                if (gSS.Length <= _fragSize.Length)
                {
                    for (int i = 0; i < gSS.Length; i++)
                    {
                        //Vérifier qu'il y a plus d'un  fragment et que la taille du fragment gss et inférieur à celui de fragSizes
                        if (gSS[gSS.Length - 1] > 0 && gSS[gSS.Length - 1] <= _fragSize[gSS.Length - 1])
                        {
                            gSS[i] = _fragSize[i];
                        }
                    }
                }
            }

            //Dans le cas inverse ...
            if (File.Length > sum)
            {
                long somme = 0;
                foreach (long frag in _fragSize)
                {
                    somme += frag;
                }

                // Si le calcul automatique possède 1 fragment de plus que celui donné
                if (gSS.Length == _fragSize.Length + 1)
                {
                    for (int i = 0; i < _fragSize.Length; i++)
                    {
                        if (gSS[_fragSize.Length] == File.Length - somme)
                        {
                            gSS[i] = _fragSize[i];
                        }
                    }
                }
            }

            return gSS;
        }

        #region Constructors
        public StdSplitManager(FileStream f)
        {
            this.File = f;
        }

       
        public StdSplitManager(FileStream f, int nbFrags)
        {
            this.File = f;
            this.Max_nb_frags = nbFrags;
        }


        public StdSplitManager(FileStream f, long[] fragSizes)
        {
            this.File = f;
            this.FragSizes = fragSizes;
        }
        #endregion


        public bool IsFileValid(FileStream f)
        {
            return File != null && Max_nb_frags >= 0;
        }
     

        public void Split()
        {
            if (File == null)
            {
                throw new FileNotFoundException("File Not Found");
            }
            try
            {
                byte[] tabBytes = System.IO.File.ReadAllBytes(File.Name);
                var rem = tabBytes.AsEnumerable();
                int fragsNameLength = FragsName.Length;

                for (int i = 0; i < fragsNameLength; i++)
                {
                    // Taille de la ponction ( en nombre de bits)
                    var chunksize = FragSizes[i];

                    // Nombre de bits prit
                    var chunk = rem.Take((int)chunksize);

                    //Le restant apres ponction
                    rem = rem.Skip((int)chunksize);

                    // Déclaration d'un StreamWriter pour écrire dans le fichier
                    using (StreamWriter sW = new StreamWriter(FragsName[i]))
                    {
                        // Conversion des bits en string pour l'écriture
                        foreach (var bit in chunk)
                        {
                            sW.Write(System.Text.Encoding.Default.GetString(new[] { bit }));
                        }
                    }

                }
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }finally
            {
                if (File != null) File.Close();
            }
        }
    }
}
