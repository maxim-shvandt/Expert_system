using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace ExpertSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //var myList = new List<string> { "Yes", "No", "Maybe" };
            ExpSystem expertSystem = new ExpSystem("D:\\Knowledge_base.txt");

            //=============== DUBUG - Input =================================
            /*Console.WriteLine("================================");
            foreach ( var obj in expertSystem.mInputDate )
            {
                Console.WriteLine(obj.ToString());
            }*/
            Console.WriteLine("::::::::::::::::: Input Data :::::::::::::::::");
            Console.WriteLine("=========== Objects ===============");

            foreach (var obj in expertSystem.getObjects() )
            {
                Console.WriteLine(obj.Key.ToString() + "::" + obj.Value.ToString());
            }

            Console.WriteLine("=========== Relations Types ==============");

            foreach (var obj in expertSystem.getRelations())
            {
                Console.WriteLine(obj.Key.ToString() + "::" + obj.Value.getRelationName().ToString() + "::" + obj.Value.getRelationType().ToString());
            }

            Console.WriteLine("========== Input Relations =============");

            int[,] tempMatrix = expertSystem.getGivenRelationMatrix();

            for ( int i = 0; i < expertSystem.getGivenRelationsListSize(); i++ )
            {
                Console.WriteLine(tempMatrix[i, 0].ToString() + "::" + tempMatrix[i, 1].ToString() + "::" + tempMatrix[i, 2].ToString());
            }

            Console.WriteLine("========== Processed data =============");

            for (int i = 0; i < expertSystem.getOutputRelations().Count; i++ )
            {
                Console.WriteLine(expertSystem.getOutputRelations()[i].mObj1Name + " " + expertSystem.getOutputRelations()[i].mRelName + " " + expertSystem.getOutputRelations()[i].mObj2Name);
            }

            Console.WriteLine("========== Processed data in codes =============");

            for (int i = 0; i < expertSystem.getOutputRelations().Count; i++)
            {
                Console.WriteLine(expertSystem.getOutputRelations()[i].mObj1Code.ToString() + ":" + expertSystem.getOutputRelations()[i].mRelCode.ToString() + ":" + expertSystem.getOutputRelations()[i].mObj2Code.ToString());
            }


            Console.WriteLine("==========-----------------------=============");
            Console.WriteLine("========== The data is read and processed. =============");
            Console.WriteLine("========== Enter the query by example: < ? : ? : ? > =============");

            string s = Console.ReadLine();

            while (s != "")
            {
                Console.WriteLine("========== ANSWER =============");
                expertSystem.queryAnalyser(s);
                Console.WriteLine("===============================");
                Console.WriteLine("========== Enter the query by example: < ? : ? : ? > =============");
                s = Console.ReadLine();
            }


        }
    }
}
