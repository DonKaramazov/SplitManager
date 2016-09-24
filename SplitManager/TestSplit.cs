using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitManager
{
    public class TestSplit
    {
        static void Main(string[] args )
        {

            FileStream f = new FileStream(@"Z:\DicoFrancais\liste_francais.txt",FileMode.Open,FileAccess.Read);

            
            StdSplitManager sM = new StdSplitManager(f, 5);
            try
            {
                sM.Split();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

