using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Globalization;

namespace ExpertSystem
{
    class ExpSystem
    {
        //================== Special data types ===============================

        public class CRelation
        {
            CRelation()
            {
                mRelationName = "";
                mRelationType = 0;
            }

            public CRelation(string name, int type)
            {
                mRelationName = name;
                mRelationType = type;
            }

            public string getRelationName()
            {
                return mRelationName;
            }

            public int getRelationType()
            {
                return mRelationType;
            }

            private string mRelationName;
            private int mRelationType;
        }

        //--------------------------------------------------------

        public class CFinalRelation
        {
            public CFinalRelation()
            {
                mObj1Code = -1;
                mObj1Name = "?";
                mRelCode = -1;
                mRelName = "?";
                mObj2Code = -1;
                mObj2Name = "?";
            }

            public CFinalRelation( int obj1Code, string obj1Name, int relationCode, string relationName, int obj2Code, string obj2Name )
            {
                mObj1Code = obj1Code;
                mObj1Name = obj1Name;
                mRelCode = relationCode;
                mRelName = relationName;
                mObj2Code = obj2Code;
                mObj2Name = obj2Name;
            }

            public int mObj1Code;
            public string mObj1Name;
            public int mRelCode;
            public string mRelName;
            public int mObj2Code;
            public string mObj2Name;
        }

        //================== C-tor ===============================

        public ExpSystem(string path)
        {
            expertSystemCretion(path);
        }

        //================== Methods ===============================

        public Dictionary<int, string> getObjects()
        {
            return objects;
        }

        public Dictionary<int, CRelation> getRelations()
        {
            return relations;
        }

        public int[,] getGivenRelationMatrix()
        {
            return givenRelationMatrix;
        }

        public int getGivenRelationsListSize()
        {
            return givenRelations.Count;
        }

        public int[,,] getRelationMatrix()
        {
            return relationMatrix;
        }

        public int getRelationMatrixSize()
        {
            return maxVal;
        }

        public List<CFinalRelation> getOutputRelations()
        {
            return mOutputRelations;
        }

        public void expertSystemCretion(string path)
        {
            maxVal = 0;
            mDataPath = path;
            string[] lines = System.IO.File.ReadAllLines(mDataPath);
            string[] splittedData;
            int dataTypeMode = 0;

            //input data parsing
            foreach (string s in lines)
            {
                mInputDate.Add(s);
            }

            relations.Add(0, new CRelation("not connected", -1));

            foreach (string s in mInputDate)
            {
                if (s == "#1")
                {
                    dataTypeMode = 1;
                }
                else if (s == "#2")
                {
                    dataTypeMode = 2;
                }
                else if (s == "#3")
                {
                    dataTypeMode = 3;
                }

                if (dataTypeMode == 1)
                {
                    if (s != "" && !s.StartsWith("#"))
                    {
                        splittedData = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        objects.Add(Convert.ToInt32(splittedData[0]), splittedData[1]);
                    }
                    else if (s == "")
                    {
                        //Console.WriteLine("----");
                    }
                }
                else if (dataTypeMode == 2)
                {
                    if (s != "" && !s.StartsWith("#"))
                    {
                        splittedData = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        relations.Add(Convert.ToInt32(splittedData[0]), new CRelation(splittedData[1], Convert.ToInt32(splittedData[2])));
                    }
                    else if (s == "")
                    {
                        //Console.WriteLine("----");
                    }
                }
                else if (dataTypeMode == 3)
                {
                    if (s != "" && !s.StartsWith("#") && s != "" )
                    {
                        givenRelations.Add(s);
                        //Console.WriteLine(s);
                    }
                }
            }

            foreach( var pair in objects )
            {
                if( Convert.ToInt32(pair.Key) > maxVal )
                {
                    maxVal = Convert.ToInt32(pair.Key) + 1;
                }
            }

            givenRelationMatrix = new int[givenRelations.Count, 3];
            //Console.WriteLine("-----------------------------------------------");

            //creating and filling matrix of the given relations
            for (int i = 0; i < givenRelations.Count(); i++ )
            {
                //Console.WriteLine(givenRelations[i]);
                splittedData = givenRelations[i].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                givenRelationMatrix[i,0] = Convert.ToInt32(splittedData[0]);
                givenRelationMatrix[i,1] = Convert.ToInt32(splittedData[1]);
                givenRelationMatrix[i,2] = Convert.ToInt32(splittedData[2]);
                //Console.WriteLine(Convert.ToInt32(splittedData[0]).ToString() + "*" + Convert.ToInt32(splittedData[1]).ToString() + "*" + Convert.ToInt32(splittedData[2]).ToString());
             }

            Console.WriteLine(maxVal);

            //creating and initialising relation matrix
            relationMatrix = new int[maxVal, maxVal, 2];//1st dim for storing relation number(coded name), 2d dimension for storing relation type (rank)

            for ( int i = 0; i < maxVal; i++ )
            {
                for (int j = 0; j < maxVal; j++)
                {
                    if ( (i == 0 && j == 0 ) )
                    {
                        relationMatrix[i, j, 0] = -1;
                        relationMatrix[i, j, 1] = -1;
                    }
                    else if ( i == 0 )
                    {
                        relationMatrix[i, j, 0] = j;
                        relationMatrix[i, j, 1] = j;
                    }
                    else if (j == 0)
                    {
                        relationMatrix[i, j, 0] = -i;
                        relationMatrix[i, j, 1] = -i;
                    }
                    else
                    {
                        relationMatrix[i, j, 0] = -1;
                        relationMatrix[i, j, 1] = -1;
                    }
                    
                }
            }

            //filling relation matrix with given relations
            for (int i = 0; i < givenRelations.Count(); i++)
            {
                int row = givenRelationMatrix[i, 0];
                int col = givenRelationMatrix[i, 2];
                int relationCodedName = givenRelationMatrix[i, 1];
                relationMatrix[row, col, 0] = relationCodedName;
                relationMatrix[row, col, 1] = relations[relationCodedName].getRelationType();

                mOutputRelations.Add(new CFinalRelation(row, objects[row], relationCodedName, relations[relationCodedName].getRelationName(), col, objects[col]));
            }

            //making new connections
            bool newConnectionAdded = true;

            while (newConnectionAdded == true)
            {
                
                for (int i = 0; i < givenRelations.Count(); i++)
                {
                    //<x A y1>
                    int x = givenRelationMatrix[i, 0];
                    int relation_A = relations[givenRelationMatrix[i, 1]].getRelationType();
                    int relA_code = givenRelationMatrix[i, 1];
                    int y1 = givenRelationMatrix[i, 2];

                    for (int j = 0; j < givenRelations.Count(); j++)
                    {
                    
                        //<y2 B z>
                        int y2 = givenRelationMatrix[j, 0];
                        int relation_B = relations[givenRelationMatrix[j, 1]].getRelationType();
                        int relB_code = givenRelationMatrix[j, 1];
                        int z = givenRelationMatrix[j, 2];
                    
                        if (( (relation_A > relation_B) && relation_B != 0 ) && (y1 == y2 ) )
                        {
                            if (!mOutputRelations.Contains(new CFinalRelation(x, objects[x],
                                relA_code, relations[relA_code].getRelationName(),
                                z, objects[z])))
                            {
                                mOutputRelations.Add(new CFinalRelation(x, objects[x],
                                relA_code, relations[relA_code].getRelationName(),
                                z, objects[z]));

                                relationMatrix[x, z, 0] = relA_code;
                                relationMatrix[x, z, 1] = relation_A;
                            }
                    
                            //x = givenRelationMatrix[i, 0];
                            //relation_A = relations[givenRelationMatrix[i, 1]].getRelationType();
                            //relA_code = givenRelationMatrix[i, 1];
                            y1 = z;
                    
                            //givenRelationMatrix[i, 0] : relation_A : givenRelationMatrix[j, 2];                    
                            newConnectionAdded = true;
                        }
                        else if((relation_A == relation_B) && (y1 == y2) )
                        {
                            if (!mOutputRelations.Contains(new CFinalRelation(x, objects[x],
                                relA_code, relations[relA_code].getRelationName(),
                                z, objects[z])))
                            {
                                mOutputRelations.Add(new CFinalRelation(x, objects[x],
                                relA_code, relations[relA_code].getRelationName(),
                                z, objects[z]));

                                relationMatrix[x, z, 0] = relA_code;
                                relationMatrix[x, z, 1] = relation_A;
                            }
                    
                            y1 = z;
                    
                            //givenRelationMatrix[i, 0] : relation_A : givenRelationMatrix[j, 2];                       
                            newConnectionAdded = true;
                        }
                        else if (( ( relation_A < relation_B) || (relation_B == 0)) && (y1 == y2) )
                        {
                            if(!mOutputRelations.Contains(new CFinalRelation(x, objects[x],
                                relB_code, relations[relB_code].getRelationName(),
                                z, objects[z])))
                            {
                                mOutputRelations.Add(new CFinalRelation(x, objects[x],
                                relB_code, relations[relB_code].getRelationName(),
                                z, objects[z]));

                                relationMatrix[x, z, 0] = relB_code;
                                relationMatrix[x, z, 1] = relation_B;
                            }
                    
                        relation_A = relations[relB_code].getRelationType();
                        relA_code = relB_code;
                        y1 = z;             
                        //givenRelationMatrix[i, 0] : relation_B : givenRelationMatrix[j, 2];                     
                        newConnectionAdded = true;
                        }
                    }
                }

                newConnectionAdded = false;
            }
        }

        public void queryAnalyser( string query )
        {
            string[] qwestion = query.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (qwestion[0] != "?" && qwestion[1] != "?" && qwestion[2] != "?")
            {
                int i = Convert.ToInt32(qwestion[0]);
                int j = Convert.ToInt32(qwestion[2]);
                int k = Convert.ToInt32(qwestion[1]);
                
                if (relationMatrix[i, j, 0] == k)
                {
                    Console.WriteLine("Yes");
                    Console.WriteLine(objects[i] + " " + relations[k].getRelationName() + " " + objects[j]);
                }        
                else
                {
                    Console.WriteLine("No");
                    Console.WriteLine("There is no such connection!");
                }         
            }
            else
            {
                string reqObj1Param = qwestion[0];
                int reqObj1Code = 0;
                string reqConnectionParam = qwestion[1];
                int reqConnectionCode = 0;
                string reqObj2Param = qwestion[2];
                int reqObj2Code = 0;

                if (reqObj1Param == "?")
                {
                    reqObj1Code = -1;
                }
                else
                {
                    reqObj1Code = Convert.ToInt32(reqObj1Param);
                }

                if (reqConnectionParam == "?")
                {
                    reqConnectionCode = -1;
                }
                else
                {
                    reqConnectionCode = Convert.ToInt32(reqConnectionParam);
                }

                if (reqObj2Param == "?")
                {
                    reqObj2Code = -1;
                }
                else
                {
                    reqObj2Code = Convert.ToInt32(reqObj2Param);
                }

                int count = 0;

                foreach (CFinalRelation rel in mOutputRelations )
                {
                    if ((      reqObj1Code == -1 || reqObj1Code == rel.mObj1Code) &&
                        (reqConnectionCode == -1 || reqConnectionCode == rel.mRelCode) &&
                        (      reqObj2Code == -1 || reqObj2Code == rel.mObj2Code) )
                    {
                        Console.WriteLine(rel.mObj1Name + " : " + rel.mRelName + " : " + rel.mObj2Name);
                        count++;
                    }
                }

                if ( count == 0)
                {
                    //Console.WriteLine("No connections pass this template");
                    Console.WriteLine(objects[Convert.ToInt32(reqObj1Param)] + " has no relation with other objects");
                }
            }

        }

        //================== Members ===============================

        private int maxVal;
        public ArrayList mInputDate = new ArrayList();
        List<string> givenRelations = new List<string>();
        private string mDataPath;//path to data file
        private int[,] givenRelationMatrix;
        private int[,,] relationMatrix;
        private Dictionary<int, string> objects = new Dictionary<int, string>();//< object code >< object name >
        private Dictionary<int, CRelation> relations = new Dictionary<int, CRelation>();//< relation code >< pair ( <relation name><relationType> ) >
        private List<CFinalRelation> mOutputRelations = new List<CFinalRelation>();//all relations we have and got
    }
}
