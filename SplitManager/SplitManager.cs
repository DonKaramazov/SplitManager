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
        /// <summary>
        /// Recupère le fichier
        /// </summary>
        /// <returns></returns>
        File GetFile();

        /// <summary>
        /// Return names of fragmented files
        /// </summary>
        /// <returns></returns>
        String GetFragsNames();

        /// <summary>
        /// Nombre maximum de fichiers à fragmenté supporter par la configuration du fragmenteur
        /// </summary>
        /// <returns></returns>
        int GetMaxFragNb();

        /// <summary>
        /// Fixe les tailles des fragments de fichiers
        /// </summary>
        /// <returns></returns>
        long[] GetFragsSizes();

        /// <summary>
        /// Check if File Valid For Split
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        bool IsFileValid(File f);

        void setFile(File f);

        /// <summary>
        /// Fixe la taille des fragments de fichier en fonction de la longeure donnée
        /// Seul le dernier fragment peut etre de longeur inférieur a la longeur ( C'est le restant ) 
        /// ex : 1024 1024 562
        /// </summary>
        void SetFragsSize(long fragSize);

        /// <summary>
        /// Les valeurs peuvent etre différentes , on rajoute un fragment si la valeur totale est dépassée
        /// </summary>
        /// <param name="fragSizes"></param>
        void SetFragsSize(long[] fragSizes);


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
