using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitManager
{
    interface SplitManager
    {

        FileStream File { get; set; }
        int Max_nb_frags { get; set; }
        String[] FragsName { get; set; }
        long[] FragSizes { get; set; }
        

        /// <summary>
        /// Check if File Valid For Split
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        bool IsFileValid(FileStream f);

        //void setFile(FileStream f);

        /// <summary>
        /// Fixe la taille des fragments de fichier en fonction de la longeure donnée
        /// Seul le dernier fragment peut etre de longeur inférieur a la longeur ( C'est le restant ) 
        /// ex : 1024 1024 562
        /// </summary>
        void SetFragsSize(long fragSize);


        /// <summary>
        /// Fixe le nombre des fragments de fichier, qui sont alors tous à peu
	    ///  près de la même taille(à un octet près).
        /// </summary>
        /// <param name="number"></param>
        void setFragsNumber(int number);

        /// <summary>
        /// Effectue la fragmentation du fichier 
        /// </summary>
        void Split();
        
    }
}
